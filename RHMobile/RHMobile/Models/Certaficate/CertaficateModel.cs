using System;
namespace XForms.Models.Certaficate
{
    public class CertaficateModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set;}
        public DateTime EndDate { get; set; }
        public string LabelStatus { get; set; }
        public string LabelType { get; set; }

    }
}
