using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        
		public TabView (ArticlePage page)
		{
			InitializeComponent ();
            this.page = page; 
        }

        public void OpenTab (ArticleView article, Tab tab = null)
        {
            if (tab == null)
            {
                tab = new Tab(article.GetTitle(), article, this);
            }
            (FindByName("TabStash") as StackLayout).Children.Add(tab);
        }

       public void TabClicked (Tab tab)
        {
            page.setArticleView(tab.GetArticleView(), tab);
        }
    }
}