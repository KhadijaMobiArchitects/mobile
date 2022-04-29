using System;
using System.Collections.Generic;
using XForms.views.Base;
using Xamarin.Forms;
using XForms.ViewModels;

namespace XForms.views.Authentication
{
    public partial class SigninPage : BasePage
    {
        public SigninPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext == null)
                BindingContext = new SigninViewModel();
        }
    }
}
