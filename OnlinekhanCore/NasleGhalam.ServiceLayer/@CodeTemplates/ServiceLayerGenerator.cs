using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AssayAnswerSheet;

namespace NasleGhalam.ServiceLayer.Services
{
	public class AssayAnswerSheetService
	{
		private const string Title = "پاسخ نامه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AssayAnswerSheet> _assayAnswerSheets;
       
	    public AssayAnswerSheetService(IUnitOfWork uow)
        {
            _uow = uow;
            _assayAnswerSheets = uow.Set<AssayAnswerSheet>();
        }

		/// <summary>
        /// گرفتن  پاسخ نامه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssayAnswerSheetViewModel GetById(int id)
        {
            return _assayAnswerSheets
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayAnswerSheetViewModel>)
                .FirstOrDefault();
        }

		/// <summary>
        /// گرفتن همه پاسخ نامه ها
        /// </summary>
        /// <returns></returns>
        public IList<AssayAnswerSheetViewModel> GetAll()
        {
            return _assayAnswerSheets
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayAnswerSheetViewModel>)
                .ToList();
        }

		/// <summary>
        /// ثبت پاسخ نامه
        /// </summary>
        /// <param name="assayAnswerSheetViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(AssayAnswerSheetCreateViewModel assayAnswerSheetViewModel)
        {
            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);
            _assayAnswerSheets.Add(assayAnswerSheet);

			var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assayAnswerSheet.Id);

            return clientResult;
        }

		/// <summary>
        /// ویرایش پاسخ نامه
        /// </summary>
        /// <param name="assayAnswerSheetViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AssayAnswerSheetUpdateViewModel assayAnswerSheetViewModel)
        {
            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);
            _uow.MarkAsChanged(assayAnswerSheet);
			
			 var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assayAnswerSheet.Id);

            return clientResult;
        }

		/// <summary>
        /// حذف پاسخ نامه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
			var  assayAnswerSheetViewModel = GetById(id);
            if (assayAnswerSheetViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);
            _uow.MarkAsDeleted(assayAnswerSheet);
            
			var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
			return Mapper.Map<ClientMessageResult>(msgRes);
        }
	}
}
