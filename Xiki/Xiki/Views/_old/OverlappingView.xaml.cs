using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xiki.Views.Overlapping;

namespace Xiki.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OverlappingView : ContentView
	{

        private Rectangle defaultBounds;
        private bool open = false;

		public OverlappingView()
		{
			InitializeComponent();
            
            IList<View> buttons = (FindByName("Buttons") as FlexLayout).Children;
            buttons.Add(new ClickableIcon("Menu", () => {
                ToggleLayoutAsync(async delegate() {

                    MenuView menu = new MenuView();

                    menu.AddItem(new NavItem("Settings", "Configure your app", async delegate() {
                        await SetContent(new SettingsView());
                    }));

                    await SetContent(menu);
                });
            }));
            buttons.Add(new ClickableIcon("Saved", () => {
                ToggleLayoutAsync(() => {

                });
            }));
            buttons.Add(new ClickableIcon("Find", () => {
                ToggleLayoutAsync(() => {

                });
            }));


            BackgroundColor = Color.WhiteSmoke;
        }

        private async Task<bool> SetContent(View view)
        {
            await ClearAsync();
            StackLayout viewport = FindByName("ContentViewport") as StackLayout;
            viewport.Children.Add(view);
            await viewport.FadeTo(1, 250, Easing.CubicIn);
            return true;
        }

        private async Task<bool> ClearAsync()
        {
            StackLayout viewport = FindByName("ContentViewport") as StackLayout;

            bool x = await viewport.FadeTo(0, 250, Easing.CubicOut);

            if (x || !x)
            {

                viewport.Children.Clear();
            }

            return true;
        }

        private async Task<bool> ClosedAsync()
        {
            await ClearAsync();
            return true;
        }

        private async void ToggleLayoutAsync(Action onOpen)
        {

            if(open)
            {
                this.LayoutTo(defaultBounds, 500, Easing.CubicInOut);
                ClosedAsync();

            } else
            {
                defaultBounds = new Rectangle(this.Bounds.X, this.Bounds.Y, this.Bounds.Width, this.Bounds.Height);


                bool x = await this.LayoutTo(
                    new Rectangle(
                        this.Bounds.X,
                        this.Bounds.Y * 0.2,
                        this.Bounds.Width,
                        500
                    ),
                    500,
                    Easing.CubicInOut
                );

                if(x || !x)
                {
                    onOpen();
                }
            }

            open = !open;
        }
	}

}