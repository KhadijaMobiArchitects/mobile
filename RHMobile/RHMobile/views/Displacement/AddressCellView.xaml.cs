using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.views
{
    public partial class AddressCellView : StackLayout
    {

        public static readonly BindableProperty AddressProperty =
BindableProperty.Create(nameof(Address), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set
            {
                SetValue(AddressProperty, value);
            }
        }


        public AddressCellView()
        {
            InitializeComponent();
        }
    }
}
