using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{
    public class Leave
    {
        [JsonIgnore]
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public string Name => Type;
        public int StatusID { get; set; }
        public bool ConfirmedBySquad { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public int RefLeaveStatusId { get; set; }
        public int RefTypeLeaveId { get; set; }


        [JsonIgnore]
        public string Status => (LeaveStatus)RefLeaveStatusId switch
        {
            LeaveStatus.Inprogress => "En cours",
            LeaveStatus.Confirmed => "Confirmé",
            LeaveStatus.Postponed => "Reporté"

        };
        //public long IdStatus => Status switch
        //{
        //    "En cours" => 1,
        //    "Confirmé" => 2,
        //    "Reporté" => 3
        //};
        [JsonIgnore]
        public Color BackgroundColor => (LeaveStatus)RefLeaveStatusId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#FEE07D"),
            LeaveStatus.Confirmed => Color.FromHex("#95D5A4"),
            LeaveStatus.Postponed => Color.FromHex("#D59595"),
            _ => Color.Gray

        };

        [JsonIgnore]
        public Color TextColor => (LeaveStatus)RefLeaveStatusId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#E6992A"),
            LeaveStatus.Confirmed => Color.FromHex("#589266"),
            LeaveStatus.Postponed => Color.FromHex("#925858"),
            _=> Color.Gray
        };

        [JsonIgnore]
        public int DifferenceOfDays => (int)(EndDate - StartDate).TotalDays;

        public Leave()
        {
        }
    }
}
