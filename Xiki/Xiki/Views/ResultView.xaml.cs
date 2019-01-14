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
	public partial class ResultView : ContentView
	{
        private StackLayout ResultContent;
		public ResultView ()
		{
			InitializeComponent ();

            ResultContent = (FindByName("Content") as StackLayout);

        }

        public void Add(View view)
        {
            ResultContent.Children.Add(view);
        }

        public void Clear()
        {
            ResultContent.Children.Clear();
        }
    }
}