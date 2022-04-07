using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XForms.Constants;
using XForms.Enum;
using XForms.HttpREST;
using XForms.Models;
using XForms.Services.Base;

namespace XForms.Services
{
    public class AppServices : BaseService
    {
        public async Task<RESTServiceResponse<IEnumerable<Leave>>> GetLeaves()
        {
            return await RESTHelper.GetRequest<IEnumerable<Leave>>(url: $"{AppUrls.GesRequestsListLeave}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<REFTypeLeave>>> GetTypesLeave()
        {
            return await RESTHelper.GetRequest<IEnumerable<REFTypeLeave>>(url: $"{AppUrls.GetRequestListTypeLeave}", method: HttpVerbs.GET);
        }
        public async Task<RESTServiceResponse<IEnumerable<Project>>> GetProjects()
        {
            return await RESTHelper.GetRequest<IEnumerable<Project>>(url: $"{AppUrls.GetRequestListProject}", method: HttpVerbs.GET);
        }
        public async Task<RESTServiceResponse<IEnumerable<SituationProject>>> GetSituationsProject()
        {
            return await RESTHelper.GetRequest<IEnumerable<SituationProject>>(url: $"{AppUrls.GetRequestSituationProject}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<object>> PostLeave(Leave postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostRequestLeave}",postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<object>> DeleteLeave(long Id)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.DeleteRequestLeave}/{Id}", method: HttpVerbs.DELETE);
        }
    }


}
