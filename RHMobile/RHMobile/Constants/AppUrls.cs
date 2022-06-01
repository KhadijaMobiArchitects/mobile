using System;
namespace XForms.Constants
{
    public class AppUrls
    {
        //public const string BaseUrl = "https://rh-api-dev.azurewebsites.net/api/";
        public const string BaseUrl = "http://rh-api-dev-mobiarchitects.azurewebsites.net/api/";

        public const string GoogleMapBaseUrl = "https://maps.googleapis.com/maps/";

        public const string Singin = BaseUrl + "Account/Authenticate";

        public const string GetRequestsListProfilsLeave = BaseUrl + "leaves/GetAllLeaves/";
        public const string GetRequestInProgressProfilsLeave = BaseUrl + "leaves/GetInProgressLeaves/";
        public const string GetRequestValidatedProfilsLeave = BaseUrl + "leaves/GetValidatedLeaves/";


        public const string GetRequestsListProfilLeaves = BaseUrl + "leaves/getprofilleaves/";

        //ProfilLeaves

        public const string GetRequestListTypeLeave = BaseUrl + "RefTypesLeave/";
        public const string GetRequestListProject = BaseUrl + "Projects/GetProjects";
        public const string GetRequestListActualProject = BaseUrl + "Projects/getactualprojects";

        public const string GetRequestListProfilProject = BaseUrl + "Projects/GetProfilProjects";
        public const string GetRequestSituationProject = BaseUrl + "RefSituationsProject/";
        public const string GetRequestProjectSquad = BaseUrl + "projects/getSquad/";
        public const string GetRequestProfils = BaseUrl + "Profils/GetProfils";
        public const string GetRequestOwners = BaseUrl + "Profils/GetOwners";

        public const string GetRequestAllCertaficates = BaseUrl + "Certificate/GetAllCertificates";
        public const string GetRequestProfilCertificates = BaseUrl + "Certificate/GetProfilCertificates";
        public const string GetRequestTypeCertificates = BaseUrl + "Certificate/GetTypeCertificates";




        public const string PostLeaveRequest = BaseUrl + "leaves/CreateLeave/";
        public const string DeleteLeaveRequest = BaseUrl + "leaves/deleteLeave";
        public const string PostProjectRequest = BaseUrl + "projects/CreateProject";
        public const string GetStaffMembersToAddRequest = BaseUrl + "Projects/GetStaffMembersToAdd";
        public const string PostMembersRequest = BaseUrl + "Projects/AddStaffMembers";
        public const string PostChangePercentRequest = BaseUrl + "Projects/ChangePercent";
        public const string PostUpdateLeaveRequest = BaseUrl + "leaves/UpdateLeave";
        public const string PostDemandCertaficate = BaseUrl + "Certificate/DemandCertificate";
        public const string PostTraitementDemandCertificate = BaseUrl + "Certificate/DemandCertificate";

        public const string PostDisplacementRequest = BaseUrl + "Deplacement/DemandDeplacement";

        public const string GetProfilDeplacement = BaseUrl + "Deplacement/GetProfilDeplacements";
        public const string GetAllDeplacement = BaseUrl + "Deplacement/GetAllDeplacements";
        public const string PostUpdateDeplacement = BaseUrl + "Deplacement/UpdateDeplacement";



        //public const string PostRequestLeave = "http://localhost:3000/Leaves";



    }
}
