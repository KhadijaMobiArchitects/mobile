using System;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{
    //public partial class CertaficateResponse
    //{
    //    public int Id { get; set; }
    //    public string PictureUrl { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string LabelStatus { get; set; }
    //    public string LabelType { get; set; }

    //}

    //public partial class CertaficateResponse
    //{
    //    public string FullName => FirstName + " " + LastName;
    //}
    public partial class CertaficateResponse
    {

        public int Id { get; set; }
        public string Objectif { get; set; } = null!;
        public string? DocUrl { get; set; }

        public string CreatedBy { get; set; } = null!;
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }

        public string? UpdatedBy { get; set; }
        public string UpdatedByName { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public string imageUrl { get; set; }

        public int RefTypeCertificateId { get; set; }
        public string RefTypeCertificateLabel { get; set; }

        public int RefStatusCertificateId { get; set; }
        public string RefStatusCertificateLabel { get; set; }
    }
    public partial class CertaficateResponse
    {
        public Color BackgroundColor => (Status)RefStatusCertificateId switch
        {
            Status.Inprogress => Color.FromHex("#FEE07D"),
            Status.Confirmed => Color.FromHex("#95D5A4"),
            _ => Color.Gray

        };
        public Color TextColor => (Status)RefStatusCertificateId switch
        {
            Status.Inprogress => Color.FromHex("#E6992A"),
            Status.Confirmed => Color.FromHex("#589266"),
            _ => Color.Gray
        };

        //public string FullName => firstName + " " + lastName;

    }
}
