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

        }
    }
}
