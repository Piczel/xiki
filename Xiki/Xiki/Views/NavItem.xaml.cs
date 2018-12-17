using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki.Views.Overlapping
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavItem : ContentView
	{

        private TapGestureRecognizer tapGestureRecognizer;

        public NavItem(string label, string sublabel, Action clicked)
		{
			InitializeComponent ();

            (FindByName("Label") as Label).Text = label;
            (FindByName("Sublabel") as Label).Text = sublabel;


            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Nav item clicked");
                clicked();
            };
            GestureRecognizers.Add(tapGestureRecognizer);
        }
	}
}