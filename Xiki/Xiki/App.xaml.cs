﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xiki
{
    public partial class App : Application
    {

        public static string    Host    = "http://10.130.216.144";
        public static int       WikiID  = 6;


        public static string PATH_TP = "~theprovider/";
        public static string PATH_WIKI = PATH_TP +"wiki/php/";
        public static string GET_ARTICLES = PATH_WIKI +"get-articles.php";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ArticlePage(17));
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
