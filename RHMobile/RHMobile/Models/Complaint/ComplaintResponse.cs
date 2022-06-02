using System;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{
    public partial class ComplaintResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Object { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int RefStatusClaimId { get; set; }
        public string RefStatusClaimName { get; set; }
        public string CreatedByFunction { get; set; }
    }
    public partial class ComplaintResponse
    {
        public Color BackgroundColor => (Status)RefStatusClaimId switch
        {
            Status.Inprogress => Color.FromHex("#FEE07D"),
            Status.Confirmed => Color.FromHex("#95D5A4"),
            _ => Color.Gray

        };
        public Color TextColor => (Status)RefStatusClaimId switch
        {
            Status.Inprogress => Color.FromHex("#E6992A"),
            Status.Confirmed => Color.FromHex("#589266"),
            _ => Color.Gray
        };

    }
}
