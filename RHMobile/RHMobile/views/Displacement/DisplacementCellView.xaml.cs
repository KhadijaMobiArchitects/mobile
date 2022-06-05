using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.ViewModels;
using XForms.Models;

namespace XForms.views
{
    public partial class DisplacementRequestCellView : ContentView
    {

        public static readonly BindableProperty BackgroundColorButtonProperty =
BindableProperty.Create(nameof(BackgroundColorButton), typeof(Color), typeof(View), default, BindingMode.TwoWay);

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

        public DisplacementRequestCellView()
        {
            InitializeComponent();
        }

        void SelectItem_Clicked(System.Object sender, System.EventArgs e)
        {
            if ((sender as View).BindingContext is Models.LeaveModel item)
            {
                //(this.Parent.BindingContext as LeaveRequestViewModel).OpenLeaveDetailsPopupView.Execute(item);
            }

        }
    }
}
