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

                var Article = new ArticleLinkItem("Article"+i,"Subtitlehehhe");
            
                Articles.Children.Add(Article);
                        
            }

        }


    }


}
