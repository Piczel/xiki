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
        
        private int articleID;

        public Tab (int articleID)
		{
			InitializeComponent ();
            this.articleID = articleID;

            tapGestureRecognizer.Tapped += (s, e) =>
            {
                 Clicked();
            };

            GestureRecognizers.Add(tapGestureRecognizer);

            (FindByName("TabName") as Label).Text = "";
            BackgroundColor = Color.Gainsboro;

        }

        public void SetTitle(string title)
        {
            (FindByName("TabName") as Label).Text = title;
        }

        public int GetArticleID()
        {
            return articleID;
        }


        private void DeletusFetus(object sender, EventArgs e)
        {
            TabView.CloseTab(this);
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
            await TabView.SwitchToTab(this);
        }


    }
}