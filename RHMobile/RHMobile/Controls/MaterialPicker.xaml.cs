using System;
using System.Collections;
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

        public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(View), null, BindingMode.TwoWay);

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set
            {

                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly BindableProperty SelectedIndexProperty =
   BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(View), default, BindingMode.TwoWay);

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        public static readonly BindableProperty SelectedItemProperty =
   BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(View), default, BindingMode.TwoWay);

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

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



        public MaterialPicker()
        {
            InitializeComponent();
        }
    }
}
