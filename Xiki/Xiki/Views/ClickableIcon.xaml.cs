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
	public partial class ClickableIcon : ContentView
	{

        private TapGestureRecognizer tapGestureRecognizer;

        public ClickableIcon(string label, Action clicked)
		{
			InitializeComponent();

            (FindByName("IconLabel") as Label).Text = label;
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");
                clicked();
            };
            GestureRecognizers.Add(tapGestureRecognizer);
        }

	}
}