using System;
using Xamarin.Forms;
using XForms.Resources;

namespace XForms.Models
{
    public partial class ProfilResponse
    {
        public string RecId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 

        //public IFormFile? Picture { get; set; }

        public string PictureUrl { get; set; }

        public bool IsOwner { get; set; }

        public int RefFunctionId { get; set; }

        public String RefFunctionLabel { get; set; }



    }

    public partial class ProfilResponse : BindableObject
    {
        public bool IsSelected { get; set; }


        public string CheckedIcon => IsSelected ? FontAwesomeFonts.CheckCircle : FontAwesomeFonts.Circle;
        public Color CheckedColor => IsSelected ? Color.Green : AppHelpers.LookupColor("PlaceholderColor");

    }
}
