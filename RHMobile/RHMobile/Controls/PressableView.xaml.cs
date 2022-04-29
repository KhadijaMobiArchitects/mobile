using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace XForms.Controls
{
    public partial class PressableView : ContentView
    {
        public static readonly BindableProperty IsHasClickEffectProperty =
    BindableProperty.Create(nameof(IsHasClickEffect), typeof(bool), typeof(PressableView), true, BindingMode.TwoWay);

        public bool IsHasClickEffect
        {
            get { return (bool)GetValue(IsHasClickEffectProperty); }
            set
            {
                SetValue(IsHasClickEffectProperty, value);
            }
        }

        public static readonly BindableProperty IsClickableProperty =
  BindableProperty.Create(nameof(IsClickable), typeof(bool), typeof(PressableView), true, BindingMode.TwoWay);

        public bool IsClickable
        {
            get { return (bool)GetValue(IsClickableProperty); }
            set
            {
                SetValue(IsClickableProperty, value);
            }
        }

        public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(PressableView), default, defaultBindingMode: BindingMode.OneWay);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
       BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(PressableView), default, defaultBindingMode: BindingMode.OneWay);

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler Clicked;


        public PressableView()
        {
            InitializeComponent();

            if (IsClickable)
            {
                var gestureRecognizer = new TapGestureRecognizer();

                gestureRecognizer.Tapped += async (object sender, EventArgs e) =>
                {
                    var view = sender as PressableView;

                    if (!IsTabStop) return;

                    if (IsHasClickEffect)
                    {
                        //await AppHelpers.ClickEffect(view);
                    }

                    Clicked?.Invoke(sender, e);
                };

                this.GestureRecognizers.Add(gestureRecognizer);
            }
        }
    }
}
