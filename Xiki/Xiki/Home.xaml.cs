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
        public Home()
        {
            InitializeComponent();
            for (int i = 0; i < 5; i++)
            {

                var Article = new ArticleLinkItem("Article"+i,"Subtitlehehhe", 0);
            
                Articles.Children.Add(Article);
                        
            }

        }
        private async void GetArticles()
        {
            JObject Response = await HttpUtil.PostAsync("~theprovider/wiki/php/get-articles.php", new
            {
                wikiID = 6
            });
        }

        public async void SearchAsync(object sender, EventArgs e)
        {

        }
    }


}
