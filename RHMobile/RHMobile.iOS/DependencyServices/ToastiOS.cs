using UIKit;
using System.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using XForms.iOS.DependencyServices;
using XForms.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(ToastIos))]
namespace XForms.iOS.DependencyServices
{
    public class ToastIos : IToast
    {

        public void Alert(string message, ToastLength toastLength, bool showActionButtons)
        {
            LongAlert(message, toastLength, showActionButtons);
        }

        public void LongAlert(string text, ToastLength toastLength, bool showActionButtons)
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window?.RootViewController;
            while (vc?.PresentedViewController != null)
            {
                // workaround for popups
                vc = vc?.PresentedViewController;
            }

            if (vc == null)
            {
                return;
            }

            var openedAlerts = AppHelpers.Helpers.GetOpenedAlerts();
            AppHelpers.Helpers.DismissAlerts(openedAlerts);

            var okAlert = UIAlertController.Create(string.Empty, text, UIAlertControllerStyle.Alert);

            if (showActionButtons)
            {
                okAlert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            }
            else
            {
                var alertDelay = Foundation.NSTimer.CreateScheduledTimer(GetToastLength(toastLength), async obj =>
                {
                    await Xamarin.Essentials.MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        DismissMessage(okAlert, obj);
                    });
                });
            }

            vc.PresentViewController(okAlert, true, null);
        }

        void DismissMessage(UIKit.UIAlertController alert, Foundation.NSTimer alertDelay)
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }

            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }

        private double GetToastLength(ToastLength toastLength)
        {
            switch (toastLength)
            {
                case ToastLength.LONG: return 3.5;
                case ToastLength.SHORT: return 2.0;
                default: return 2.0;
            }
        }

    }
}