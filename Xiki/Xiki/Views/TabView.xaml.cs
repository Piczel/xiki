using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;
namespace Xiki.Views

{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TabView : ContentView
    {
        private static TabView instance;
        private static Dictionary<int, Tab> tabs = new Dictionary<int, Tab>();

        public static TabView GetInstance()
        {
            if(instance == null)
            {
                instance = new TabView();
            }

            return instance;
        }

		private TabView ()
		{
			InitializeComponent ();
        }

        public static async void OpenArticle (int articleID)
        {
            if(tabs.ContainsKey(articleID))
            {
                await SwitchToTab(tabs[articleID]);
            } else
            {
                StackLayout tabStash = GetInstance().FindByName("TabStash") as StackLayout;
                Tab tab = new Tab(articleID);

                tabStash.Children.Add(tab);
                tabs.Add(articleID, tab);

                await SwitchToTab(tab);

                tab.SetTitle((await ArticleView.Get(articleID)).GetTitle());
            }
        }


        private static void SetActive(Tab tab)
        {
            IList<View> tabs = (GetInstance().FindByName("TabStash") as StackLayout).Children;
            for (int i=0;i<tabs.Count;i++)
            {
                ((Tab) tabs[i]).SetInactive();
            }

            tab.SetActive();
        }


        public static async Task<bool> SwitchToTab (Tab tab)
        {

            ArticleView old = ArticlePage.GetArticleView();
            if (old != null)
            {
                await old.FadeOut(150);
            }

            // Retrieve ArticleView from cache (or load new)
            ArticleView article = await ArticleView.Get(tab.GetArticleID());
            ArticlePage.SetArticleView(article);
            SetActive(tab);
            return await article.FadeIn(250);
        }

        public static async Task<bool> CloseTab (Tab tab)
        {
            StackLayout tabStash = GetInstance().FindByName("TabStash") as StackLayout;
            tabStash.Children.Remove(tab);
            tabs.Remove(tab.GetArticleID());

            return await SwitchToTab((Tab)tabStash.Children.ElementAt(0)); // TO DO: pop navigation history
        }
    }
}