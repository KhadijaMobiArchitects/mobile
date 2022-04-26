using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.ViewModels;
using XForms.Models;

namespace XForms.views
{
    public partial class ProfilProjectCellView : ContentView
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
BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(View), default, BindingMode.TwoWay);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        public static readonly BindableProperty ShowOwnerProperty =
BindableProperty.Create(nameof(ShowOwner), typeof(bool), typeof(View), false, BindingMode.TwoWay);

        public bool ShowOwner
        {
            get { return (bool)GetValue(ShowOwnerProperty); }
            set
            {
                SetValue(ShowOwnerProperty, value);
            }
        }

        public static readonly BindableProperty ShowCheckBoxProperty =
BindableProperty.Create(nameof(CheckBox), typeof(bool), typeof(View), false, BindingMode.TwoWay);

        public bool ShowCheckBox
        {
            get { return (bool)GetValue(ShowCheckBoxProperty); }
            set
            {
                SetValue(ShowCheckBoxProperty, value);
            }
        }


        public ProfilProjectCellView()
        {
            InitializeComponent();
        }

        void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        {
            if ((sender as View).BindingContext is Models.Leave item)
            {
                //(this.Parent.BindingContext as LeaveRequestViewModel).OpenProfilLeaveDetailsPopupView.Execute(item);
            }

        }
    }
}
