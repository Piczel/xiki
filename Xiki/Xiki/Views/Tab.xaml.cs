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

        public Tab (string name, ArticleView article, TabView tabView)
		{
			InitializeComponent ();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                 Clicked();
            };
           
            this.article = article;

            this.tabView = tabView; 

            GestureRecognizers.Add(tapGestureRecognizer);

            (FindByName("TabName") as Label).Text = "Jessica Jones";

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

        private async void Clicked()
        {
            tabView.TabClicked(this);
        }

        public ArticleView GetArticleView ()
        {
            return article;            
        }


    }
}