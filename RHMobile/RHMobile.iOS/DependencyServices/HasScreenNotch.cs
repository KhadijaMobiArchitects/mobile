using UIKit;
using System.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using XForms.iOS.DependencyServices;
using XForms.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(HasNotchScreen))]
namespace XForms.iOS.DependencyServices
{
    public class HasNotchScreen : INotchScreen
    {
        public bool CheckHasNotchScreen()
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    return UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom > 0;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                //XForms.AppHelpers.Alert(ex.Message);
                return false;
            }
        }
    }
}