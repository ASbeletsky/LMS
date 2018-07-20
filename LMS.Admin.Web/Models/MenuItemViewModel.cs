using System.Collections.Generic;

namespace LMS.Admin.Web.Models
{
    public class MenuItemViewModel : List<MenuItemViewModel>
    {
        public MenuItemViewModel(string title, string icon, string action, string controller)
        {
            Title = title;
            Icon = icon;
            Action = action;
            Controller = controller;
        }

        public MenuItemViewModel(string title, string action, string controller)
            : this(title, null, action, controller)
        {

        }

        public MenuItemViewModel(string title, string icon)
            : this(title, icon, null, null)
        {

        }

        public string Title { get; }
        public string Icon { get; }
        public string Action { get; }
        public string Controller { get; }
    }
}
