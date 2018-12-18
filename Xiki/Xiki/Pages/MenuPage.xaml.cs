using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
        private StackLayout Items;

        public MenuPage ()
		{
			InitializeComponent ();

            Items = FindByName("NavItems") as StackLayout;
        }

        public void AddItem(View view)
        {
            Items.Children.Add(view);
            Items.Children.Add(new BoxView
            {
                BackgroundColor = Color.FromHex("#464646"),
                HeightRequest = 1
                 
            });


        }
    }
}