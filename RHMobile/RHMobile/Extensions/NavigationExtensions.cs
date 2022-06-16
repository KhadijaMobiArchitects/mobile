using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;

namespace XForms
{
    public static class NavigationExtensions
    {
        public static async Task PushSingleAsync(this INavigation nav, Page page, bool animated = false)
        {
            if (nav.NavigationStack.Count == 0 || nav.NavigationStack.Last().GetType() != page.GetType())
                await nav.PushAsync(page, animated);
        }

        public static async Task PushModalSingleAsync(this INavigation nav, Page page, bool animated = true)
        {
            if (nav.ModalStack.Count == 0 || nav.ModalStack.Last().GetType() != page.GetType())
                await nav.PushModalAsync(page, animated);
        }

        public static async Task PopSafeAsync(this INavigation nav, bool animated = false)
        {

            if (nav.NavigationStack.Count > 0)
                await nav.PopAsync(animated);
        }

        public static async Task PopModalSafeAsync(this INavigation nav, bool animated = true)
        {
            if (nav.ModalStack.Count > 0)
                await nav.PopModalAsync(animated);
        }
    }
}