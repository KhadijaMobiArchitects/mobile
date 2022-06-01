using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class MaterialEntryGeolocalion : StackLayout
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

        public static readonly BindableProperty DateProperty =
BindableProperty.Create(nameof(Date), typeof(DateTime), typeof(View), null, BindingMode.TwoWay);

        public DateTime? Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set
            {
                SetValue(DateProperty, value);
            }
        }

        //        public static readonly BindableProperty FocusProperty =
        //BindableProperty.Create(nameof(Focus), typeof(string), typeof(View), string.Empty, BindingMode.TwoWay);

        public bool IsFocused
        {
            get { return entry.IsFocused; }
            set
            {

            }
        }

        private bool IsSelecetedDate;

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

        public static readonly BindableProperty IsIconClickedProperty =
BindableProperty.Create(nameof(IsIconClicked), typeof(bool), typeof(View), false, BindingMode.TwoWay);

        public bool IsIconClicked
        {
            get { return (bool)GetValue(IsIconClickedProperty); }
            set
            {
                SetValue(IsIconClickedProperty, value);
            }
        }

        public Color IconColor { get; set; }

        public MaterialEntryGeolocalion()
        {
            InitializeComponent();
            IconColor = AppHelpers.LookupColor("PlaceholderColor");
            //entry.PropertyChanged += (object sender, System.ComponentModel.PropertyChangedEventArgs e)=>
            //{
            //    if(e.PropertyName == nameof(IsFocused))
            //    {
            //        IsIconClicked = !IsIconClicked;
            //    }
            //};
        }

        void CustomButton_Clicked(System.Object sender, System.EventArgs e)
        {
            IsIconClicked = !IsIconClicked;
            IconColor = IsIconClicked ? AppHelpers.LookupColor("Primary") : AppHelpers.LookupColor("PlaceholderColor");

        }
    }
}
