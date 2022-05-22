using System;
namespace XForms.Models
{
    public class ComplaintModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public  DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
