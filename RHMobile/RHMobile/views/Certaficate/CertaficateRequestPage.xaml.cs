using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class CertaficateRequestPage : BasePage
    {
        public CertaficateRequestPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
                BindingContext = new CertaficateViewModel();
        }
    }
}
