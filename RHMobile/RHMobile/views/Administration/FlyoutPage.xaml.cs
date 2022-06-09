using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.views.Base;

namespace XForms.views
{
    public partial class FlyoutPage : BasePopupView
    {
        public FlyoutPage()
        {
            InitializeComponent();
        }

        void Switch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            AppPreferences.EnableFaceID = !AppPreferences.EnableFaceID;
        }
    }
}
