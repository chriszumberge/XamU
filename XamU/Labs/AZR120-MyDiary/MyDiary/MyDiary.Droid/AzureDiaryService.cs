using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace MyDiary.Services
{
    partial class AzureDiaryService
    {
        private Task PlatformLoginAsync(MobileServiceAuthenticationProvider provider)
        {
            return azureClient.LoginAsync(Xamarin.Forms.Forms.Context, provider);
        }
    }
}