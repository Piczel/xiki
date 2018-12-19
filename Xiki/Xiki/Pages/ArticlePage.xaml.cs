using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Pages;
using Xiki.Views;
using Xiki.Views.Overlapping;

namespace Xiki.Article
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ArticlePage : ContentPage
	{
        private TabView tabs;
		public ArticlePage (int articleID)
		{
			InitializeComponent();
            tabs = new TabView(this);
            (FindByName("HorizontalStack") as StackLayout).Children.Add(tabs);
            setArticleView(articleID);
            

            IList<View> buttons = (FindByName("Buttons") as FlexLayout).Children;
            buttons.Add(new ClickableIcon("Menu", () => {
                MenuPage menu = new MenuPage();
                menu.AddItem(new NavItem("Settings", "Configure your app", delegate ()
                {
                    MenuPage settings = new MenuPage();
                    settings.AddItem(new NavItem("Set server IP", "Change the where the content is loaded from", delegate ()
                    {
                        Navigation.PushModalAsync(new PromptPage(
                            "Set server IP",
                            App.Host,
                            delegate(string input)
                            {
                                App.Host = input;
                            }
                        ));
                    }));
                    settings.AddItem(new NavItem("Set wiki ID", "Update wiki ID reference", delegate ()
                    {
                        Navigation.PushModalAsync(new PromptPage(
                            "Set wiki ID",
                            "" + App.WikiID,
                            delegate(string input)
                            {
                                try
                                {
                                    App.WikiID = int.Parse(input);

                                } catch(FormatException exc)
                                {
                                    DisplayAlert("Error", "Not a valid integer", "OK");
                                }
                            }
                        ));
                    }));

                    Navigation.PushAsync(settings);
                }));

                Navigation.PushAsync(menu);
            }));
            buttons.Add(new ClickableIcon("Saved", () => {
                
            }));
            buttons.Add(new ClickableIcon("Find", () => {
                Navigation.PushAsync(new Find(this));
            }));
            
            

            // DisplayAlert("Message", "Page loaded, ID: " + ArticleID, "OK");
        }
        public void setArticleView (int articleID)
        {

            // Creates a new view and loads its content

            (FindByName("ArticleViewport") as ScrollView).Content = tabs.OpenTab(articleID);
        }
        
        public void setArticleView(Tab tab)
        {
            // Sets the view from clicked tab


            tabs.SetActive(tab);
            (FindByName("ArticleViewport") as ScrollView).Content = tab.GetArticleView();
        }
      
    }
}