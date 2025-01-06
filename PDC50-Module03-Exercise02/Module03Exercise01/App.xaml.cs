using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Module03Exercise01.View;
using Module03Exercise01.Resources.Styles;
using Microsoft.Extensions.DependencyInjection;

namespace Module03Exercise01
{
    public partial class App : Application
    {
        private const string TestUrl = "https://www.reqbin.com/";
        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            MainPage = new AppShell();
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());
            }
            else if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Resources.MergedDictionaries.Add(new AndroidResources());
            }
        }

        protected override async void OnStart()
        {
            var current = Connectivity.NetworkAccess;

            bool isWebsiteReachable = await IsWebsiteReachable(TestUrl);

            if (current == NetworkAccess.Internet && isWebsiteReachable) 
            {
                await Shell.Current.GoToAsync("//LoginPage");
                MainPage = _serviceProvider.GetRequiredService<LoginPage>();
                Debug.WriteLine("Application started with LoginPage.");
            }
            else
            {
                await Shell.Current.GoToAsync("//OfflinePage");
                Debug.WriteLine("No internet connection or website unreachable. Navigating to OfflinePage.");
            }
        }

        protected override void OnSleep()
        {
            Debug.WriteLine("Application is sleeping.");
        }

        protected override void OnResume()
        {
            Debug.WriteLine("Application resumed.");
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception in IsWebsiteReachable: {ex.Message}");
                return false;
            }
        }
    }
}