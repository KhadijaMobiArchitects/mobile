using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.views.Base;
using XForms.ViewModels;

namespace XForms.views
{
    public partial class ComplaintAdministrationPage : BasePage
    {
        public ComplaintAdministrationPage()
        {
            InitializeComponent();
            BindingContext = new ComplaintAdministrationViewModel();
            
        }
    }
}
