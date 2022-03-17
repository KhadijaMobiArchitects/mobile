using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XForms.Models
{
    public class Conge
    {
        [JsonIgnore]
        public long ID { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public string Name => Type;
        public string Status { get; set; }

        //public long IdStatus => Status switch
        //{
        //    "En cours" => 1,
        //    "Confirmé" => 2,
        //    "Reporté" => 3
        //};
        [JsonIgnore]
        public Color BackgroundColor => Status switch
        {
            "En cours" => Color.FromHex("#FEE07D"),
            "Confirmé" => Color.FromHex("#95D5A4"),
            "Reporté" => Color.FromHex("#D59595"),
            _ => Color.Gray

        };
        [JsonIgnore]
        public Color TextColor => Status switch
        {
            "En cours" => Color.FromHex("#E6992A"),
            "Confirmé" => Color.FromHex("#589266"),
            "Reporté" => Color.FromHex("#925858"),
            _=> Color.Gray
        };

        [JsonIgnore]
        public int DifferenceOfDays => (int)(DateFin - DateDebut).TotalDays;

        public Conge()
        {
        }
    }
}
