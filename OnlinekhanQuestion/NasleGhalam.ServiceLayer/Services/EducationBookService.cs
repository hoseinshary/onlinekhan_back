using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationBook;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationBookService
    {
        private const string Title = "کتاب درسی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationBook> _educationBooks;

        public EducationBookService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationBooks = uow.Set<EducationBook>();
        }

        /// <summary>
        /// گرفتن  کتاب درسی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationBookViewModel GetById(int id)
        {
            return _educationBooks
                .Include(current => current.Topics)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationBookViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        ///  گرفتن همه کتاب های درسی یک درس
        /// </summary>
        /// <returns></returns>
        public IList<EducationBookViewModel> GetAllByLessonId(int lessonId)
        {
            return _educationBooks
                .Where(current => current.LessonId == lessonId)
                .AsEnumerable()
                .Select(Mapper.Map<EducationBookViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(EducationBookCreateViewModel educationBookViewModel)
        {
            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                educationBook.Topics.Add(topic);
            }

            _educationBooks.Add(educationBook);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationBook.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش کتاب درسی
        /// </summary>
        /// <param name="educationBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(EducationBookCreateViewModel educationBookViewModel)
        {
            var transaction = _uow.BeginTransaction();
            _uow.ExecuteSqlCommand("delete from Topics_EducationBooks where EducationBookId=@id",
                new SqlParameter("@id", educationBookViewModel.Id));

            var educationBook = Mapper.Map<EducationBook>(educationBookViewModel);
            _uow.MarkAsChanged(educationBook);

            foreach (var topicId in educationBookViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                educationBook.Topics.Add(topic);
            }

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success)
                transaction.Commit();
            else
                transaction.Rollback();

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationBook.Id);

            return clientResult;
        }


        /// <summary>
        /// حذف کتاب درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var educationBook = _educationBooks
                .Include(current => current.Topics)
                .FirstOrDefault(current => current.Id == id);

            if (educationBook == null)
            {
                return ClientMessageResult.NotFound();
            }

            var topics = educationBook.Topics.ToList();
            foreach (var topic in topics)
            {
                educationBook.Topics.Remove(topic);
            }
            _uow.MarkAsDeleted(educationBook);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
