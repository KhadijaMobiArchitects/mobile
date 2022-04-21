using System;
namespace XForms.Constants
{
    public class AppUrls
    {
        //public const string BaseUrl = "https://rh-api-dev.azurewebsites.net/api/";
        public const string BaseUrl = "http://rh-api-dev-mobiarchitects.azurewebsites.net/api/";

        

        public const string Singin = BaseUrl + "Account/Authenticate";

        public const string GesRequestsListLeave = BaseUrl + "leaves/getleaves/";
        public const string GetRequestListTypeLeave = BaseUrl + "RefTypesLeave/";
        public const string GetRequestListProject = BaseUrl + "Projects/GetProjects";
        public const string GetRequestSituationProject = BaseUrl + "RefSituationsProject/";
        public const string PostRequestLeave = BaseUrl + "leaves/postLeave/";
        public const string DeleteRequestLeave = BaseUrl + "leaves/deleteLeave";




        //public const string PostRequestLeave = "http://localhost:3000/Leaves";



    }
}
