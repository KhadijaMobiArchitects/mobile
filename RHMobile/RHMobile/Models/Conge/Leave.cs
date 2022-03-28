using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using XForms.Enum;

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
        public int StatusID { get; set; }

        [JsonIgnore]
        public string Status => (StatusConge)StatusID switch
        {
            StatusConge.Inprogress => "En cours",
            StatusConge.Confirmed => "Confirmé",
            StatusConge.Postponed => "Reporté"

        };
        //public long IdStatus => Status switch
        //{
        //    "En cours" => 1,
        //    "Confirmé" => 2,
        //    "Reporté" => 3
        //};
        [JsonIgnore]
        public Color BackgroundColor => (StatusConge)StatusID switch
        {
            StatusConge.Inprogress => Color.FromHex("#FEE07D"),
            StatusConge.Confirmed => Color.FromHex("#95D5A4"),
            StatusConge.Postponed => Color.FromHex("#D59595"),
            _ => Color.Gray

        };

        [JsonIgnore]
        public Color TextColor => (StatusConge)StatusID switch
        {
            StatusConge.Inprogress => Color.FromHex("#E6992A"),
            StatusConge.Confirmed => Color.FromHex("#589266"),
            StatusConge.Postponed => Color.FromHex("#925858"),
            _=> Color.Gray
        };

        [JsonIgnore]
        public int DifferenceOfDays => (int)(DateFin - DateDebut).TotalDays;

        public Conge()
        {
        }
    }
}
