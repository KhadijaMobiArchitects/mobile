using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XForms.Resources;

namespace XForms.Models
{
    public partial class ProfilResponse : BindableObject
    {
        public string RecId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }

        //public IFormFile? Picture { get; set; }

        public string PictureUrl { get; set; }

        public bool IsOwner { get; set; }

        public int RefFunctionId { get; set; }

        public String RefFunctionLabel { get; set; }

        public object DateAffectation { get; set; }
        public List<string> OtherProjects { get; set; }
        public string ProjetName { get; set; }
        public double EstimationProject { get; set; }

        //public string FullName => FirstName +" "+ LastName ;
        public string OtherProjectsString => String.Join(" , ", OtherProjects);

    }

    public partial class ProfilResponse : BindableObject
    {
        public bool IsSelected { get; set; }
        public bool IsSelectedAsOwner { get; set; }
        public bool IsSelectedAsMember { get; set; } 

        public string CheckedIcon => IsSelected ? FontAwesomeFonts.CheckCircle : FontAwesomeFonts.Circle;
        public Color CheckedColor => IsSelected ? Color.Green : AppHelpers.LookupColor("PlaceholderColor");

    }
}
