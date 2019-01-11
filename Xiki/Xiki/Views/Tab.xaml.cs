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

        public async Task<bool> TransitionIn()
        {
            bool x = await ViewExtensions.LayoutTo(this, new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, 120), 200, Easing.CubicInOut);
            x |= await (FindByName("CloseButton") as Button).FadeTo(1, 50);
            if (x || !x)
            {
                this.HeightRequest = 120;
            }

            return x;
        }

        public async Task<bool> TransitionOut()
        {
            (FindByName("TabName") as Label).FadeTo(0, 50);
            bool x = await (FindByName("CloseButton") as Button).FadeTo(0, 50);
            x |= await ViewExtensions.LayoutTo(this, new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, 20), 150, Easing.CubicInOut);
            if (x || !x)
            {
                this.HeightRequest = 20;
            }

            return x;
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