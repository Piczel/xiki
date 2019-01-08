using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xiki
{
    public partial class App : Application
    {

        
        public static readonly string PATH_TP = "~theprovider/";
        public static readonly string PATH_WIKI = PATH_TP +"wiki/php/";
        public static readonly string GET_ARTICLES = PATH_WIKI +"get-articles.php";


        private static ISettings AppSettings => CrossSettings.Current;

        public static string Host
        {
            get => AppSettings.GetValueOrDefault(nameof(Host), "http://10.130.216.144");
            set => AppSettings.AddOrUpdateValue(nameof(Host), value);
        }

        public static int WikiID
        {
            get => AppSettings.GetValueOrDefault(nameof(WikiID), 6);
            set => AppSettings.AddOrUpdateValue(nameof(WikiID), value);
        }








        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (ArticlePage.GetInstance());
           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        public static async void PushArticleAsync(ArticlePage page)
        {
            
            await page.Navigation.PushAsync(page);

        }
    }
}
