using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionUpdate;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionUpdateService
    {
        private const string Title = "تاریخچه سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionUpdate> _questionUpdates;

        public QuestionUpdateService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionUpdates = uow.Set<QuestionUpdate>();
        }

        /// <summary>
        /// گرفتن  تاریخچه سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionUpdateViewModel GetById(int id)
        {
            return _questionUpdates
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionUpdateViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه تاریخچه سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionUpdateViewModel> GetAll()
        {
            return _questionUpdates
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionUpdateViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت تاریخچه سوال
        /// </summary>
        /// <param name="questionUpdateViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionUpdateViewModel questionUpdateViewModel)
        {
            var questionUpdate = Mapper.Map<QuestionUpdate>(questionUpdateViewModel);
            _questionUpdates.Add(questionUpdate);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Id = serverResult.Id;

            return clientResult;
        }

        /// <summary>
        /// ویرایش تاریخچه سوال
        /// </summary>
        /// <param name="questionUpdateViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionUpdateViewModel questionUpdateViewModel)
        {
            var questionUpdate = Mapper.Map<QuestionUpdate>(questionUpdateViewModel);
            _uow.MarkAsChanged(questionUpdate);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(questionUpdate.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف تاریخچه سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var questionUpdateViewModel = GetById(id);
            if (questionUpdateViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionUpdate = Mapper.Map<QuestionUpdate>(questionUpdateViewModel);
            _uow.MarkAsDeleted(questionUpdate);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
