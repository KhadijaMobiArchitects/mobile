using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{

    public partial class DisplacementResponse : BindableObject
    {
        public int Id { get; set; }
        public int StartPositionId { get; set; }
        public int EndPositionId { get; set; }
        public StartPostion StartPostion { get; set; }
        public EndPostion EndPostion { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string CreatedBy { get; set; }
        public int RefStatusDeplacementId { get; set; }
        public string RefStatusName { get; set; }
        public string Motif { get; set; }
        public string PictureUrl { get; set; }
    }

    public partial class DisplacementResponse
    {
        public Color BackgroundColor => (Status)RefStatusDeplacementId switch
        {
            Status.Inprogress => Color.FromHex("#FEE07D"),
            Status.Confirmed => Color.FromHex("#95D5A4"),
            _ => Color.Gray

        };
        public Color TextColor => (Status)RefStatusDeplacementId switch
        {
            Status.Inprogress => Color.FromHex("#E6992A"),
            Status.Confirmed => Color.FromHex("#589266"),
            _ => Color.Gray
        };

        public string StartAddress { get; set; }
        public string EndAddress { get; set; }

    }

    public class StartPostion
    {
        [JsonProperty("item1")]
        public double Latitude { get; set; }

        [JsonProperty("item2")]
        public double Longitude { get; set; }
    }
    public class EndPostion
    {
        [JsonProperty("item1")]
        public double Latitude { get; set; }

        [JsonProperty("item2")]
        public double Longitude { get; set; }
    }
}
