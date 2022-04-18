using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class ProjectPage : BasePage
    {
        public ProjectPage()
        {
            InitializeComponent();

            BindingContext = new ProjectViewModel();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.iOS)
            {
                var isHasNotchScreen = AppHelpers.CheckHasNotchScreen();

                MyHeader.Padding = isHasNotchScreen ? new Thickness(30, 40, 30, 0) : new Thickness(30, 30, 30, 0);
            }
        }
    }
}
