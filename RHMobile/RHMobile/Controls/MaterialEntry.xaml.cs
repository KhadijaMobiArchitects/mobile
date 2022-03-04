using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace XForms.Controls
{
    public partial class MaterialEntry : StackLayout
    {
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public static readonly BindableProperty PlaceholderProperty =
    BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        public static readonly BindableProperty CornerRadiussProperty =
                BindableProperty.Create(nameof(CornerRadiuss), typeof(CornerRadius), typeof(View),default , BindingMode.TwoWay);

        public CornerRadius CornerRadiuss
        {
            get { return (CornerRadius)GetValue(CornerRadiussProperty); }
            set
            {
                SetValue(CornerRadiussProperty, value);
            }
        }


        public MaterialEntry()
        {
            InitializeComponent();
        }
    }
}
