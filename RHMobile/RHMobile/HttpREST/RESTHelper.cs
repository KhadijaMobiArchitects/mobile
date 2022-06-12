using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XForms.Constants;
using XForms.Enum;
using XForms.Models;

namespace XForms.HttpREST
{
    public static class RESTHelper
    {
        public static async Task<RESTServiceResponse<T>> GetRequest<T>(string url, HttpVerbs method = HttpVerbs.GET, System.Collections.Specialized.NameValueCollection getParams = null, object postObject = null, bool isNeedAcces = true, string contentType = "application/json")
        {
            try
            {
                 //Skip Certificate Validation
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                using (var client = new HttpClient(clientHandler))
                {
                    Uri uri = new Uri(url);
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    if (isNeedAcces)
                    {
                        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppPreferences.Token);
                    }

                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (method)
                    {
                        case HttpVerbs.GET:
                            response = await client.GetAsync(uri);
                            break;
                        case HttpVerbs.POST:
                            var content = new StringContent(JsonConvert.SerializeObject(postObject), Encoding.UTF8, contentType);
                            response = await client.PostAsync(uri, content);
                            break;
                        case HttpVerbs.DELETE:
                            response = await client.DeleteAsync(uri);
                            break;
                        default:
                            break;
                    }

                    var stringResponseJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(stringResponseJson);

                    return result;
                }
            }
            catch(Exception Ex)
            {
                //return new RESTServiceResponse<T>(false, Ex.Message);
                return null;
            }
        }
        //public static async Task<RESTServiceResponse<T>> UploadFileAsync<T>(string url, Models.File fileData, Dictionary<string, string> stringContent = null)
        //{
        //    try
        //    {
        //        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

        //        using (var client = new HttpClient())
        //        {
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppPreferences.Token);

        //            MultipartFormDataContent formData = new MultipartFormDataContent();
        //            HttpContent fileStreamContent = new StreamContent(fileData.Stream);

        //            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
        //            {
        //                Name = "FileByte",
        //                //FileName = fileData.Name
        //            };
        //            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

        //            formData.Add(fileStreamContent);

        //            if (stringContent != null)
        //            {
        //                foreach (var item in stringContent)
        //                {
        //                    formData.Add(new StringContent(item.Value), item.Key);
        //                }
        //            }

        //            var response = await client.PostAsync(url, formData);

        //            var responseMessage = await response.Content.ReadAsStringAsync();

        //            RESTServiceResponse<T> result = JsonConvert.DeserializeObject<RESTServiceResponse<T>>(responseMessage);

        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new RESTServiceResponse<T>(false, ex.Message);
        //    }
        //}

        
        public static async Task<RESTServiceResponse<object>> UploadAdministratifPjAsync(ProjectRequest fileData)
        {
            //#region IsConnected
            //if (!AppHelpers.IsConnected())
            //{
            //    //Snack("Vous n'êtes pas connéctés !");
            //    return new RESTServiceResponse<object>(false, "Vous n'êtes pas connéctés !");
            //}
            //#endregion

            //if (!AppHelper.IsPJFileAuthorized(fileData.FileExtension))
            //{
            //    AppsHelper.Snack("Le format du fichier non supporté");
            //    return new RESTServiceResponse<object>(false, "Le format du fichier non supporté");
            //    //return true;
            //}

            var stream = System.IO.File.OpenRead(fileData.ProjectFile.Path);

            //var acceptedSize = (long)Constant.MAX_FILE_UPLOAD_WS * 1024 * 1024 * 1024;//convert to Mo

            //if (stream.Length > acceptedSize)
            //{
            //    Debug.WriteLine("la taille de fichier doit pas dépasser 10 mo");
            //    //return;
            //}

            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (var client = new HttpClient(clientHandler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppPreferences.Token);

                MultipartFormDataContent formData = new MultipartFormDataContent();
                HttpContent fileStreamContent = new StreamContent(stream);

                fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "Picture",
                    FileName = fileData.ProjectFile.Name
                };
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                formData.Add(fileStreamContent);

                formData.Add(new StringContent(fileData.ProjectName),"ProjectName");
                formData.Add(new StringContent(fileData.StartedAt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")), "startedAt");
                formData.Add(new StringContent(fileData.EndedAt.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")), "EndedAt");
                formData.Add(new StringContent(fileData.OwnerBy), "ownerBy");
                formData.Add(new StringContent(fileData.members), "members");

                var response = await client.PostAsync(AppUrls.PostProjectRequest, formData);

                var responseMessage = await response.Content.ReadAsStringAsync();

                RESTServiceResponse<object> result = JsonConvert.DeserializeObject<RESTServiceResponse<object>>(responseMessage);

                return new RESTServiceResponse<object>(result.succeeded, result.message);
            }
        }
        //public static async Task<RESTServiceResponse<object>> UploadAdministratifCertaficateAsync(CertaficateTreatementRequest fileData)
        //{

        //    var stream = System.IO.File.OpenRead(fileData.Document.Path);

        //    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });

        //    HttpClientHandler clientHandler = new HttpClientHandler();
        //    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        //    using (var client = new HttpClient(clientHandler))
        //    {
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + AppPreferences.Token);

        //        MultipartFormDataContent formData = new MultipartFormDataContent();
        //        HttpContent fileStreamContent = new StreamContent(stream);

        //        fileStreamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //        {
        //            Name = "Document",
        //            FileName = fileData.Document.Name
        //        };
        //        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

        //        formData.Add(fileStreamContent);

        //        formData.Add(new StringContent(fileData.Id.ToString()), "d");

        //        var response = await client.PostAsync(AppUrls.PostTraitementDemandCertificate, formData);

        //        var responseMessage = await response.Content.ReadAsStringAsync();

        //        RESTServiceResponse<object> result = JsonConvert.DeserializeObject<RESTServiceResponse<object>>(responseMessage);

        //        return new RESTServiceResponse<object>(result.succeeded, result.message);

        //    }
        //}


        #region GetRequest Json
        public static async Task<T> GetRequestJson<T>(string url, HttpVerbs method = HttpVerbs.GET, System.Collections.Specialized.NameValueCollection getParams = null, object postObject = null, string contentType = "application/json")
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //setup client
                    Uri uri = new Uri(url);
                    #region Setting Attachements

                    //if (method == HttpVerbs.GET && getParams != null)
                    //{
                    //    uri = uri.AttachParameters(getParams);
                    //}

                    #endregion
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.AccessToken);
                    //client.DefaultRequestHeaders.TryAddWithoutValidation("From", AppUrls.AppHash);

                    //make request
                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (method)
                    {
                        case HttpVerbs.GET:
                            response = await client.GetAsync(uri);
                            break;
                        case HttpVerbs.POST:
                            var content = new StringContent(JsonConvert.SerializeObject(postObject), Encoding.UTF8, contentType);
                            response = await client.PostAsync(uri, content);
                            break;
                        default:
                            break;
                    }

                    var stringResponseJson = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<T>(stringResponseJson);

                    return result;
                }
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        #endregion
    }
}
