using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xiki
{
    public partial class App : Application
    {

        public static string Host = "http://10.130.216.144";


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage (new Home());
           
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
