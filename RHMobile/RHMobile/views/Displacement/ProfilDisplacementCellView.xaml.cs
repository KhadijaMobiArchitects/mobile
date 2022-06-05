using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.views
{
    public partial class ProfilDisplacementCellView : ContentView
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
        public ProfilDisplacementCellView()
        {
            InitializeComponent();
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
