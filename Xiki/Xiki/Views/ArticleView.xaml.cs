using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Article;

namespace Xiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticleView : ContentView
	{

        private int ArticleID;
        
        private StackLayout ArticleElements;

        private string Title;
        private Tab tab;

        public ArticleView (Tab tab, int articleID)
		{
			InitializeComponent ();
           
            this.tab = tab;
            Title = "";
            this.ArticleID = articleID;

            ArticleElements = this.FindByName("ArticleContent") as StackLayout;

            LoadArticleAsync();               
        }

        public string GetTitle()
        {
            return Title;        
        }

        public async Task<bool> FadeIn(uint duration = 500)
        {
            return await ArticleElements.FadeTo(1, duration);
        }

        public async Task<bool> FadeOut(uint duration = 500)
        {
            return await ArticleElements.FadeTo(0, duration);
        }

        private async void LoadArticleAsync()
        {
            try
            {
                JObject response = await HttpUtil.PostAsync("~theprovider/wiki/php/get-articles.php", new
                {
                    wikiID = App.WikiID,
                    articleID = this.ArticleID
            });
                
                JArray articles = (JArray)response["articles"];

                if (articles.Count < 1)
                {
                    throw new Exception("Article not found");
                }

                LoadElements((JObject)articles[0]);
                

            }
            catch (Exception exc)
            {
              /*  await DisplayAlert("Error!", exc.ToString(), "OK"); */
            }
        }

        private void LoadElements(JObject article)
        {
            
            JObject content = JObject.Parse((string)article["content"]);
            JArray data = (JArray)content["data"];

            this.Title = (string)article["title"];
            tab.SetArticleView(this);
            (FindByName("PageTitle") as Label).Text = this.Title;
            
            JArray tags = (JArray)article["tags"];

            for(int i = 0; i < tags.Count; i++)
            {
                string tag = (string) ((JObject) tags[i])["name"];

                (FindByName("Tags") as FlexLayout).Children.Add(new TagView(tag));
            }

    
            for (int i = 0; i < data.Count; i++)
            {
                JObject section = (JObject)data[i];

                switch ((string)section["type"])
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
                if (chars[i] == '[')
                {

                    str.Spans.Add(new Span
                    {
                        Text = text.Substring(start, i - start)
                    });

                    i++;
                    switch (chars[i])
                    {
                        case 'a':

                            while (text.Substring(i, 5) != "ref='")
                            {
                                i++;
                            }
                            i += 5;

                            int refStart = i;

                            while (chars[i] != '\'')
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


                            while (chars[i] != '[')
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
                                ArticlePage.SetArticleView(articleID);
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

            Image image = new Image
            {
                Source = ImageSource.FromUri(new Uri((string)i["url"])),
                Style = Resources["Image"] as Style
            };

            ArticleElements.Children.Add(image);

            Label text = ParseParagraphLabel((string)i["text"]);
            text.Style = Resources["ImageP"] as Style;

            ArticleElements.Children.Add(text);
        }
    }
}