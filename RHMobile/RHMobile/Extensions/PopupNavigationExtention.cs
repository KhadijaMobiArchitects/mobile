using System;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;

namespace XForms
{
    public static class PopNavigationExtensions
    {
        public static async Task PushSingleAsync(
            this IPopupNavigation nav,
            PopupPage page,
            bool animated = true)
        {
            if (nav.PopupStack.Count == 0 || nav.PopupStack.Last().GetType() != page.GetType())
            {
                await nav.PushAsync(page, animated).ConfigureAwait(false);
            }
        }

        public static async Task PopSafeAsync(
            this IPopupNavigation nav,
            bool animated = true)
        {
            if (nav.PopupStack.Count > 0)
            {
                await nav.PopAsync(animated).ConfigureAwait(false);
            }
        }
    }
}
