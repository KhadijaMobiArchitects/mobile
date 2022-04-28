using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class NewProjectPage : BasePage
    {
        public NewProjectPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
                BindingContext = new NewProjectViewModel();


            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    var isHasNotchScreen = AppHelpers.CheckHasNotchScreen();

            //    MyHeader.Padding = isHasNotchScreen ? new Thickness(30, 40, 30, 20) : new Thickness(30, 30, 30, 20);
            //}         
        }
    }
}
