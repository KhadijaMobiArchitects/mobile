using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms;
using XForms.Enums;

namespace XForms.Controls
{
    public class CarouselIndicators : Grid
    {

        public static readonly BindableProperty SelectedDataTemplateProperty = BindableProperty.Create(nameof(Position), typeof(int), typeof(CarouselIndicators), 0, BindingMode.TwoWay, propertyChanging: PositionChanging);
        public DataTemplate SelectedDataTemplate
        {
            get { return (DataTemplate)this.GetValue(SelectedDataTemplateProperty); }
            set { this.SetValue(IndicatorWidthProperty, value); }
        }

        private Frame UnselectedBox;
        private Frame SelectedBox;

        private readonly StackLayout _indicators = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.CenterAndExpand };
        public CarouselIndicators()
        {
            _indicators.Spacing = IndicatorSpacing;

            this.HorizontalOptions = LayoutOptions.CenterAndExpand;
            this.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            this.Children.Add(_indicators);
        }

        public static readonly BindableProperty PositionProperty = BindableProperty.Create(nameof(Position), typeof(int), typeof(CarouselIndicators), 0, BindingMode.TwoWay, propertyChanging: PositionChanging);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CarouselIndicators), Enumerable.Empty<object>(), BindingMode.OneWay, propertyChanged: ItemsChanged);
        public static readonly BindableProperty StyleCornerProperty = BindableProperty.Create(nameof(StyleCorner), typeof(ControlStyleCorner), typeof(CarouselIndicators), ControlStyleCorner.Circle, BindingMode.OneWay);
        public static readonly BindableProperty IndicatorColorProperty = BindableProperty.Create(nameof(IndicatorColor), typeof(Color), typeof(CarouselIndicators), Color.White, BindingMode.OneWay);
        public static readonly BindableProperty IndicatorWidthProperty = BindableProperty.Create(nameof(IndicatorWidth), typeof(double), typeof(CarouselIndicators), 0.0, BindingMode.OneWay);
        public static readonly BindableProperty IndicatorHeightProperty = BindableProperty.Create(nameof(IndicatorHeight), typeof(double), typeof(CarouselIndicators), 0.0, BindingMode.OneWay);
        public static readonly BindableProperty IndicatorCornerRaduisProperty = BindableProperty.Create(nameof(IndicatorCornerRaduis), typeof(double), typeof(CarouselIndicators), 0.0, BindingMode.OneWay);
        public static readonly BindableProperty IndicatorSpacingProperty = BindableProperty.Create(nameof(IndicatorSpacing), typeof(int), typeof(CarouselIndicators), 10, BindingMode.OneWay);

        public ControlStyleCorner StyleCorner
        {
            get { return (ControlStyleCorner)this.GetValue(StyleCornerProperty); }
            set { this.SetValue(StyleCornerProperty, value); }
        }

        public Color IndicatorColor
        {
            get { return (Color)this.GetValue(IndicatorColorProperty); }
            set { this.SetValue(IndicatorColorProperty, value); }
        }

        public double IndicatorWidth
        {
            get { return (double)this.GetValue(IndicatorWidthProperty); }
            set { this.SetValue(IndicatorWidthProperty, value); }
        }
        public double IndicatorHeight
        {
            get { return (double)this.GetValue(IndicatorHeightProperty); }
            set { this.SetValue(IndicatorHeightProperty, value); }
        }
        public double IndicatorCornerRaduis
        {
            get { return (double)this.GetValue(IndicatorCornerRaduisProperty); }
            set { this.SetValue(IndicatorCornerRaduisProperty, value); }
        }

        public int Position
        {
            get { return (int)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }

        public int IndicatorSpacing
        {
            get { return (int)this.GetValue(IndicatorSpacingProperty); }
            set
            {
                this.SetValue(IndicatorSpacingProperty, value);

            }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, (object)value); }
        }
        private void Clear()
        {
            _indicators.Children.Clear();
        }

        private void Init(int position)
        {
            if (_indicators.Children.Count > 0)
            {
                for (int i = 0; i < _indicators.Children.Count; i++)
                {
                    if (((Frame)_indicators.Children[i]).ClassId == nameof(State.Selected) && i != position)
                    {
                        ((Frame)_indicators.Children[i]).ClassId = State.Unselected.ToString();
                        ((Frame)_indicators.Children[i]).BackgroundColor = Color.LightGray;
                        //((Frame)_indicators.Children[i]).Opacity = 0.3;
                        //((Frame)_indicators.Children[i]).ScaleTo(0.8, 300, Easing.SinOut);

                    }
                    else if (((Frame)_indicators.Children[i]).ClassId == nameof(State.Unselected) && i == position)
                    {
                        ((Frame)_indicators.Children[i]).ClassId = State.Selected.ToString();
                        ((Frame)_indicators.Children[i]).BackgroundColor = IndicatorColor;
                        //((Frame)_indicators.Children[i]).Opacity = 1;
                        //((Frame)_indicators.Children[i]).ScaleTo(1.2, 300, Easing.SinOut);
                    }
                }
            }
            else
            {
                if (ItemsSource != null)
                {
                    var enumerator = ItemsSource.GetEnumerator();
                    int count = 0;
                    while (enumerator.MoveNext())
                    {
                        var frame = new Frame()
                        {
                            Padding = 0,
                            HeightRequest = IndicatorHeight,
                            WidthRequest = IndicatorWidth,
                            //BorderColor = IndicatorColor,
                            IsClippedToBounds = true,
                            HasShadow = false,
                            TabIndex = count
                        };

                        switch (StyleCorner)
                        {
                            case ControlStyleCorner.Circle:
                                frame.WidthRequest = IndicatorHeight;
                                frame.CornerRadius = (float)IndicatorHeight / 2;
                                break;
                            case ControlStyleCorner.RoundCorner:
                                frame.CornerRadius = (float)IndicatorHeight / 2;
                                break;
                        };

                        var tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += (sender, eventArgs) => { Position = (sender as Frame).TabIndex; };
                        frame.GestureRecognizers.Add(tapGestureRecognizer);

                        if (position == count)
                        {
                            frame.ClassId = State.Selected.ToString();
                            frame.BackgroundColor = IndicatorColor;
                            //frame.Opacity = 1;
                            //frame.Scale = 1.2;
                        }
                        else
                        {
                            frame.ClassId = State.Unselected.ToString();
                            frame.BackgroundColor = Color.LightGray;
                            //frame.Opacity = 0.3;
                            //frame.Scale = 0.8;
                        }

                        _indicators.Children.Add(frame);
                        count++;
                    }
                }
            }
        }

        private static void PositionChanging(object bindable, object oldValue, object newValue)
        {
            var carouselIndicators = bindable as CarouselIndicators;
            carouselIndicators.Init(Convert.ToInt32(newValue));
        }
        private static void ItemsChanged(object bindable, object oldValue, object newValue)
        {
            var carouselIndicators = bindable as CarouselIndicators;
            carouselIndicators.Clear();
            carouselIndicators.Init(0);
        }

        public enum State
        {
            Selected,
            Unselected
        }
    }
}