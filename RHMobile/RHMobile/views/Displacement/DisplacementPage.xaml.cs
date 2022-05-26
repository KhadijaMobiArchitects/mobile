using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class DisplacementPage : BasePage
    {
        public DisplacementPage()
        {
            InitializeComponent();
            BindingContext = new DisplacementViewModel();
        }
    }
}
