using Newtonsoft.Json.Linq;
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
	public partial class UpdateWiki : ContentPage
	{

        private int accountID;
        private string token;
        private int wikiID;

		public UpdateWiki(
            int accountID,
            string token,
            int wikiID,
            string name,
            string description
        ) {
			InitializeComponent();

            this.accountID = accountID;
            this.token = token;

            this.wikiID = wikiID;

            entryName.Placeholder = name;
            editorDescription.Placeholder = description;


            DisplayAlert("Wiki", "Name: "+ name +"\nDescription"+ description, "OK");
		}

        private void Save_Button(object sender, EventArgs e)
        {
            SaveAsync(
                entryName.Text,
                editorDescription.Text
            );
        }

        private async void SaveAsync(
            string name,
            string description
        )
        {
            try
            {
                JObject response = await HttpUtil.PostAsync("~theprovider/wiki/php/update-wiki.php", new { accountID = accountID, token = token, wikiID = wikiID, name = name, description = description});
                if (!(bool)response["status"]) throw new Exception((string)response["message"]);

                await DisplayAlert("Lyckades", (string)response["message"], "OK");

                await Navigation.PopAsync();

            } catch(Exception e)
            {
                await DisplayAlert("Fel", e.Message, "OK");
            }
        }
    }
}