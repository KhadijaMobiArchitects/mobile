using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XForms.ViewModels;
using XForms.views.Base;

namespace XForms.views.Project
{
    public partial class AddPointsPopup : BasePopupView
    {
        public AddPointsPopup()
        {
            InitializeComponent();
        }

        void Minus_Clicked(System.Object sender, System.EventArgs e)
        {
            if ((this.BindingContext as ProjectViewModel).MyNotes > 0)
                (this.BindingContext as ProjectViewModel).MyNotes--;

        }
        void Plus_Clicked(System.Object sender, System.EventArgs e)
        {
            (this.BindingContext as ProjectViewModel).MyNotes++;
        }
    }
}
