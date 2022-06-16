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
        public async Task<RESTServiceResponse<IEnumerable<LeaveResponse>>> GetProfilsLeave()
        {
            return await RESTHelper.GetRequest<IEnumerable<LeaveResponse>>(url: $"{AppUrls.GetRequestsListProfilsLeave}", method: HttpVerbs.GET);

        }

        public async Task<RESTServiceResponse<IEnumerable<LeaveResponse>>> GetInProgressProfilsLeave()
        {
            return await RESTHelper.GetRequest<IEnumerable<LeaveResponse>>(url: $"{AppUrls.GetRequestInProgressProfilsLeave}", method: HttpVerbs.GET);
        }
        public async Task<RESTServiceResponse<IEnumerable<LeaveResponse>>> GetValidatedProfilsLeave()
        {
            return await RESTHelper.GetRequest<IEnumerable<LeaveResponse>>(url: $"{AppUrls.GetRequestValidatedProfilsLeave}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<LeaveResponse>>> GetProfilLeaves()
        {
            return await RESTHelper.GetRequest<IEnumerable<LeaveResponse>>(url: $"{AppUrls.GetRequestsListProfilLeaves}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<LeaveResponse>>> GetLeaves()
        {
            return await RESTHelper.GetRequest<IEnumerable<LeaveResponse>>(url: $"{AppUrls.GetRequestsListProfilLeaves}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<StatistiqueLeaveModel>> GetStatistics_ProfilLeaves()
        {
            return await RESTHelper.GetRequest<StatistiqueLeaveModel>(url: $"{AppUrls.GetStatistics_ProfilLeaves}", method: HttpVerbs.GET);
        }


        public async Task<RESTServiceResponse<IEnumerable<REFTypeLeave>>> GetTypesLeave()
        {
            return await RESTHelper.GetRequest<IEnumerable<REFTypeLeave>>(url: $"{AppUrls.GetRequestListTypeLeave}", method: HttpVerbs.GET);
        }
        public async Task<RESTServiceResponse<IEnumerable<ProjectModel>>> GetProjects()
        {
            return await RESTHelper.GetRequest<IEnumerable<ProjectModel>>(url: $"{AppUrls.GetRequestListProject}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProjectModel>>> GetAcualProjects()
        {
            return await RESTHelper.GetRequest<IEnumerable<ProjectModel>>(url: $"{AppUrls.GetRequestListActualProject}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProjectModel>>> GetProfilProjects()
        {
            return await RESTHelper.GetRequest<IEnumerable<ProjectModel>>(url: $"{AppUrls.GetRequestListProfilProject}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProfilResponse>>> GetProjectSquad(long projectId)
        {
            return await RESTHelper.GetRequest<IEnumerable<ProfilResponse>>(url: $"{AppUrls.GetRequestProjectSquad}?projectId={projectId}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProfilResponse>>> GetProjectStaffMembersToAdd(long projectId)
        {
            return await RESTHelper.GetRequest<IEnumerable<ProfilResponse>>(url: $"{AppUrls.GetStaffMembersToAddRequest}?projectId={projectId}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProfilResponse>>> GetProfils()
        {
            return await RESTHelper.GetRequest<IEnumerable<ProfilResponse>>(url: $"{AppUrls.GetRequestProfils}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ProfilResponse>>> GetOwners()
        {
            return await RESTHelper.GetRequest<IEnumerable<ProfilResponse>>(url: $"{AppUrls.GetRequestOwners}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<SituationProject>>> GetSituationsProject()
        {
            return await RESTHelper.GetRequest<IEnumerable<SituationProject>>(url: $"{AppUrls.GetRequestSituationProject}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<CertaficateResponse>>> GetAllCertificates()
        {
            return await RESTHelper.GetRequest<IEnumerable<CertaficateResponse>>(url: $"{AppUrls.GetRequestAllCertaficates}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<CertaficateResponse>>> GetProfilCertificates()
        {
            return await RESTHelper.GetRequest<IEnumerable<CertaficateResponse>>(url: $"{AppUrls.GetRequestProfilCertificates}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<TypeCertaficate>>> GetTypeCertificates()
        {
            return await RESTHelper.GetRequest<IEnumerable<TypeCertaficate>>(url: $"{AppUrls.GetRequestTypeCertificates}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<object>> PostLeave(LeaveModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostLeaveRequest}",postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<IEnumerable<DisplacementResponse>>> GetProfilDeplacement()
        {
            return await RESTHelper.GetRequest<IEnumerable<DisplacementResponse>>(url: $"{AppUrls.GetProfilDeplacement}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<DisplacementResponse>>> GetAllDeplacement()
        {
            return await RESTHelper.GetRequest<IEnumerable<DisplacementResponse>>(url: $"{AppUrls.GetAllDeplacement}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ComplaintResponse>>> GetProfilComplaint()
        {
            return await RESTHelper.GetRequest<IEnumerable<ComplaintResponse>>(url: $"{AppUrls.GetProfilClaims}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<IEnumerable<ComplaintResponse>>> GetAllComplaint()
        {
            return await RESTHelper.GetRequest<IEnumerable<ComplaintResponse>>(url: $"{AppUrls.GetAllClaims}", method: HttpVerbs.GET);
        }

        public async Task<RESTServiceResponse<int>> GetSumPoints()
        {
            return await RESTHelper.GetRequest<int>(url: $"{AppUrls.GetSumPoints}", method: HttpVerbs.GET);
        }

        //public async Task<RESTServiceResponse<object>> PostProject(ProjectRequest postParams)
        //{
        //    return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostProjectRequest}", postObject: postParams, method: HttpVerbs.POST);
        //}

        public async Task<RESTServiceResponse<object>> PostProject(ProjectRequest postParams)
        {
            return await RESTHelper.UploadAdministratifPjAsync(postParams);
        }
        public async Task<RESTServiceResponse<object>> PostCertaficateTreatement(CertaficateTreatementRequest postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostCertaficateTreatement}", postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<object>> PostMembers(AddMembersRequest postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostMembersRequest}", postObject: postParams, method: HttpVerbs.POST);
        }
        public async Task<RESTServiceResponse<object>> PostChangePercent(ChangePercentModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostChangePercentRequest}", postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<object>> DeleteMemberFromProjet(DeleteMemeberProjectModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.DeleteMemberProject}", postObject: postParams, method: HttpVerbs.POST);
        }




        public async Task<RESTServiceResponse<object>> DeleteLeave(DeleteLeave postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.DeleteLeaveRequest}", postObject: postParams, method: HttpVerbs.POST);

        }

        public async Task<RESTServiceResponse<object>> PostUpdateLeave(UpdateLeaveModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostUpdateLeaveRequest}", postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<object>> PostCertificate(CertaficateModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostDemandCertaficate}", postObject: postParams, method: HttpVerbs.POST);
        }
        public async Task<RESTServiceResponse<object>> TraitementDemandCertificate(CertaficateTreatementRequest postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostTraitementDemandCertificate}", postObject: postParams, method: HttpVerbs.POST);
        }


        public async Task<RESTServiceResponse<object>> PostDisplacement(DisplacementModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostDisplacementRequest}", postObject: postParams, method: HttpVerbs.POST);
        }
        public async Task<RESTServiceResponse<object>> PosteUpdateDisplacement(UpdateDeplacementModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostUpdateDeplacement}", postObject: postParams, method: HttpVerbs.POST);
        }

        public async Task<RESTServiceResponse<object>> PostComplaint(ComplaintModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostClaimRequest}", postObject: postParams, method: HttpVerbs.POST);
        }
        public async Task<RESTServiceResponse<object>> PosteUpdateComplaint(ComplaintTraitement postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.PostTraitementClaimRequest}", postObject: postParams, method: HttpVerbs.POST);
        }
        public async Task<RESTServiceResponse<object>> GranttPoints(PointModel postParams)
        {
            return await RESTHelper.GetRequest<object>(url: $"{AppUrls.GranttPoints}", postObject: postParams, method: HttpVerbs.POST);
        }
  


    }


}
