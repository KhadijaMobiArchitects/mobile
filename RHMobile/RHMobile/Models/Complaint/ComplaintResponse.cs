using System;
namespace XForms.Models
{
    public class ComplaintResponse
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Objet { get; set; }
    }
}
