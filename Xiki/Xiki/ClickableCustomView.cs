using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xiki
{
    class ClickableCustomView : FlexLayout
    {

        private TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        public ClickableCustomView(Action action)
        {
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Image Clicked w/ Lambda");
                action();

            };

            GestureRecognizers.Add(tapGestureRecognizer);
        }
    }
}
