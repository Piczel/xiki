using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki.Article
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlePage : ContentPage
	{

        private int ArticleID;

        private StackLayout ArticleElements;

		public ArticlePage (int articleID)
		{
			InitializeComponent();

            this.ArticleID = articleID;

            ArticleElements = this.FindByName("ArticleContent") as StackLayout;

            LoadArticleAsync();

            // DisplayAlert("Message", "Page loaded, ID: " + ArticleID, "OK");
		}

        private async void LoadArticleAsync()
        {
            try
            {
                JObject response = await HttpUtil.PostAsync("~theprovider/wiki/php/get-articles.php", new {
                    wikiID = App.WikiID,
                    articleID = this.ArticleID
                });



                JArray articles = (JArray)response["articles"];

                if(articles.Count < 1)
                {
                    throw new Exception("Article not found");
                }

                LoadElements((JObject)articles[0]);

            } catch(Exception exc)
            {
                await DisplayAlert("Error!", exc.ToString(), "OK");
            }
        }

        private void LoadElements(JObject article)
        {
            JObject content = JObject.Parse((string)article["content"]);
            JArray data = (JArray)content["data"];

            (FindByName("PageTitle") as Label).Text = (string)article["title"];

            for(int i = 0; i < data.Count; i++)
            {
                JObject section = (JObject) data[i];

                switch((string)section["type"])
                {
                    case "heading":
                        AppendHeading(section);
                        break;
                    case "paragraph":
                        AppendParagraph(section);
                        break;
                    case "image":
                        AppendImage(section);
                        break;
                    
                }
            }
        }

        private void AppendHeading(JObject h)
        {
            

            Label heading = new Label();
            heading.Style = Resources[(string)h["style"]] as Style;
            heading.Text = (string)h["text"];

            ArticleElements.Children.Add(heading);
        }

        private void AppendParagraph(JObject p)
        {
            Label paragraph = ParseParagraphLabel((string)p["text"]);
            paragraph.Style = Resources[(string)p["style"]] as Style;

            ArticleElements.Children.Add(paragraph);
        }

        private Label ParseParagraphLabel(string text)
        {
            Label label = new Label();
            char[] chars = text.ToCharArray();

            FormattedString str = new FormattedString();

            int start = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if(chars[i] == '[')
                {
                        
                    str.Spans.Add(new Span
                    {
                        Text = text.Substring(start, i - start)
                    });

                    i++;
                    switch (chars[i])
                    {
                        case 'a':

                            while(text.Substring(i, 5) != "ref='")
                            {
                                i++;
                            }
                            i += 5;

                            int refStart = i;

                            while(chars[i] != '\'')
                            {
                                i++;
                            }
                            string num = text.Substring(refStart, i - refStart);
                            int articleID = int.Parse(num);

                            while (chars[i] != ']')
                            {
                                i++;
                            }
                            i++;

                            start = i;


                            while(chars[i] != '[')
                            {
                                i++;
                            }

                            var span = new Span
                            {
                                Text = text.Substring(start, i - start),
                                Style = Resources["Anchor"] as Style
                            };

                            TapGestureRecognizer tapListener = new TapGestureRecognizer();
                            tapListener.Tapped += (s, e) =>
                            {
                                Navigation.PushAsync(new ArticlePage(articleID));
                            };
                            span.GestureRecognizers.Add(tapListener);

                            str.Spans.Add(span);

                            i += 3;
                            start = i + 1;

                            break;
                        case 'b':

                            str.Spans.Add(new Span
                            {
                                Text = "\n"
                            });

                            i += 2;
                            start = i + 1;

                            break;
                    }
                }
            }
            str.Spans.Add(new Span
            {
                Text = text.Substring(start, text.Length - start)
            });


            label.FormattedText = str;

            return label;
        }

        private void AppendImage(JObject i)
        {

            Image image = new Image {
                Source = ImageSource.FromUri(new Uri((string)i["url"])),
                Style = Resources["Image"] as Style
            };

            ArticleElements.Children.Add(image);

            Label text = new Label
            {
                Style = Resources["ImageP"] as Style,
                Text = (string)i["text"]
            };

            ArticleElements.Children.Add(text);
        }
	}
}