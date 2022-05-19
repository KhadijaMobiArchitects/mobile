using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class ComplaintPage : BasePage
    {
        public ComplaintPage()
        {
            InitializeComponent();
            try
            {
                BindingContext = new ComplaintViewModel();

            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext == null)
                BindingContext = new ComplaintViewModel();
        }
    }
}
