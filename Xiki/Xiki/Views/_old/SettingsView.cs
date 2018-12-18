using System;
using System.Collections.Generic;
using System.Text;

namespace Xiki.Views.Overlapping
{
    class SettingsView : MenuView
    {

        public SettingsView()
        {
            AddItem(new NavItem("Set server IP", "Choose what server to fetch content from", () => { }));
            AddItem(new NavItem("Set server IP", "Choose what server to fetch content from", () => { }));
        }
    }
}
