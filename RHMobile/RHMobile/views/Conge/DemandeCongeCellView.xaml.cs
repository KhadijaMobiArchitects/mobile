using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.views.Conge
{
    public partial class DemandeCongeCellView : ContentView
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

        public DemandeCongeCellView()
        {
            InitializeComponent();
        }
    }
}
