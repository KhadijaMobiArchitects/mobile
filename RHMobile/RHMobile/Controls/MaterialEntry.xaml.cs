using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace RHMobile.Controls
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

        public static readonly BindableProperty CornerRadiusProperty =
                BindableProperty.Create(nameof(cornerRadius), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string cornerRadius
        {
            get { return (string)GetValue(CornerRadiusProperty); }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }


        public MaterialEntry()
        {
            InitializeComponent();
        }
    }
}
