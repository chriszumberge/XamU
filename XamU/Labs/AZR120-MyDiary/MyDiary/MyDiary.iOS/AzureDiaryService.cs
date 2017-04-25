using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace MyDiary.Services
{
    partial class AzureDiaryService
    {
        private Task PlatformLoginAsync(MobileServiceAuthenticationProvider provider)
        {
            return azureClient.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
        }
    }
}
