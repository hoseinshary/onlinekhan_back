using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionAnswerJudge;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionAnswerJudgeService
    {
        private const string Title = "کارشناسی جواب سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionAnswerJudge> _questionAnswerJudges;
        private readonly IDbSet<QuestionAnswer> _questionAnswer;

        private readonly Lazy<QuestionService> _questionService;

        private int NumberOfJudges = 1;

        public QuestionAnswerJudgeService(IUnitOfWork uow, Lazy<QuestionService> questionService)
        {
            _uow = uow;
            _questionAnswerJudges = uow.Set<QuestionAnswerJudge>();
            _questionAnswer = uow.Set<QuestionAnswer>();
            _questionService = questionService;
        }

        /// <summary>
        /// گرفتن  کارشناسی جواب سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionAnswerJudgeViewModel GetById(int id)
        {
            return _questionAnswerJudges
                .Include(current => current.User)
                .Include(current => current.Lookup_ReasonProblem)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerJudgeViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه کارشناسی جواب سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionAnswerJudgeViewModel> GetAllByQuestionAnswerId(int questionAnswerId, int userid, int rollLevel)
        {
            if (rollLevel < 3)
            {
                return _questionAnswerJudges
                .Include(current => current.User)
                .Include(current => current.Lookup_ReasonProblem)
                .Where(current => current.QuestionAnswerId == questionAnswerId)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerJudgeViewModel>)
                .ToList();
            }
            else
            {
                return _questionAnswerJudges
                    .Include(current => current.User)
                    .Include(current => current.Lookup_ReasonProblem)
                    .Where(current => current.QuestionAnswerId == questionAnswerId)
                    .Where(current => current.UserId == userid)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionAnswerJudgeViewModel>)
                    .ToList();
            }
        }

        /// <summary>
        /// ثبت کارشناسی جواب سوال
        /// </summary>
        /// <param name="questionAnswerJudgeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionAnswerJudgeCreateViewModel questionAnswerJudgeViewModel)
        {
            var questionAnswer = _questionAnswer.Where(x => x.Id == questionAnswerJudgeViewModel.QuestionAnswerId)
                .Include(x => x.QuestionAnswerJudges).FirstOrDefault();

            if (questionAnswer.QuestionAnswerJudges.Any(x => x.UserId == questionAnswerJudgeViewModel.UserId))
            {
                return new ClientMessageResult()
                {
                    Message = $"کارشناسان اجازه ثبت بیش از یک کارشناسی ندارند!",
                    MessageType = MessageType.Error
                };
            }

            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _questionAnswerJudges.Add(questionAnswerJudge);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                if (_questionAnswerJudges.Count(current => current.QuestionAnswerId == questionAnswerJudgeViewModel.QuestionAnswerId) >=
                    NumberOfJudges)
                {
                    var questionAnswerJudges = _questionAnswerJudges
                        .Where(current => current.QuestionAnswerId == questionAnswerJudgeViewModel.QuestionAnswerId)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isActive = 0;

                    foreach (var judge in questionAnswerJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsActiveQuestionAnswer == true)
                            count_isActive++;
                    }

                    var updateQuestionAnswer = _questionAnswer
                        .First(x => x.Id == questionAnswerJudgeViewModel.QuestionAnswerId);

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestionAnswer.IsDelete = true;
                    else
                        updateQuestionAnswer.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestionAnswer.IsUpdate = true;
                    else
                        updateQuestionAnswer.IsUpdate = false;
                    if (count_isActive > NumberOfJudges / 2)
                        updateQuestionAnswer.IsActive = true;
                    else
                        updateQuestionAnswer.IsActive = false;

                    _uow.MarkAsChanged(updateQuestionAnswer);
                    _uow.ValidateOnSaveEnabled(false);
                    var msgResUpdate = _uow.CommitChanges(CrudType.Update, Title);
                }
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionAnswerJudge.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش کارشناسی جواب سوال
        /// </summary>
        /// <param name="questionAnswerJudgeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionAnswerJudgeUpdateViewModel questionAnswerJudgeViewModel)
        {
            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _uow.MarkAsChanged(questionAnswerJudge);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                if (_questionAnswerJudges.Count(current =>
                        current.QuestionAnswerId == questionAnswerJudgeViewModel.QuestionAnswerId) >=
                    NumberOfJudges)
                {
                    var questionAnswerJudges = _questionAnswerJudges
                        .Where(current => current.QuestionAnswerId == questionAnswerJudgeViewModel.QuestionAnswerId)
                        .OrderByDescending(current => current.Id).Take(NumberOfJudges).ToList();

                    int count_isDelete = 0;
                    int count_isUpdate = 0;
                    int count_isActive = 0;

                    foreach (var judge in questionAnswerJudges)
                    {
                        if (judge.IsDelete == true)
                            count_isDelete++;
                        if (judge.IsUpdate == true)
                            count_isUpdate++;
                        if (judge.IsActiveQuestionAnswer == true)
                            count_isActive++;
                    }

                    var updateQuestionAnswer = _questionAnswer
                        .First(x => x.Id == questionAnswerJudgeViewModel.QuestionAnswerId);

                    if (count_isDelete > NumberOfJudges / 2)
                        updateQuestionAnswer.IsDelete = true;
                    else
                        updateQuestionAnswer.IsDelete = false;

                    if (count_isUpdate > NumberOfJudges / 2)
                        updateQuestionAnswer.IsUpdate = true;
                    else
                        updateQuestionAnswer.IsUpdate = false;
                    if (count_isActive > NumberOfJudges / 2)
                        updateQuestionAnswer.IsActive = true;
                    else
                        updateQuestionAnswer.IsActive = false;
                }
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionAnswerJudge.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف کارشناسی جواب سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var questionAnswerJudgeViewModel = GetById(id);
            if (questionAnswerJudgeViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionAnswerJudge = Mapper.Map<QuestionAnswerJudge>(questionAnswerJudgeViewModel);
            _uow.MarkAsDeleted(questionAnswerJudge);

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
