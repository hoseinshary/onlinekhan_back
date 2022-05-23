using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationYear;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationYearService
    {
        private const string Title = "سال تحصیلی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationYear> _educationYears;

        public EducationYearService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationYears = uow.Set<EducationYear>();
        }

        /// <summary>
        /// گرفتن  سال تحصیلی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationYearViewModel GetById(int id)
        {
            return _educationYears
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationYearViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه سال تحصیلی ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationYearViewModel> GetAll()
        {
            return _educationYears
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationYearViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت سال تحصیلی
        /// </summary>
        /// <param name="educationYearViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(EducationYearCreateViewModel educationYearViewModel)
        {
            var transaction = _uow.BeginTransaction();
            if (educationYearViewModel.IsActiveYear)
            {
                _uow.ExecuteSqlCommand("update EducationYears set IsActiveYear = 0");
            }
            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _educationYears.Add(educationYear);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
            {
                clientResult.Obj = educationYear;
                transaction.Commit();
            }
            else
                transaction.Rollback();

            return clientResult;
        }

        /// <summary>
        /// ویرایش سال تحصیلی
        /// </summary>
        /// <param name="educationYearViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(EducationYearUpdateViewModel educationYearViewModel)
        {
            var transaction = _uow.BeginTransaction();
            if (educationYearViewModel.IsActiveYear)
            {
                _uow.ExecuteSqlCommand("update EducationYears set IsActiveYear = 0");
            }
            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _uow.MarkAsChanged(educationYear);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
            {
                clientResult.Obj = educationYear;
                transaction.Commit();
            }
            else
                transaction.Rollback();

            return clientResult;
        }


        /// <summary>
        /// حذف سال تحصیلی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var educationYearViewModel = GetById(id);
            if (educationYearViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var educationYear = Mapper.Map<EducationYear>(educationYearViewModel);
            _uow.MarkAsDeleted(educationYear);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
