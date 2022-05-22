using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace XForms.iOS.AppHelpers
{
    public static class Helpers
    {
        public static List<UIAlertController> GetOpenedAlerts()
        {
            var alertViewController = UIApplication
                .SharedApplication
                .Windows
                .Where(x => x.RootViewController?.PresentedViewController?.GetType() == typeof(UIAlertController))
                .Select(s => s.RootViewController?.PresentedViewController as UIAlertController)
                .ToList();

            return alertViewController;
        }

        public static bool DismissAlerts(List<UIAlertController> alertControllers)
        {
            foreach (var item in alertControllers)
            {
                if (item != null)
                {
                    item.DismissViewController(true, null);
                }
            }

            return true;
        }
    }
}