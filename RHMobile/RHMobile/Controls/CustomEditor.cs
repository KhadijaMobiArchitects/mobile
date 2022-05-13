using XForms.Enums;
using System;
using Xamarin.Forms;

namespace XForms.Controls
{
    public class CustomEditor : Editor
    {
        private ControlStyleCorner styleCorner = ControlStyleCorner.Circle;
        public ControlStyleCorner StyleCorner
        {
            get { return styleCorner; }
            set { styleCorner = value; }
        }

        public static readonly BindableProperty IsCustomProperty =
           BindableProperty.Create(nameof(IsCustom), typeof(bool), typeof(CustomEditor), false, BindingMode.TwoWay);

        public bool IsCustom
        {
            get { return (bool)GetValue(IsCustomProperty); }
            set
            {
                SetValue(IsCustomProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty =
           BindableProperty.Create(nameof(BorderColor), typeof(Xamarin.Forms.Color), typeof(CustomEditor), Xamarin.Forms.Color.Gray, BindingMode.TwoWay);
        public Xamarin.Forms.Color BorderColor
        {
            get { return (Xamarin.Forms.Color)GetValue(BorderColorProperty); }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }

        public static readonly BindableProperty IsHasBorderProperty =
          BindableProperty.Create(nameof(IsHasBorder), typeof(bool), typeof(CustomEditor), true, BindingMode.TwoWay);

        public bool IsHasBorder
        {
            get { return (bool)GetValue(IsHasBorderProperty); }
            set
            {
                SetValue(IsHasBorderProperty, value);
            }
        }

        public static readonly BindableProperty ErrorTextProperty =
        BindableProperty.Create(nameof(ErrorText), typeof(string), typeof(CustomEditor), string.Empty);

        public string ErrorText
        {
            get { return (string)GetValue(ErrorTextProperty); }
            set
            {
                SetValue(ErrorTextProperty, value);
            }
        }

        public static readonly BindableProperty IsBorderErrorVisibleProperty =
            BindableProperty.Create(nameof(IsBorderErrorVisible), typeof(bool), typeof(CustomEditor), false, BindingMode.TwoWay);

        public bool IsBorderErrorVisible
        {
            get { return (bool)GetValue(IsBorderErrorVisibleProperty); }
            set
            {
                SetValue(IsBorderErrorVisibleProperty, value);
            }
        }

        public static readonly BindableProperty CornerRaduisProperty =
     BindableProperty.Create(nameof(CornerRaduis), typeof(int), typeof(CustomEditor), 10);

        public int CornerRaduis
        {
            get { return (int)GetValue(CornerRaduisProperty); }
            set
            {
                SetValue(CornerRaduisProperty, value);
            }
        }

        public static readonly BindableProperty BorderWidthProperty =
     BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(CustomEditor), 1);

        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set
            {
                SetValue(BorderWidthProperty, value);
            }
        }

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
    BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(CustomEditor), TextAlignment.Start);

        public TextAlignment HorizontalTextAlignment
        {

            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set
            {
                SetValue(HorizontalTextAlignmentProperty, value);
            }
        }

        public static readonly BindableProperty PaddingProperty =
  BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomEditor), new Thickness(15, 15));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set
            {
                SetValue(PaddingProperty, value);
            }
        }

        public static readonly BindableProperty GlyphProperty =
           BindableProperty.Create(nameof(Glyph), typeof(string), typeof(CustomEditor), string.Empty);
        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }


        public static readonly BindableProperty GlyphFontSizeProperty =
           BindableProperty.Create(nameof(GlyphFontSize), typeof(int), typeof(CustomEditor), 40);

        public int GlyphFontSize
        {
            get { return (int)GetValue(GlyphFontSizeProperty); }
            set { SetValue(GlyphFontSizeProperty, value); }
        }

        public static readonly BindableProperty ImageProperty =
    BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomEditor), string.Empty);

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Xamarin.Forms.Color), typeof(CustomEditor), Color.White);

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomEditor), 20);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomEditor), 20);

        //public static readonly BindableProperty ImageAlignmentProperty =
        //    BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(CustomEditor), ImageAlignment.Left);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        //public ImageAlignment ImageAlignment
        //{
        //    get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
        //    set { SetValue(ImageAlignmentProperty, value); }
        //}

        public CustomEditor()
        {

            //this.SizeChanged += (object sender, EventArgs e) =>
            //{
            //    switch (StyleCorner)
            //    {
            //        case ControlStyleCorner.Circle:
            //            break;
            //    };
            //};
        }
    }
}