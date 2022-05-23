using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Tag;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TagService
    {
        private const string Title = "برچسب";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Tag> _tags;

        public TagService(IUnitOfWork uow)
        {
            _uow = uow;
            _tags = uow.Set<Tag>();
        }

        /// <summary>
        /// گرفتن  برچسب با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TagViewModel GetById(int id)
        {
            return _tags
                    .Where(current => current.Id == id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<TagViewModel>)
                    .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه برچسب ها
        /// </summary>
        /// <returns></returns>
        public IList<TagViewModel> GetAll()
        {
            return _tags
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<TagViewModel>)
                    .ToList();
        }

        /// <summary>
        /// ثبت برچسب
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(TagViewModel tagViewModel)
        {
            var tag = Mapper.Map<Tag>(tagViewModel);
            _tags.Add(tag);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(tag.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش برچسب
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(TagViewModel tagViewModel)
        {
            var tag = Mapper.Map<Tag>(tagViewModel);
            _uow.MarkAsChanged(tag);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(tag.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف برچسب
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var tagViewModel = GetById(id);
            if (tagViewModel == null)
                return ClientMessageResult.NotFound();

            var tag = Mapper.Map<Tag>(tagViewModel);
            _uow.MarkAsDeleted(tag);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
