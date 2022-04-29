using System;
using System.Collections.Generic;
using System.Windows.Input;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.views.Base;

namespace XForms.views.SharedViews
{
    public partial class SuggestionsNavBarView : BaseContent
    {

        public static readonly BindableProperty HasTitleProperty = BindableProperty.Create(nameof(HasTitle), typeof(bool), typeof(SuggestionsNavBarView), false);
        public bool HasTitle
        {
            get
            {
                return (bool)GetValue(HasTitleProperty);
            }
            set
            {
                SetValue(HasTitleProperty, value);
            }
        }
        public static readonly BindableProperty HasImageProfilProperty = BindableProperty.Create(nameof(HasImageProfil), typeof(bool), typeof(SuggestionsNavBarView), false);
        public bool HasImageProfil
        {
            get
            {
                return (bool)GetValue(HasImageProfilProperty);
            }
            set
            {
                SetValue(HasImageProfilProperty, value);
            }
        }

        public static readonly BindableProperty HasDescriptionProperty = BindableProperty.Create(nameof(HasDescription), typeof(bool), typeof(SuggestionsNavBarView), false);
        public bool HasDescription
        {
            get
            {
                return (bool)GetValue(HasDescriptionProperty);
            }
            set
            {
                SetValue(HasDescriptionProperty, value);
            }
        }

        public static readonly BindableProperty HasNotificationProperty = BindableProperty.Create(nameof(HasNotification), typeof(bool), typeof(SuggestionsNavBarView), false);
        public bool HasNotification
        {
            get
            {
                return (bool)GetValue(HasNotificationProperty);
            }
            set
            {
                SetValue(HasNotificationProperty, value);
            }
        }

        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create(nameof(HasBackButton), typeof(bool), typeof(SuggestionsNavBarView),false);
        public bool HasBackButton
        {
            get
            {
                return (bool)GetValue(HasBackButtonProperty);
            }
            set
            {
                SetValue(HasBackButtonProperty, value);
            }
        }
        public static readonly BindableProperty BackButtonColorProperty = BindableProperty.Create(nameof(BackButtonColor), typeof(Color), typeof(SuggestionsNavBarView), default);
        public Color BackButtonColor
        {
            get
            {
                return (Color)GetValue(BackButtonColorProperty);
            }
            set
            {
                SetValue(BackButtonColorProperty, value);
            }
        }
        public static readonly BindableProperty BackButtonBorderColorProperty = BindableProperty.Create(nameof(BackButtonBorderColor), typeof(Color), typeof(SuggestionsNavBarView), default);
        public Color BackButtonBorderColor
        {
            get
            {
                return (Color)GetValue(BackButtonBorderColorProperty);
            }
            set
            {
                SetValue(BackButtonBorderColorProperty, value);
            }
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(SuggestionsNavBarView), string.Empty);
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(SuggestionsNavBarView), string.Empty);
        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        public static readonly BindableProperty ImageProfilProperty =
BindableProperty.Create(nameof(ImageProfil), typeof(ImageSource), typeof(View), null, BindingMode.TwoWay);

        public ImageSource ImageProfil
        {
            get { return (ImageSource)GetValue(ImageProfilProperty); }
            set
            {
                SetValue(ImageProfilProperty, value);
            }
        }

        private bool canNavigationBack = true;
        public ICommand NavigationBack => new Command(() =>
        {
            //App.Current.MainPage.Navigation.PushAsync(new LeaveRequest());
            try
            {
                canNavigationBack = false;
                App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                canNavigationBack = true;
            }


        },
    () => canNavigationBack);

        public SuggestionsNavBarView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var isHasNotchScreen = AppHelpers.CheckHasNotchScreen();

                this.Padding = isHasNotchScreen ? new Thickness(30, 40, 30, 0) : new Thickness(30, 30, 30, 0);
            }
            else if (Device.RuntimePlatform == Device.Android)
                this.Padding = new Thickness(30, 20, 30, 0);
        }
        

    }
}
