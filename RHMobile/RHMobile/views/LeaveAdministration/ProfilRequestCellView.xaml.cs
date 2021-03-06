using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.ViewModels;
using XForms.Models;

namespace XForms.views
{
    public partial class ProfilRequestCellView : ContentView
    {

        public static readonly BindableProperty BackgroundColorButtonProperty =
BindableProperty.Create(nameof(BackgroundColorButton), typeof(Color), typeof(View), Color.Yellow, BindingMode.TwoWay);

        public Color BackgroundColorButton
        {
            get { return (Color)GetValue(BackgroundColorButtonProperty); }
            set
            {
                SetValue(BackgroundColorButtonProperty, value);
            }
        }

        public static readonly BindableProperty TextColorProperty =
BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(View), Color.Yellow, BindingMode.TwoWay);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public ProfilRequestCellView()
        {
            InitializeComponent();
        }

        //void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    if ((sender as View).BindingContext is LeaveResponse item)
        //    {
        //        (this.Parent.BindingContext as LeaveAdministrationViewModel).OpenProfilLeaveDetailsPopupView.Execute(item);
        //    }

        //}

    }
}
