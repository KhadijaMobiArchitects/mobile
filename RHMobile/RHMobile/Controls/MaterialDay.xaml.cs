using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class MaterialDay : StackLayout
    {
        public static readonly BindableProperty StatusColorProperty =
    BindableProperty.Create(nameof(StatusColor), typeof(Color), typeof(View),Color.Green, BindingMode.TwoWay);

        public Color StatusColor
        {
            get { return (Color)GetValue(StatusColorProperty); }
            set
            {
                SetValue(StatusColorProperty, value);
            }
        }

        public static readonly BindableProperty NumberOfDaysProperty =
BindableProperty.Create(nameof(NumberOfDays), typeof(int), typeof(View),0, BindingMode.TwoWay);

        public int NumberOfDays
        {
            get { return (int)GetValue(NumberOfDaysProperty); }
            set
            {
                SetValue(NumberOfDaysProperty, value);
            }
        }
        public static readonly BindableProperty StatusDaysProperty =
            BindableProperty.Create(nameof(StatusDays), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string StatusDays
        {
            get { return (string)GetValue(StatusDaysProperty); }
            set
            {
                SetValue(StatusDaysProperty, value);
            }
        }


        public MaterialDay()
        {
            InitializeComponent();
        }
    }
}
