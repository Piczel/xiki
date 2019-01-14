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
        private static ArticlePage instance;

        private TabView tabs;
        private ArticlePage()
        {
            InitializeComponent();
            tabs = TabView.GetInstance();
            (FindByName("HorizontalStack") as StackLayout).Children.Add(tabs);
            //SetArticleView(articleID);


            IList<View> buttons = (FindByName("Buttons") as FlexLayout).Children;
            buttons.Add(new ClickableIcon(Views.Icon.MENU, "Menu", () =>
            {
                MenuPage menu = new MenuPage();
                menu.AddItem(new NavItem("Settings", "Configure your app", delegate ()
                {
                    MenuPage settings = new MenuPage();
                    settings.AddItem(new NavItem("Set server IP", "Change the where the content is loaded from", delegate ()
                    {
                        Navigation.PushModalAsync(new PromptPage(
                            "Set server IP",
                            App.Host,
                            delegate (string input)
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
                            delegate (string input)
                            {
                                try
                                {
                                    App.WikiID = int.Parse(input);

                                }
                                catch (FormatException exc)
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
            buttons.Add(new ClickableIcon(Views.Icon.BOOKMARKS, "Saved", () =>
            {

            }));
            buttons.Add(new ClickableIcon(Views.Icon.SEARCH, "Find", () =>
            {
                Navigation.PushAsync(new Find());
            }));



            // DisplayAlert("Message", "Page loaded, ID: " + ArticleID, "OK");
        }

        public static ArticlePage GetInstance()
        {
            if (instance == null)
                instance = new ArticlePage();

            return instance;
        }

        public static void SetArticleView(ArticleView article)
        {
            ScrollView scroll = (GetInstance().FindByName("ArticleViewport") as ScrollView);
            if (scroll.Content != null)
            {
                (scroll.Content as ArticleView).ScrollPos = scroll.ScrollY;
            }
            scroll.Content = article;
            scroll.ScrollToAsync(0, article.ScrollPos, false);
        }

        public static ArticleView GetArticleView()
        {
            return (GetInstance().FindByName("ArticleViewport") as ScrollView).Content as ArticleView;
        }
    }
}