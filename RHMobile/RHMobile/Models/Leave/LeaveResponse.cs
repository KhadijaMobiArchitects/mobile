using System;
<<<<<<< Updated upstream
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{

    public partial class LeaveResponse
=======
namespace XForms.Models
{
    public class LeaveResponse
>>>>>>> Stashed changes
    {
        public int id { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool confirmedBySquad { get; set; }
        public DateTime createdOn { get; set; }
        public string createdBy { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int refStatusLeaveId { get; set; }
        public string labelStatus { get; set; }
        public int refTypeLeaveId { get; set; }
        public string labelType { get; set; }
<<<<<<< Updated upstream
        public string pictureUrl { get; set; }

    }
    public partial class LeaveResponse
    {
        public Color BackgroundColor => (LeaveStatus)refStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#FEE07D"),
            LeaveStatus.Confirmed => Color.FromHex("#95D5A4"),
            _ => Color.Gray

        };
        public Color TextColor => (LeaveStatus)refStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#E6992A"),
            LeaveStatus.Confirmed => Color.FromHex("#589266"),
            _ => Color.Gray
        };

        public string FullName => firstName + " " + lastName;

=======
>>>>>>> Stashed changes
    }
}
