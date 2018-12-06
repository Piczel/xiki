using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleLinkItem : ContentView
	{

        private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        public ArticleLinkItem(string title, string subtitle)
        {
            InitializeComponent();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Image Clicked w/ Lambda");
                Clicked();

            };

            GestureRecognizers.Add(tapGestureRecognizer);
            LabelTitle.Text = title;
            LabelSubTitle.Text = subtitle;
        }

        private async void Clicked()
        {
            await Navigation.PushAsync(new NewPage());
        }
    }
}
