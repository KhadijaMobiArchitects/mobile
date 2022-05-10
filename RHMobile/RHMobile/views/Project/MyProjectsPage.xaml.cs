using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class MyProjectsPage : BasePage
    {
        public MyProjectsPage()
        {
            InitializeComponent();

            BindingContext = new ProjectViewModel();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
    }
}
