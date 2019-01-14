using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;
using Xiki.Views;

namespace Xiki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Find : ContentPage
	{
        private ResultView resultView;

		public Find ()
		{
            InitializeComponent();

            resultView = new ResultView();
            resultView.Add(new Label
            {
                Text = "Search for articles",
                HorizontalTextAlignment = TextAlignment.Center
            });
            (FindByName("MainLayout") as StackLayout).Children.Add(resultView);
		}

        public async void SearchAsync(object sender, EventArgs e)
        {
            try
            {
                JObject response = await HttpUtil.PostAsync(App.GET_ARTICLES, new
                {
                    wikiID = App.WikiID,
                    search = (FindByName("Search") as Entry).Text
                });

                resultView.Clear();

                JArray articles = (JArray)response["articles"];
                int count = articles.Count;

                resultView.Add(new Label
                {
                    Text =  count + " article(s) found",
                    HorizontalTextAlignment = TextAlignment.Center
                });

                for(int i = 0; i < count; i++)
                {
                    JObject article = (JObject)articles[i];

                    string subtitle = "Subtitle"; // ((string)((JObject)((JArray)((JObject)article["content"])["data"])[0])["text"]).Substring(0, 20) + "...";

                    resultView.Add(new ArticleLinkItem(
                        (string) article["title"],
                        subtitle,
                        (int)article["articleID"]
                    ));
                }

            } catch(Exception exc)
            {
                await DisplayAlert("Error", exc.ToString(), "OK");
            }


        }

	}
}