using System;
using Newtonsoft.Json;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{
    public class LeaveModel
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool ConfirmedBySquad { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int RefStatusLeaveId { get; set; }
        public int RefTypeLeaveId { get; set; }
        public string LabelType { get; set; }
        public string labelStatus { get; set; }
        public int ProjectId { get; set; }
        public int RefSituationProjectId { get; set; }

        [JsonIgnore]
        public string ProjectName { get; set; }
        [JsonIgnore]
        public string SituationProjectName { get; set; }

        [JsonIgnore]
        public string Name => LabelType;

        [JsonIgnore]
        public Color BackgroundColor => (LeaveStatus)RefStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#FEE07D"),
            LeaveStatus.Confirmed => Color.FromHex("#95D5A4"),
            LeaveStatus.Postponed => Color.FromHex("#D59595"),
            _ => Color.Gray

        };

        [JsonIgnore]
        public Color TextColor => (LeaveStatus)RefStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#E6992A"),
            LeaveStatus.Confirmed => Color.FromHex("#589266"),
            LeaveStatus.Postponed => Color.FromHex("#925858"),
            _=> Color.Gray
        };

        [JsonIgnore]
        public int DifferenceOfDays => (int)(EndDate - StartDate).TotalDays+1;

    }
}
