using System;
namespace XForms.Constants
{
    public class AppUrls
    {
        public const string BaseUrl = "https://rh-api-dev.azurewebsites.net/api/";

        public const string GesRequestsListLeave = BaseUrl + "leaves/getleaves/";
        public const string GetRequestListTypeLeave = BaseUrl + "RefTypesLeave/";
        public const string GetRequestListProject = BaseUrl + "Projects/GetProjects";
        public const string GetRequestSituationProject = BaseUrl + "RefSituationsProject/";
    }
}
