using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Administration
{
    public partial class HomeAdminPage : BasePage
    {
        public HomeAdminPage()
        {
            InitializeComponent();

            BindingContext = new HomeAdminViewModel();
        }
    }
}
