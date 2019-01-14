using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Pages;

namespace Xiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TagView : ContentView
	{
        private TapGestureRecognizer tapGestureRecognizer;
        private string Tag;
        public TagView (string name)
		{
			InitializeComponent ();
            Margin = new Thickness(0, 0, 6, 0);
            this.Tag = name;
            (FindByName("TagName") as Label).Text = " " + name + " ";

            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                Clicked();
            };
            GestureRecognizers.Add(tapGestureRecognizer);

        }

        private async void Clicked()
        {
            await Navigation.PushAsync(new TagResultPage(Tag));
        }


	}
}