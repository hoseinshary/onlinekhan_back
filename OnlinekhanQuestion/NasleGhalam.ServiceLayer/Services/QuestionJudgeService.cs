using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionJudge;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionJudgeService
    {
        private const string Title = "کارشناسی سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionJudge> _questionJudges;
        private readonly IDbSet<Question> _questions;
        private readonly IDbSet<Lookup> _lookups;

        private readonly Lazy<QuestionGroupService> _questionGroupService;
        private readonly Lazy<QuestionService> _questionService;

        private int NumberOfJudges = 1;

        public QuestionJudgeService(IUnitOfWork uow, Lazy<QuestionGroupService> questionGroupService, Lazy<QuestionService> questionService)
        {
            _uow = uow;
            _questionJudges = uow.Set<QuestionJudge>();
            _questions = uow.Set<Question>();
            _lookups = uow.Set<Lookup>();

            _questionGroupService = questionGroupService;
            _questionService = questionService;


        }


        /// <summary>
        /// گرفتن  کارشناسی سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionJudgeViewModel GetById(int id)
        {
            return _questionJudges
                .Include(current => current.Lookup_QuestionHardnessType)
                .Include(current => current.Lookup_RepeatnessType)
                .Include(current => current.User)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionJudgeViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// تعداد همه   کارشناسی سوالات 
        /// </summary>
        /// <returns></returns>
        public int CountAll()
        {
            return _questionJudges
                .Count();
        }

        /// <summary>
        /// گرفتن همه کارشناسی سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionJudgeViewModel> GetAllByQuestionId(int questionId, int userid, int rollLevel)
        {
            if (rollLevel < 3)
            {
                return _questionJudges
                    .Include(current => current.Lookup_QuestionHardnessType)
                    .Include(current => current.Lookup_RepeatnessType)
                    .Include(current => current.User)
                    .Where(current => current.QuestionId == questionId)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionJudgeViewModel>)
                    .ToList();
            }
            else
            {
                return _questionJudges
                    .Include(current => current.Lookup_QuestionHardnessType)
                    .Include(current => current.Lookup_RepeatnessType)
                    .Include(current => current.User)
                    .Where(current => current.QuestionId == questionId)
                    .Where(current =>  current.UserId == userid)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionJudgeViewModel>)
                    .ToList();
            }
            
        }

        //public ClientMessageResult CorrectAllJudges()
        //{
        //    int count = 0;
        //    foreach (var item in _questions.ToList())
        //    {
        //        count++;
        //        if(count < 5600)
        //        { continue; }
        //        try
        //        {
        //            SetNumberOfjudges(item.Id);
        //            if (_questionJudges.Count(current => current.QuestionId == item.Id) >=
        //                    NumberOfJudges)
        //            {
        //                var questionJudges = _questionJudges
        //                    .Where(current => current.QuestionId == item.Id)
        //                    .Include(current => current.Lookup_QuestionHardnessType)
        //                    .Include(current => current.Lookup_RepeatnessType)
        //                    .Include(current => current.Lookup_QuestionRank)
        //                    .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

        //                double lookup_questionhardness = 0;
        //                double lookup_repeatness = 0;
        //                double lookup_questionRank = 0;
        //                int count_isStandard = 0;
        //                int count_isDelete = 0;
        //                int count_isUpdate = 0;
        //                int count_isLearning = 0;
        //                int count_isActiveQuestion = 0;
        //                int count_isActiveQuestionAnswer = 0;
        //                int responseTime = 0;

        //                foreach (var judge in questionJudges)
        //                {
        //                    if (judge.IsDelete == true)
        //                        count_isDelete++;
        //                    if (judge.IsUpdate == true)
        //                        count_isUpdate++;
        //                    if (judge.IsStandard == true)
        //                        count_isStandard++;
        //                    if (judge.IsLearning == true)
        //                        count_isLearning++;
        //                    if (judge.IsActiveQuestion == true)
        //                        count_isActiveQuestion++;
        //                    if (judge.IsActiveQuestionAnswer == true)
        //                        count_isActiveQuestionAnswer++;

        //                    lookup_questionhardness += judge.Lookup_QuestionHardnessType.State;
        //                    lookup_repeatness += judge.Lookup_RepeatnessType.State;
        //                    lookup_questionRank += judge.Lookup_QuestionRank.State;

        //                    responseTime += judge.ResponseSecond;
        //                }

        //                var updateQuestion = _questions.Include(x => x.QuestionAnswers)
        //                    .First(x => x.Id == item.Id);
        //                updateQuestion.ResponseSecond = Convert.ToInt16(responseTime / NumberOfJudges);
        //                if (count_isStandard > NumberOfJudges / 2)
        //                    updateQuestion.IsStandard = true;
        //                else
        //                    updateQuestion.IsStandard = false;

        //                if (count_isLearning > NumberOfJudges / 2)
        //                    updateQuestion.IsLearning = true;
        //                else
        //                    updateQuestion.IsLearning = false;

        //                if (count_isDelete > NumberOfJudges / 2)
        //                    updateQuestion.IsDelete = true;
        //                else
        //                    updateQuestion.IsDelete = false;

        //                if (count_isUpdate > NumberOfJudges / 2)
        //                    updateQuestion.IsUpdate = true;
        //                else
        //                    updateQuestion.IsUpdate = false;
        //                if (count_isActiveQuestion > NumberOfJudges / 2)
        //                    updateQuestion.IsActive = true;
        //                else
        //                    updateQuestion.IsActive = false;
        //                if (updateQuestion.QuestionAnswers.Any())
        //                {
        //                    if (count_isActiveQuestionAnswer > NumberOfJudges / 2)
        //                        updateQuestion.QuestionAnswers.FirstOrDefault(x => x.IsMaster == true).IsActive = true;
        //                    else
        //                        updateQuestion.QuestionAnswers.FirstOrDefault(x => x.IsMaster == true).IsActive = false;
        //                }

        //                updateQuestion.LookupId_QuestionHardnessType = _lookups
        //                    .First(x => x.Name == "QuestionHardnessType" &&
        //                                x.State == (int)Math.Round(lookup_questionhardness / NumberOfJudges))
        //                    .Id;

        //                updateQuestion.LookupId_RepeatnessType = _lookups
        //                    .First(x => x.Name == "RepeatnessType" &&
        //                                x.State == (int)Math.Round(lookup_repeatness / NumberOfJudges))
        //                    .Id;

        //                updateQuestion.LookupId_QuestionRank = _lookups
        //                    .First(x => x.Name == "QuestionRank" &&
        //                                x.State == (int)Math.Round(lookup_questionRank / NumberOfJudges))
        //                    .Id;

        //                _uow.MarkAsChanged(updateQuestion);
        //                _uow.ValidateOnSaveEnabled(false);
        //            }
        //        }
        //        catch { }
                    
        //        if (count % 100 == 0 && count != 0)
        //        {
        //            var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
        //        }
        //    }

        //var clientResult = Mapper.Map<ClientMessageResult>(null);
        //    if (clientResult.MessageType == MessageType.Success)
        //        clientResult.Obj = count.ToString();
        //    return clientResult;
        //}
            /// <summary>
            /// ثبت کارشناسی سوال
            /// </summary>
            /// <param name="questionJudgeViewModel"></param>
            /// <param name="userId"></param>
            /// <returns></returns>
            public ClientMessageResult Create(QuestionJudgeCreateViewModel questionJudgeViewModel, int userId)
        {
            var question = _questions.Where(x => x.Id == questionJudgeViewModel.QuestionId)
                .Include(x => x.QuestionJudges).FirstOrDefault();

            if (question.QuestionJudges.Any(x => x.UserId == userId && x.EducationGroup == questionJudgeViewModel.EducationGroup))
            {
                foreach (var questionQuestionJudge in question.QuestionJudges)
                {
                    if (questionQuestionJudge.EducationGroup == questionJudgeViewModel.EducationGroup)
                    {
                        return new ClientMessageResult()
                        {
                            Message = $"کارشناسان اجازه ثبت بیش از یک کارشناسی ندارند!",
                            MessageType = MessageType.Error
                        };
                    }
                }
            }



            SetNumberOfjudges(questionJudgeViewModel.QuestionId);
            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            questionJudge.UserId = userId;
            _questionJudges.Add(questionJudge);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            if (serverResult.MessageType == MessageType.Success)
            {
                if (_questionJudges.Count(current => current.QuestionId == questionJudgeViewModel.QuestionId) >=
                    NumberOfJudges)
                {
                    var questionJudges = _questionJudges
                        .Where(current => current.QuestionId == questionJudgeViewModel.QuestionId)
                        .Include(current => current.Lookup_QuestionHardnessType)
                        .Include(current => current.Lookup_RepeatnessType)
                        .Include(current => current.Lookup_QuestionRank)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    double lookup_questionhardness = 0;
                    double lookup_repeatness = 0;
                    double lookup_questionRank = 0;
                    int count_isStandard = 0;
                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isLearning = 0;
                    int count_isActiveQuestion = 0;
                    int count_isActiveQuestionAnswer = 0;
                    int responseTime = 0;

                    foreach (var judge in questionJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsStandard == true)
                            count_isStandard++;
                        if (judge.IsLearning == true)
                            count_isLearning++;
                        if (judge.IsActiveQuestion == true)
                            count_isActiveQuestion++;
                        if (judge.IsActiveQuestionAnswer == true)
                            count_isActiveQuestionAnswer++;

                        lookup_questionhardness += judge.Lookup_QuestionHardnessType.State;
                        lookup_repeatness += judge.Lookup_RepeatnessType.State;
                        lookup_questionRank += judge.Lookup_QuestionRank.State;

                        responseTime += judge.ResponseSecond;
                    }

                    var updateQuestion = _questions.Include(x => x.QuestionAnswers)
                        .First(x => x.Id == questionJudgeViewModel.QuestionId);
                    updateQuestion.ResponseSecond = Convert.ToInt16(responseTime / NumberOfJudges);
                    if (count_isStandard > NumberOfJudges / 2)
                        updateQuestion.IsStandard = true;
                    else
                        updateQuestion.IsStandard = false;

                    if (count_isLearning > NumberOfJudges / 2)
                        updateQuestion.IsLearning = true;
                    else
                        updateQuestion.IsLearning = false;

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestion.IsDelete = true;
                    else
                        updateQuestion.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestion.IsUpdate = true;
                    else
                        updateQuestion.IsUpdate = false;
                    if (count_isActiveQuestion > NumberOfJudges / 2)
                        updateQuestion.IsActive = true;
                    else
                        updateQuestion.IsActive = false;
                    if (updateQuestion.QuestionAnswers.Any())
                    {
                        if (count_isActiveQuestionAnswer > NumberOfJudges / 2)
                            updateQuestion.QuestionAnswers.FirstOrDefault(x => x.IsMaster == true).IsActive = true;
                        else
                            updateQuestion.QuestionAnswers.FirstOrDefault(x => x.IsMaster == true).IsActive = false;
                    }

                    updateQuestion.LookupId_QuestionHardnessType = _lookups
                        .First(x => x.Name == "QuestionHardnessType" &&
                                    x.State == (int)Math.Round(lookup_questionhardness / NumberOfJudges))
                        .Id;

                    updateQuestion.LookupId_RepeatnessType = _lookups
                        .First(x => x.Name == "RepeatnessType" &&
                                    x.State == (int)Math.Round(lookup_repeatness / NumberOfJudges))
                        .Id;

                    updateQuestion.LookupId_QuestionRank = _lookups
                        .First(x => x.Name == "QuestionRank" &&
                                    x.State == (int)Math.Round(lookup_questionRank / NumberOfJudges))
                        .Id;

                    _uow.MarkAsChanged(updateQuestion);
                    _uow.ValidateOnSaveEnabled(false);
                    var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
                }
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionJudge.Id);
            return clientResult;

        }


        /// <summary>
        /// ویرایش کارشناسی سوال
        /// </summary>
        /// <param name="questionJudgeViewModel"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionJudgeUpdateViewModel questionJudgeViewModel, int userId)
        {
            SetNumberOfjudges(questionJudgeViewModel.QuestionId);
            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            questionJudge.UserId = userId;
            _uow.MarkAsChanged(questionJudge);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success)
            {
                if (_questionJudges.Count(current => current.QuestionId == questionJudgeViewModel.QuestionId) >= NumberOfJudges)
                {
                    var questionJudges = _questionJudges
                        .Where(current => current.QuestionId == questionJudgeViewModel.QuestionId)
                        .Include(current => current.Lookup_QuestionHardnessType)
                        .Include(current => current.Lookup_RepeatnessType)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    double lookup_questionhardness = 0;
                    double lookup_repeatness = 0;
                    int count_isStandard = 0;
                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isLearning = 0;
                    int count_isActiveQuestion = 0;
                    int count_isActiveQuestionAnswer = 0;
                    int responseTime = 0;

                    foreach (var judge in questionJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsStandard == true)
                            count_isStandard++;
                        if (judge.IsLearning == true)
                            count_isLearning++;
                        if (judge.IsActiveQuestion == true)
                            count_isActiveQuestion++;
                        if (judge.IsActiveQuestionAnswer == true)
                            count_isActiveQuestionAnswer++;

                        lookup_questionhardness += judge.Lookup_QuestionHardnessType.State;
                        lookup_repeatness += judge.Lookup_RepeatnessType.State;

                        responseTime += judge.ResponseSecond;
                    }

                    var updateQuestion = _questions.Include(x => x.QuestionAnswers).First(x => x.Id == questionJudgeViewModel.QuestionId);
                    updateQuestion.ResponseSecond = Convert.ToInt16(responseTime / NumberOfJudges);
                    if (count_isStandard > NumberOfJudges / 2)
                        updateQuestion.IsStandard = true;
                    else
                        updateQuestion.IsStandard = false;

                    if (count_isLearning > NumberOfJudges / 2)
                        updateQuestion.IsLearning = true;
                    else
                        updateQuestion.IsLearning = false;

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestion.IsDelete = true;
                    else
                        updateQuestion.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestion.IsUpdate = true;
                    else
                        updateQuestion.IsUpdate = false;

                    if (count_isActiveQuestion > NumberOfJudges / 2)
                        updateQuestion.IsActive = true;
                    else
                        updateQuestion.IsActive = false;

                    if (count_isActiveQuestionAnswer > NumberOfJudges / 2)
                        updateQuestion.QuestionAnswers.First(x => x.IsMaster == true).IsActive = true;
                    else
                        updateQuestion.QuestionAnswers.First(x => x.IsMaster == true).IsActive = false;

                    updateQuestion.LookupId_QuestionHardnessType = _lookups
                        .First(x => x.Name == "QuestionHardnessType" && x.State == (int)Math.Round(lookup_questionhardness / NumberOfJudges))
                        .Id;

                    updateQuestion.LookupId_RepeatnessType = _lookups
                        .First(x => x.Name == "RepeatnessType" && x.State == (int)Math.Round(lookup_repeatness / NumberOfJudges))
                        .Id;

                    _uow.MarkAsChanged(updateQuestion);
                    _uow.ValidateOnSaveEnabled(false);
                    var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
                }
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionJudge.Id);
            return clientResult;
        }


        /// <summary>
        /// حذف کارشناسی سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var questionJudgeViewModel = GetById(id);
            if (questionJudgeViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionJudge = Mapper.Map<QuestionJudge>(questionJudgeViewModel);
            _uow.MarkAsDeleted(questionJudge);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);


            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }



        /// <summary>
        /// یافت تعدا کارشناس این درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void SetNumberOfjudges(int questionId)
        {
            NumberOfJudges = _questionService.Value.GetNumberOfjudges(questionId);
        }


    }
}
