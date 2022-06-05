using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views
{
    public partial class MyRequestsDisplacementPage : BasePage
    {
        public MyRequestsDisplacementPage()
        {
            InitializeComponent();
            BindingContext = new MyRequetsDisplacemntViewModel();
        }

    }
}
