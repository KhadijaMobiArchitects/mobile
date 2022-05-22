using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class NewCertaficateRequestPage : BasePage
    {
        public NewCertaficateRequestPage()
        {
            InitializeComponent();

            BindingContext = new NewCertaficateRequestViewModel();
        }
    }
}
