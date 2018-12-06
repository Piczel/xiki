using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Xiki
{
	public class NewPage : ContentPage
	{
		public NewPage ()
		{
			Content = new StackLayout {
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}