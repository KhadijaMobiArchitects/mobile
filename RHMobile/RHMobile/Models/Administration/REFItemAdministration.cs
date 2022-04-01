using System;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace XForms.Models
{
    public class REFItemAdministration
    {
        public string Title { get; set; }
        public SvgImageSource ICone { get; set; }
        public Thickness padding { get; set; }

        public REFItemAdministration()
        {
        }
    }
}
