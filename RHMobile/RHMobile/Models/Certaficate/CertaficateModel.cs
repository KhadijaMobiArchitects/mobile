using System;
namespace XForms.Models
{
    //public class CertaficateModel
    //{
    //    public int Id { get; set; }
    //    public DateTime StartDate { get; set;}
    //    public DateTime EndDate { get; set; }
    //    public string LabelStatus { get; set; }
    //    public string LabelType { get; set; }

    //}

    public class CertaficateModel
    {
        public int Id { get; set; }
        public string Objectif { get; set; } = null!;
        public int RefTypeCertificateId { get; set; }


    }

    public class CertaficateTreatementRequest
    {
        public int Id { get; set; }
        public File Document { get; set; }

    }

    public class TypeCertaficate
    {
        public int Id { get; set; }
        public string Label { get; set; }
    }
}
