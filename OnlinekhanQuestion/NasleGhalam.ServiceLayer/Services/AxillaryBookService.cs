using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AxillaryBook;

namespace NasleGhalam.ServiceLayer.Services
{
    public class AxillaryBookService
    {
        private const string Title = "کتاب کمک درسی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AxillaryBook> _axillaryBooks;

        public AxillaryBookService(IUnitOfWork uow)
        {
            _uow = uow;
            _axillaryBooks = uow.Set<AxillaryBook>();
        }

        /// <summary>
        /// گرفتن  کتاب کمک درسی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <param name="imgUrlPath"></param>
        /// <returns></returns>
        public AxillaryBookViewModel GetById(int id, string imgUrlPath = "")
        {
            return _axillaryBooks
                .Include(current => current.Lookup_BookType)
                .Include(current => current.Lookup_PaperType)
                .Include(current => current.Lookup_PrintType)
                .Include(current => current.Publisher)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AxillaryBookViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه کتاب کمک درسی ها
        /// </summary>
        /// <returns></returns>
        public IList<AxillaryBookViewModel> GetAll()
        {
            return _axillaryBooks
                .Include(current => current.Lookup_BookType)
                .Include(current => current.Lookup_PaperType)
                .Include(current => current.Lookup_PrintType)
                .Include(current => current.Publisher)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AxillaryBookViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _axillaryBooks.Add(axillaryBook);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(axillaryBook.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش کتاب کمک درسی
        /// </summary>
        /// <param name="axillaryBookViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AxillaryBookViewModel axillaryBookViewModel)
        {
            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            if (string.IsNullOrEmpty(axillaryBook.ImgName))
            {
                _uow.ExcludeFieldsFromUpdate(axillaryBook, x => x.ImgName);
            }
            else
            {
                _uow.MarkAsChanged(axillaryBook);
            }

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(axillaryBook.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف کتاب کمک درسی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var axillaryBookViewModel = GetById(id);
            if (axillaryBookViewModel == null)
                return ClientMessageResult.NotFound();

            var axillaryBook = Mapper.Map<AxillaryBook>(axillaryBookViewModel);
            _uow.MarkAsDeleted(axillaryBook);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(axillaryBook.ImgName))
            {
                File.Delete(axillaryBookViewModel.ImgAbsPath);
            }

            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
