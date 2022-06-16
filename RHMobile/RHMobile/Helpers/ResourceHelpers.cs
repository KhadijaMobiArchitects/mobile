using System;
using XForms.Enum;
using System.Linq;
namespace XForms
{
    public static class ResourceHelpers
    {
        public static object GetServiceIcon(AdministrationService service)
        {
            FFImageLoading.Svg.Forms.SvgImageSource iconService = null;

            //switch (service)
            //{
            //    case AdministrationService.Leave:
            //        iconService = AppHelpers.GetSvgResource("calendar.svg");
            //        break;
            //}

            iconService = service switch
            {
                AdministrationService.Leave => AppHelpers.GetSvgResource("calendar.svg"),
                AdministrationService.Certaficate => AppHelpers.GetSvgResource("certaficate.svg"),
                AdministrationService.Move => AppHelpers.GetSvgResource("move.svg"),
                AdministrationService.Complaint => AppHelpers.GetSvgResource("complaint.svg"),
                AdministrationService.Project => AppHelpers.GetSvgResource("project.svg"),
                AdministrationService.Intership => AppHelpers.GetSvgResource("intership.svg"),
                AdministrationService.PersonalData => AppHelpers.GetSvgResource("personalData.svg"),
                AdministrationService.Delegation => AppHelpers.GetSvgResource("delegation.svg"),
                AdministrationService.Payslips => AppHelpers.GetSvgResource("payslips.svg"),
                AdministrationService.RCAR => AppHelpers.GetSvgResource("rcar.svg"),
                AdministrationService.Recore => AppHelpers.GetSvgResource("recore.svg"),
                AdministrationService.RecorePrime => AppHelpers.GetSvgResource("recoreprime.svg"),
                _ => AppHelpers.GetSvgResource("calendar.svg")
            };

            return iconService;
        }

        public static string GetServiceTitle(AdministrationService service)
        {
            string Title=null;

            Title = service switch
            {
                AdministrationService.Leave => BackToLine("Congé"),
                AdministrationService.Certaficate => BackToLine("Attestation"),
                AdministrationService.Move => BackToLine("Déplacement"),
                AdministrationService.Complaint => BackToLine("Réclamation"),
                AdministrationService.Project => BackToLine("Projet"),
                AdministrationService.Intership => BackToLine("Espace Stagaires"),
                AdministrationService.PersonalData => BackToLine("Données Personnelles"),
                AdministrationService.Delegation => BackToLine("Délegations"),
                AdministrationService.Payslips => BackToLine("Bulletions De paie"),
                AdministrationService.RCAR => BackToLine("RCAR"),
                AdministrationService.Recore => BackToLine("Recore"),
                AdministrationService.RecorePrime => BackToLine("Recore Sur prime"),
                _ => " "
            };

            return Title;
        }

        public static string BackToLine(string ServiceName)
        {
            try
            {
                string[] subs = ServiceName.Split(' ');
                ServiceName = (subs.Length == 1) ? subs[0] + "\n" : (subs.Length == 2)? subs[0] + "\n" + subs[1]: subs[0] + "\n" + subs[1] +" "+subs[2];
                return ServiceName;


            }
            catch (Exception ex)
            {

            }

            return ServiceName;
        }

    }
}
