using System;
using Xamarin.Forms;
using XForms.Enum;

namespace XForms.Models
{

    public partial class LeaveResponse
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool ConfirmedBySquad { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RefStatusLeaveId { get; set; }
        public string LabelStatus { get; set; }
        public int RefTypeLeaveId { get; set; }
        public string LabelType { get; set; }
        public string PictureUrl { get; set; }
        public string ProjectName { get; set; }
        public string SituationProjet { get; set; }
        public int NombreJours { get; set; }
        public string Owner { get; set; }
        public string RefFunctionLabel { get; set; }
    }
    public partial class LeaveResponse
    {
        public Color BackgroundColor => (LeaveStatus)RefStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#FEE07D"),
            LeaveStatus.Confirmed => Color.FromHex("#95D5A4"),
            _ => Color.Gray

        };
        public Color TextColor => (LeaveStatus)RefStatusLeaveId switch
        {
            LeaveStatus.Inprogress => Color.FromHex("#E6992A"),
            LeaveStatus.Confirmed => Color.FromHex("#589266"),
            _ => Color.Gray
        };

        public string FullName => FirstName + " " + LastName;
        //public string confirmerParSquad => ConfirmedBySquad switch
        //{
        //     true => "Confirmé",
        //    false => "-----"
        //};

    }
}
