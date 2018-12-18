using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuView : ContentView
	{
        private StackLayout Items;
		public MenuView ()
		{
			InitializeComponent ();

            Items = FindByName("NavItems") as StackLayout;
		}

        public void AddItem(View view)
        {
            Items.Children.Add(view);
        }
	}
}