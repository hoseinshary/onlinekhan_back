using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationSubGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationSubGroupService
    {
        private const string Title = "زیر گروه آموزشی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationSubGroup> _educationSubGroups;

        

        public EducationSubGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationSubGroups = uow.Set<EducationSubGroup>();
        }

        /// <summary>
        /// گرفتن  زیر گروه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationSubGroupViewModel GetById(int id)
        {
            return _educationSubGroups
                .Include(current => current.EducationTree)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationSubGroupViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه زیر گروه ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationSubGroupViewModel> GetAll()
        {
            return _educationSubGroups
                .Include(current => current.EducationTree)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationSubGroupViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت زیر گروه
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(EducationSubGroupCreateViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _educationSubGroups.Add(educationSubGroup);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationSubGroup.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش زیر گروه
        /// </summary>
        /// <param name="educationSubGroupViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(EducationSubGroupUpdateViewModel educationSubGroupViewModel)
        {
            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsChanged(educationSubGroup);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationSubGroup.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف زیر گروه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var educationSubGroupViewModel = GetById(id);
            if (educationSubGroupViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var educationSubGroup = Mapper.Map<EducationSubGroup>(educationSubGroupViewModel);
            _uow.MarkAsDeleted(educationSubGroup);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
