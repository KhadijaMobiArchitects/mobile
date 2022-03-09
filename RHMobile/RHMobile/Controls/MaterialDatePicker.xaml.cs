using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class MaterialDatePicker : StackLayout
    {
        public static readonly BindableProperty TitleProperty =
BindableProperty.Create(nameof(Title), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public MaterialDatePicker()
        {
            InitializeComponent();
        }
    }
}
