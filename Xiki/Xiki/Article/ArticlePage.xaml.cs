using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Views;

namespace Xiki.Article
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlePage : ContentPage
	{
        private TabView tabs;
		public ArticlePage (int articleID)
		{
			InitializeComponent();
            tabs = new TabView(this);
            (FindByName("HorizontalStack") as StackLayout).Children.Add(tabs);
            setArticleView(articleID);
            // DisplayAlert("Message", "Page loaded, ID: " + ArticleID, "OK");
        }
        public void setArticleView (int articleID)
        {

            // Creates a new view and loads its content

            (FindByName("ArticleViewport") as ScrollView).Content = tabs.OpenTab(articleID);
        }
        
        public void setArticleView(Tab tab)
        {
            // Sets the view from clicked tab


            tabs.SetActive(tab);
            (FindByName("ArticleViewport") as ScrollView).Content = tab.GetArticleView();
        }
      
    }
}