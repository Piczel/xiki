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
	public partial class PromptPage : ContentPage
	{

        private Action<string> onConfirm;
		public PromptPage(string label, string placeholder, Action<string> onConfirm)
		{
			InitializeComponent ();

            (FindByName("Label") as Label).Text = label;
            (FindByName("Input") as Entry).Placeholder = placeholder;

            this.onConfirm = onConfirm;
		}


        public async void ClickedAsync(object sender, EventArgs args)
        {
            string retval = (FindByName("Input") as Entry).Text;

            if (retval == null) retval = (FindByName("Input") as Entry).Placeholder;
            if (retval.Length < 1) retval = (FindByName("Input") as Entry).Placeholder;

            onConfirm(retval);

            await Navigation.PopModalAsync();
        }
	}
}