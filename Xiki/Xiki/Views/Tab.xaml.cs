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

    public partial class Tab : ContentView
	{
    private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();


        private ArticleView article;
        
        private TabView tabView;

        public Tab (TabView tabView)
		{
			InitializeComponent ();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                 Clicked();
            };

            this.tabView = tabView; 

            GestureRecognizers.Add(tapGestureRecognizer);

            (FindByName("TabName") as Label).Text = "Untitled Tab";
            BackgroundColor = Color.Gainsboro;

        }

        public void SetArticleView(ArticleView article)
        {
            this.article = article;
            (FindByName("TabName") as Label).Text = article.GetTitle();
        }
        public ArticleView GetArticleView()
        {
            return article;
        }


        private void DeletusFetus(object sender, EventArgs e)
        {
            StackLayout tabs = tabView.FindByName("TabStash") as StackLayout;
            
            tabs.Children.Remove(this);


            try
            {
                tabView.TabClicked(
                    (Tab) tabs.Children.ElementAt(0)
                );

            } catch(ArgumentOutOfRangeException exc)
            {
                // All tabs closed

            }
        }

        public void SetActive()
        {
            this.BackgroundColor = Color.White;
        }

        public void SetInactive()
        {
            this.BackgroundColor = Color.Gainsboro;
        }

        private async void Clicked()
        {
            tabView.TabClicked(this);
        }


    }
}