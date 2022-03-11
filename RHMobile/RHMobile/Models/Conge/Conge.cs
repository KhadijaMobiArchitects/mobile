using System;
namespace XForms.Models
{
    public class Conge
    {
        public long ID { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Type { get; set; }
        public string Name => Type;
        public string Status { get; set; }

        public Conge()
        {
        }
    }
}
