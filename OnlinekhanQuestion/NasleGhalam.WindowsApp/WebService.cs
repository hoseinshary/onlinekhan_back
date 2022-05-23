using NasleGhalam.Common;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionAnswer;

namespace NasleGhalam.WindowsApp
{
    public class WebService : IDisposable
    {
        private readonly RestClient _client;

        public WebService()
        {
            _client = new RestClient { BaseUrl = new Uri(ConstantSettings.ApiUrl) };
            
        }

        #region ### User ###
        public async Task<LoginResultViewModel> Login(LoginViewModel login)
        {
            try
            {
                _client.Timeout = -1;
                var request = new RestRequest("User/Login", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(login), ParameterType.RequestBody);
                IRestResponse response = _client.Execute(request);

                var resultObject1 = JsonConvert.DeserializeObject<LoginResultViewModel>(response.Content);
                SetToken(resultObject1.Token); // set token
                return resultObject1;
            }
            catch (Exception e)
            {
                LogWriter.LogException(e);
            }

            return new LoginResultViewModel();
        }

        public void SetToken(string token)
        {
            _client.AddDefaultHeader("Token", token);
        }
        #endregion


     //   #region ### FavoriteProduct ###
        public async Task<ResponseObject<QuestionGroupViewModel>> QuestionGrounCreate(QuestionGroupCreateViewModel questionGroup)
        {
            try
            {
                _client.Timeout = -1;
                var request = new RestRequest("QuestionGroup/CreateForWindowsApp", Method.POST);
                request.AddParameter("Title", questionGroup.Title , ParameterType.QueryString);
                request.AddParameter("LessonId", questionGroup.LessonId , ParameterType.QueryString);
               
                request.AddFile("word", questionGroup.QuestionGroupWordPath);
                request.AddFile("excel", questionGroup.QuestionGroupExcelPath);
                IRestResponse response = _client.Execute(request);
                var resultObject1 = JsonConvert.DeserializeObject<ResponseObject<QuestionGroupViewModel>>(response.Content);
                return resultObject1;
            }
            catch (Exception e)
            {
                LogWriter.LogException(e);
            }
            return new ResponseObject<QuestionGroupViewModel>();
        }


        public async Task<ResponseObject<QuestionViewModel>> QuestionCreate(QuestionCreateWindowsViewModel question)
        {
            var content = "";
            try
            {
                _client.Timeout = -1;
                var request = new RestRequest("Question/CreateForWindowsApp", Method.POST);

                request.AddParameter("QuestionNumber", question.QuestionNumber);
                request.AddParameter("QuestionPoint", question.QuestionPoint);
                request.AddParameter("UseEvaluation", question.UseEvaluation);
                request.AddParameter("IsStandard", question.IsStandard);
                request.AddParameter("WriterId", question.WriterId);
                request.AddParameter("SupervisorUserId", question.SupervisorUserId);
                request.AddParameter("ResponseSecond", question.ResponseSecond);
                request.AddParameter("Description", question.Description);
                request.AddParameter("AnswerNumber", question.AnswerNumber);
                request.AddParameter("LookupId_QuestionType", question.LookupId_QuestionType);
                request.AddParameter("LookupId_QuestionHardnessType", question.LookupId_QuestionHardnessType);
                request.AddParameter("LookupId_RepeatnessType", question.LookupId_RepeatnessType);
                request.AddParameter("LookupId_AuthorType", question.LookupId_AuthorType);
                request.AddParameter("LookupId_AreaType", question.LookupId_AreaTypes);
                request.AddParameter("IsHybrid", question.IsHybrid);
                request.AddParameter("LookupId_QuestionRank", question.LookupId_QuestionRank);
                request.AddParameter("Context", question.Context);
                request.AddParameter("QuestionGroupId", question.QuestionGroupId);
                request.AddParameter("WordBase64File", question.WordBase64File);
                request.AddParameter("PngBase64File", question.PngBase64File);

                //request.AddFile("word", question.FilePath+".docx");
                //request.AddFile("png", question.FilePath+".png");
                IRestResponse response = _client.Execute(request);
                content = response.Content;
                var resultObject1 = JsonConvert.DeserializeObject<ResponseObject<QuestionViewModel>>(content);
                return resultObject1;
            }
            catch (Exception e)
            {
                LogWriter.LogException(content);
                LogWriter.LogException(e);
            }
            return new ResponseObject<QuestionViewModel>();
        }

