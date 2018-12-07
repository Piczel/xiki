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

        public static string Host = "http://10.130.216.144";

        public static List<ContentPage> History = new List<ContentPage>();

        private static int HistoryIndex = 0;
        
        private static INavigation Nav = null; 

        public static void Forward()
        {
            HistoryIndex++;
            Nav.PushModalAsync(History[HistoryIndex]);
        }

        public static void Backward()
        {
            HistoryIndex--;
            Nav.PushModalAsync(History[HistoryIndex]);
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new Home());
            Nav = MainPage.Navigation; 
           
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
    }
}
