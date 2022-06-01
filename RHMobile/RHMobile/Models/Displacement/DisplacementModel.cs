using System;

using Xamarin.Forms;

namespace XForms.Models
{
    public class DisplacementModel
    {
        public double LatitudeDepart { get; set; }
        public double LongitudeDepart { get; set; }
        public double LatitudeArrivée { get; set; }
        public double LongitudeArrivée { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public string Motif { get; set; }
    }
    public class UpdateDeplacementModel
    {
        public int Id { get; set; }
        public int RefStatusDeplacementId { get; set; }
    }
}

