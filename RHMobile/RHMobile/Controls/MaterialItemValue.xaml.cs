using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class MaterialItemValue : StackLayout
    {

        public static readonly BindableProperty ItemProperty =
 BindableProperty.Create(nameof(Item), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Item
        {
            get { return (string)GetValue(ItemProperty); }
            set
            {
                SetValue(ItemProperty, value);
            }
        }

        public static readonly BindableProperty ValueProperty =
         BindableProperty.Create(nameof(Value), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }


        public MaterialItemValue()
        {
            InitializeComponent();
        }
    }
}
