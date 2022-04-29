using System;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace XForms.Models
{
    public partial class Project 
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public string OwnerBy { get; set; }
        public string CreatedBy { get; set; }
        public string PictureUrl { get; set; }

        //public ImageSource Image { get; set; }

    }

    public partial class Project : BindableObject
    {
        public bool IsSelected { get; set; }

        public Color BackgroundColor => IsSelected ? Color.FromHex("#4ACFF9") : Color.White;
        public Color TextColor => IsSelected ? Color.White : Color.Black;
        public bool ShowPercent { get; set; } = true;
        public String OwnerName { get; set; }

        public Color PercentBackgroundColor {
            get {
                if (Percent < 30)
                    return AppHelpers.LookupColor("postponedColor");
                else if (Percent >= 30 && Percent <= 70)
                    return AppHelpers.LookupColor("InProgessColor");
                else
                    return AppHelpers.LookupColor("ConfirmedColor");
            }
        }

        public Color PercentTextColor
        {
            get
            {
                if (Percent < 30)
                    return AppHelpers.LookupColor("postponedTextColor");
                else if (Percent >= 30 && Percent <= 70)
                    return AppHelpers.LookupColor("InProgessTextColor");
                else
                    return AppHelpers.LookupColor("ConfirmedTextColor");
            }
        }

    }
}
