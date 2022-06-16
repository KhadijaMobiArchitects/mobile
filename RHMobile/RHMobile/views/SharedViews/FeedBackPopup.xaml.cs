using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using XForms.views.Base;

namespace XForms.Popups
{
    public partial class FeedBackPopup : BasePopupView
    {
        public event EventHandler<bool> OnEventAcquired;

        public string HeaderGlyph { get; set; }
        public string TitleText { get; set; }
        public string DescriptionText { get; set; }

        public string CancelActionText { get; set; }
        public bool HasCancelAction { get; set; }

        public string ConfirmActionText { get; set; }

        public Color HeaderGlyphBackground { get; set; }

        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; } = AppHelpers.LookupColor("Danger");

        public bool CanReturnEvent { get; set; }


        public FeedBackPopup(
            string headerGlyph = "",
            Color headerGlyphBackground = default,
            string title = "",
            string description = "",
            string confirmActionText = "",
            string cancelActionText = "",
            bool hasCancelAction = false,
            Color primaryColor = default,
            Color secondaryColor = default,
            bool canReturnEvent = true
            )
        {
            InitializeComponent();

            BindingContext = this;

            HeaderGlyphBackground = headerGlyphBackground;

            HeaderGlyph = headerGlyph;
            TitleText = title;
            DescriptionText = description;

            ConfirmActionText = confirmActionText;
            CancelActionText = cancelActionText;
            HasCancelAction = hasCancelAction;

            PrimaryColor = primaryColor;
            SecondaryColor = secondaryColor;
            
            CanReturnEvent = canReturnEvent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void ConfirmClicked(System.Object sender, System.EventArgs e)
        {
            if (CanReturnEvent)
            {
                OnEventAcquired?.Invoke(this, true);
            }
            else
            {
                await PopupNavigation.Instance.PopSafeAsync();
            }
        }

        private async void Cancel_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopSafeAsync();

            OnEventAcquired?.Invoke(this, false);

        }
    }
}