﻿using Newtonsoft.Json.Linq;
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

        private ArticlePage page;

        private Dictionary<int, Tab> openTabs = new Dictionary<int, Tab>();
        
		public TabView (ArticlePage page)
		{
			InitializeComponent ();
            this.page = page; 
        }

        public ArticleView OpenTab (int articleID)
        {
            // Opens a new tab

            ArticleView article = null;
            Tab tab = null;

            if (openTabs.ContainsKey(articleID))
            {
                tab = openTabs[articleID];
            
                article = tab.GetArticleView();
            } else
            {
                tab = new Tab(this);
                openTabs.Add(articleID, tab);

                article = new ArticleView(page, tab, articleID);
                (FindByName("TabStash") as StackLayout).Children.Add(tab);
            }

            SetActive(tab);

            return article;

        }



        public void SetActive(Tab tab)
        {
            // Opens existing tab
            IList<View> tabs = (FindByName("TabStash") as StackLayout).Children;
            for (int i=0;i<tabs.Count;i++)
            {
                ((Tab) tabs[i]).SetInactive();
            }

            tab.SetActive();

        }


        public void TabClicked (Tab tab)
        {
            page.setArticleView(tab);
        }
    }
}