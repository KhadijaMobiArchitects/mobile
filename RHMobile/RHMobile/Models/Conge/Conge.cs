using System;
using Xamarin.Forms;

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

        //public long IdStatus => Status switch
        //{
        //    "En cours" => 1,
        //    "Confirmé" => 2,
        //    "Reporté" => 3
        //};

        public Color BackgroundColor => Status switch
        {
            "En cours" => Color.FromHex("#FEE07D"),
            "Confirmé" => Color.FromHex("#95D5A4"),
            "Reporté" => Color.FromHex("#D59595")
        };

        public Color TextColor => Status switch
        {
            "En cours" => Color.FromHex("#E6992A"),
            "Confirmé" => Color.FromHex("#589266"),
            "Reporté" => Color.FromHex("#925858")
        };

        public Conge()
        {
        }
    }
}
