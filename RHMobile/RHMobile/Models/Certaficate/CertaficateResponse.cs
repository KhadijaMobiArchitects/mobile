using System;
namespace XForms.Models
{
    public partial class CertaficateResponse
    {
        public int Id { get; set; }
        public string PictureUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LabelStatus { get; set; }
        public string LabelType { get; set; }

    }

    public partial class CertaficateResponse
    {
        public string FullName => FirstName + " " + LastName;
    }
}
