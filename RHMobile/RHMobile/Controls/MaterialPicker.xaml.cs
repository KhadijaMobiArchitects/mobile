using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class MaterialPicker : StackLayout
    {
        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(string), typeof(View), String.Empty, BindingMode.TwoWay);

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty LeftGlyphProperty =
         BindableProperty.Create(nameof(LeftGlyph), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string LeftGlyph
        {
            get { return (string)GetValue(LeftGlyphProperty); }
            set
            {
                SetValue(LeftGlyphProperty, value);
            }
        }


        public MaterialPicker()
        {
            InitializeComponent();
        }
    }
}
