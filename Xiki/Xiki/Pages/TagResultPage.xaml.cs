using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Views;

namespace Xiki.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TagResultPage : ContentPage
	{
        private ResultView resultView;
        private string tagName;
		public TagResultPage (string tagName)
		{
			InitializeComponent ();
            resultView = new ResultView();
            resultView.Add(new Label
            {
                Text = "Displaying articles tagged with: " + tagName,
                HorizontalTextAlignment = TextAlignment.Center
            });
            (FindByName("MainLayout") as StackLayout).Children.Add(resultView);
            this.tagName = tagName;
            SearchAsync(tagName);
		}

        public async void SearchAsync(string tagName)
        {
            try
            {
                JObject response = await HttpUtil.PostAsync(App.GET_ARTICLES, new
                {
                    wikiID = App.WikiID,
                    tags = new [] {tagName}
                });

                resultView.Clear();

                JArray articles = (JArray)response["articles"];
                int count = articles.Count;

                resultView.Add(new Label
                {
                    Text = count + " article(s) found with tag: " + tagName,
                    HorizontalTextAlignment = TextAlignment.Center
                });

                for (int i = 0; i < count; i++)
                {
                    JObject article = (JObject)articles[i];

                    string subtitle = "Subtitle"; // ((string)((JObject)((JArray)((JObject)article["content"])["data"])[0])["text"]).Substring(0, 20) + "...";

                    resultView.Add(new ArticleLinkItem(
                        (string)article["title"],
                        subtitle,
                        (int)article["articleID"]
                    ));
                }

            }
            catch (Exception exc)
            {
                await DisplayAlert("Error", exc.ToString(), "OK");
            }


        }
    }


}