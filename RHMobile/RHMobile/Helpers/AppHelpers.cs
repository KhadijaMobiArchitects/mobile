using System;
using System.Reflection;
using Xamarin.Forms;

namespace XForms
{
    public static class AppHelpers
    {
        public static FFImageLoading.Svg.Forms.SvgImageSource GetSvgResource(string sourceName)
        {
            try
            {
                return FFImageLoading.Svg.Forms.SvgImageSource.FromResource("XForms.Resources.Images." + sourceName, typeof(AppHelpers).GetTypeInfo().Assembly);
            }
            catch
            {
                return default;
            }
        }

        public static Xamarin.Forms.ImageSource GetImageResource(string sourceName)
        {
            try
            {
                return ImageSource.FromResource("XForms.Resources.Images." + sourceName, typeof(AppHelpers).GetTypeInfo().Assembly);
            }
            catch
            {
                return default;
            }
        }
    }
}
