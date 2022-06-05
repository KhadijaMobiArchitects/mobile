using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class DisplacementAdministrationPage : BasePage
    {
        public DisplacementAdministrationPage()
        {
            InitializeComponent();
            BindingContext = new DisplacementAdministrationViewModel();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //if(BindingContext == null)
            //    BindingContext = new DisplacementAdministrationViewModel();

        }
    }
}
