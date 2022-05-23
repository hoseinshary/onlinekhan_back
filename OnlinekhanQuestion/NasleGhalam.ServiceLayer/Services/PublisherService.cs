using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Publisher;

namespace NasleGhalam.ServiceLayer.Services
{
    public class PublisherService
    {
        private const string Title = "انتشارات";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Publisher> _publishers;

        public PublisherService(IUnitOfWork uow)
        {
            _uow = uow;
            _publishers = uow.Set<Publisher>();
        }

        /// <summary>
        /// گرفتن  انتشارات با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PublisherViewModel GetById(int id)
        {
            return _publishers
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PublisherViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه انتشارات ها
        /// </summary>
        /// <returns></returns>
        public IList<PublisherViewModel> GetAll()
        {
            return _publishers
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PublisherViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت انتشارات
        /// </summary>
        /// <param name="publisherViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(PublisherViewModel publisherViewModel)
        {
            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _publishers.Add(publisher);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(publisher.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش انتشارات
        /// </summary>
        /// <param name="publisherViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(PublisherViewModel publisherViewModel)
        {
            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _uow.MarkAsChanged(publisher);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(publisher.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف انتشارات
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var publisherViewModel = GetById(id);
            if (publisherViewModel == null)
                return ClientMessageResult.NotFound();

            var publisher = Mapper.Map<Publisher>(publisherViewModel);
            _uow.MarkAsDeleted(publisher);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
