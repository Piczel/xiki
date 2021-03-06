﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;

namespace Xiki
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Find : ContentPage
	{

        private ArticlePage page;
		public Find (ArticlePage page)
		{
            InitializeComponent();
            this.page = page;
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

                StackLayout container = FindByName("Articles") as StackLayout;

                container.Children.Clear();

                JArray articles = (JArray)response["articles"];
                int count = articles.Count;

                container.Children.Add(new Label
                {
                    Text =  count + " article(s) found",
                    HorizontalTextAlignment = TextAlignment.Center
                });

                for(int i = 0; i < count; i++)
                {
                    JObject article = (JObject)articles[i];

                    string subtitle = "Subtitle"; // ((string)((JObject)((JArray)((JObject)article["content"])["data"])[0])["text"]).Substring(0, 20) + "...";

                    container.Children.Add(new ArticleLinkItem(
                        page,
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