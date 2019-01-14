using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClickableIcon : ContentView
	{

        private TapGestureRecognizer tapGestureRecognizer;

        public ClickableIcon(string icon, string label, Action clicked)
		{
			InitializeComponent();

            (FindByName("IconLabel") as Label).Text = label;
            (FindByName("Image") as Image).Source = ImageSource.FromFile(icon);
            

            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");
                clicked();
            };
            GestureRecognizers.Add(tapGestureRecognizer);
        }

	}

    public class Icon
    {
        public static readonly string ARROW_LEFT = "sharp_navigate_before_black_18dp.png";
        public static readonly string ARROW_RIGHT = "sharp_navigate_next_black_18dp.png";
        public static readonly string BOOKMARK_EMPTY = "sharp_bookmark_border_black_18dp.png";
        public static readonly string BOOKMARK_FILLED = "sharp_bookmark_black_18dp.png";
        public static readonly string BOOKMARKS = "sharp_bookmarks_black_18dp.png";
        public static readonly string MENU = "sharp_menu_black_18dp.png";
        public static readonly string EDIT = "sharp_edit_black_18dp.png";
        public static readonly string SEARCH = "sharp_search_black_18dp.png";
        
    }
}