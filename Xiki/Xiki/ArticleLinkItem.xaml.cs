using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;

namespace Xiki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleLinkItem : ContentView
	{

        private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        private int articleID;

        public ArticleLinkItem(string title, string subtitle, int articleID)
        {
            InitializeComponent();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("lmao");
                Clicked();

            };

            GestureRecognizers.Add(tapGestureRecognizer);
            LabelTitle.Text = title;
            LabelSubTitle.Text = subtitle;
            this.articleID = articleID; 
        }

        private async void Clicked()
        {
            await Navigation.PushAsync(new ArticlePage());
        }
    }
}
