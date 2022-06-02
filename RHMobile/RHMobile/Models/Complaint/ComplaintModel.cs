using System;
namespace XForms.Models
{
    public class ComplaintModel
    {
        public string Title { get; set; }
        public string Object { get; set; }

    }
    public class ComplaintTraitement
    {
        public int Id { get; set; }
        public int RefStatusClaimId { get; set; }
    }
}
