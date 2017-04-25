using CastMyVote.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastMyVote.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Diagnostics;

namespace CastMyVote.Services
{
    public class AzureSurveyService : ISurveyQuestionService
    {
        const string AzureServiceUrl = "https://xamu-voter.azurewebsites.net";
        MobileServiceClient mClient;

        IMobileServiceSyncTable<SurveyQuestion> mQuestionsTable;
        IMobileServiceSyncTable<SurveyResponse> mResponsesTable;

        async void InitializeAsync()
        {
            if (mClient != null)
                return;

            var store = new MobileServiceSQLiteStore("survery.db");
            store.DefineTable<SurveyQuestion>();
            store.DefineTable<SurveyResponse>();

            mClient = new MobileServiceClient(AzureServiceUrl);
            await mClient.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            mQuestionsTable = mClient.GetSyncTable<SurveyQuestion>();
            mResponsesTable = mClient.GetSyncTable<SurveyResponse>();

            try
            {
                await mClient.SyncContext.PushAsync();
                await mQuestionsTable.PullAsync("allQuestions", mQuestionsTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Got exception: {ex.Message}");
            }
        }

        async Task SynchronizeResponsesAsync(string questionId)
        {
            try
            {
                await mResponsesTable.PullAsync("syncResponses" + questionId,
                    mResponsesTable.Where(r => r.SurveyQuestionId == questionId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Got exception: {ex.Message}");
            }
        }

        public async Task AddOrUpdateSurveyResponseAsync(SurveyResponse response)
        {
            InitializeAsync();
            if (String.IsNullOrEmpty(response.Id))
            {
                await mResponsesTable.InsertAsync(response);
            }
            else
            {
                await mResponsesTable.UpdateAsync(response);
            }
            await SynchronizeResponsesAsync(response.SurveyQuestionId);
        }

        public async Task DeleteSurveyResponseAsync(SurveyResponse response)
        {
            InitializeAsync();
            await mResponsesTable.DeleteAsync(response);
            await SynchronizeResponsesAsync(response.SurveyQuestionId);
        }

        public Task<IEnumerable<SurveyQuestion>> GetQuestionsAsync()
        {
            InitializeAsync();
            return mQuestionsTable.ReadAsync();
        }

        string lastQuestionId;

        public async Task<SurveyResponse> GetResponseForSurveyAsync(string questionId, string name)
        {
            InitializeAsync();
            
            if (lastQuestionId != questionId)
            {
                // Get the latest responses for this question
                await SynchronizeResponsesAsync(questionId);
                lastQuestionId = questionId;
            }

            return (await mResponsesTable.Where(r => r.SurveyQuestionId == questionId && r.Name == name).ToEnumerableAsync()).FirstOrDefault();
        }

        public async Task<IEnumerable<SurveyResponse>> GetResponsesForSurveyAsync(string questionId)
        {
            InitializeAsync();
            return await mResponsesTable
                .Where(r => r.SurveyQuestionId == questionId)
                .OrderByDescending(r => r.UpdatedAt)
                .Take(100).ToEnumerableAsync();
        }
    }
}
