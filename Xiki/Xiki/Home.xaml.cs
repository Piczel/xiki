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
	public partial class Home : ContentPage
	{
		public Home ()
		{
			InitializeComponent ();
		}

        private void Signin_Button(object sender, EventArgs e)
        {
            SigninAsync(
                entryPassword.Text,
                entryUsername.Text
            );
        }

        private async void SigninAsync(
            string username,
            string password
        )
        {
            var host = Application.Current.Resources["Host"];
            try
            {
                JObject response = await HttpUtil.PostAsync("~theprovider/generate-token.php", new { username = username, password = password });
                if (!(bool)response["status"]) throw new Exception((string)response["message"]);

                int accountID = (int)response["accountID"];
                string token = (string)response["token"];

                await DisplayAlert("Lyckades", "Du loggades in", "OK");

                JObject wikiResponse = await HttpUtil.PostAsync("~theprovider/wiki/php/get-wiki.php", new { accountID = accountID, token = token });
                if (!(bool)wikiResponse["status"]) throw new Exception((string)wikiResponse["message"]);

                JObject wiki = (JObject)wikiResponse["wiki"];



                await Navigation.PushAsync(new UpdateWiki(
                    accountID,
                    token,
                    (int)wiki["wikiID"],
                    (string)wiki["name"],
                    (string)wiki["description"]
                ));
            }
            catch (Exception e)
            {

                await DisplayAlert("Fel", e.Message, "OK");
            }

        }
    }
}