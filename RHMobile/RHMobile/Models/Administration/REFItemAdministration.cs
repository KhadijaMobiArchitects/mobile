using System;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{
    public class REFItemAdministration
    {
        public AdministrationService Id { get; set; }
        public string Title { get; set; }
        public SvgImageSource ICone { get; set; }

        public REFItemAdministration()
        {
        }
    }
}