        public async Task<ResponseObject<QuestionAnswerViewModel>> QuestionAnswerCreate(QuestionAnswerCreateViewModel question)
        {
            try
            {
                _client.Timeout = -1;
                var request = new RestRequest("QuestionAnswer/CreateForWindowsApp", Method.POST);

                request.AddParameter("Title", question.Title);
                request.AddParameter("LessonName", question.LessonName);
                request.AddParameter("Description", question.Description);
                request.AddParameter("WriterId", question.WriterId);
                request.AddParameter("IsMaster", question.IsMaster);
                request.AddParameter("IsActive", question.IsActive);
                request.AddParameter("QuestionId", question.QuestionId);
                request.AddParameter("FilePath", question.FilePath);
                request.AddParameter("FileName", question.FileName);
                request.AddParameter("Context", question.Context);
                request.AddParameter("WordBase64File", question.WordBase64File);
                request.AddParameter("PngBase64File", question.PngBase64File);

                //request.AddFile("word", question.FileName + ".docx");
                //request.AddFile("png", question.FileName + ".png");
                IRestResponse response = _client.Execute(request);
                var resultObject1 = JsonConvert.DeserializeObject<ResponseObject<QuestionAnswerViewModel>>(response.Content);
                return resultObject1;
            }
            catch (Exception e)
            {
                LogWriter.LogException(e);
            }
            return new ResponseObject<QuestionAnswerViewModel>();
        }

        //public async Task<List<FavoriteProductViewModel>> FavoriteProductList()
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync("FavoriteProduct/List");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductViewModel>> FavoriteProductCreate(string productLink, string productName, long price, long target)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            productName,
        //            productLink,
        //            price,
        //            target
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProduct/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductViewModel>> FavoriteProductUpdate(Guid id, long price, long target, bool disable)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            id,
        //            price,
        //            target,
        //            disable
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProduct/Update", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductViewModel>();
        //}
        //#endregion

        //#region ### FavoriteProductEvent ###
        //public async Task<ResponseObject<FavoriteProductEventViewModel>> FavoriteProductEventDetail(Guid id)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductEvent/{id}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductEventViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductEventViewModel>();
        //}

        //public async Task<List<FavoriteProductEventViewModel>> FavoriteProductEventList(Guid favoriteProductId)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductEvent/List/{favoriteProductId}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductEventViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductEventViewModel>();
        //}

        //public async Task<ResponseObject<FavoriteProductEventViewModel>> FavoriteProductEventCreate(Guid favoriteProductId, string eventName, bool isDone, long price, long target)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            Event = eventName,
        //            isSuccessful = isDone,
        //            price,
        //            target,
        //            favoriteProductId
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("FavoriteProductEvent/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<FavoriteProductEventViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<FavoriteProductEventViewModel>();
        //}

        //public async Task<List<ResponseObject<FavoriteProductEventViewModel>>> FavoriteProductEventCreate(List<FavoriteProductViewModel> favoriteProducts, string eventName, bool isDone)
        //{
        //    var responseList = new List<ResponseObject<FavoriteProductEventViewModel>>();
        //    foreach (var favoriteProduct in favoriteProducts)
        //    {
        //        try
        //        {
        //            var result = await FavoriteProductEventCreate(favoriteProduct.Id, eventName, isDone,
        //                favoriteProduct.Price, favoriteProduct.Target);
        //            responseList.Add(result);
        //        }
        //        catch (Exception e)
        //        {
        //            LogWriter.LogException(e);
        //        }
        //    }

        //    return responseList;
        //}
        //#endregion

        //#region ### FavoriteProductPrice ###
        //public async Task<List<FavoriteProductPriceViewModel>> FavoriteProductPriceList(Guid favoriteProductId)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"FavoriteProductPrice/List/{favoriteProductId}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<FavoriteProductPriceViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<FavoriteProductPriceViewModel>();
        //}
        //#endregion

        //#region ### UserSummary ###
        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryDetail(Guid id)
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync($"UserSummary/{id}");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}

        //public async Task<List<UserSummaryViewModel>> UserSummaryList()
        //{
        //    try
        //    {
        //        var response = await _client.GetAsync("UserSummary/List");
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<List<UserSummaryViewModel>>>(result).Data;
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new List<UserSummaryViewModel>();
        //}

        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryCreate()
        //{
        //    try
        //    {
        //        var strContent = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("UserSummary/Create", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}

        //public async Task<ResponseObject<UserSummaryViewModel>> UserSummaryUpdate(Guid id, int countOfCrawl)
        //{
        //    try
        //    {
        //        var data = new
        //        {
        //            id,
        //            countOfCrawl
        //        };
        //        var strContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        //        var response = await _client.PostAsync("UserSummary/Update", strContent);
        //        var result = await response.Content.ReadAsStringAsync();

        //        return JsonConvert.DeserializeObject<ResponseObject<UserSummaryViewModel>>(result);
        //    }
        //    catch (Exception e)
        //    {
        //        LogWriter.LogException(e);
        //    }

        //    return new ResponseObject<UserSummaryViewModel>();
        //}
        //#endregion

        public void Dispose()
        {
         //   _client?.Dispose();
        }
    }
}
