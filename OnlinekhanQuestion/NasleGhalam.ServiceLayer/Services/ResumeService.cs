using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Resume;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ResumeService
    {
        private const string Title = "رزومه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Resume> _resumes;

        public ResumeService(IUnitOfWork uow)
        {
            _uow = uow;
            _resumes = uow.Set<Resume>();
        }

        /// <summary>
        /// گرفتن  رزومه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResumeViewModel GetById(int id)
        {
            return _resumes
                .Include(current => current.City)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ResumeViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه رزومه ها
        /// </summary>
        /// <returns></returns>
        public IList<ResumeViewModel> GetAll()
        {
            return _resumes
                .Include(current => current.City)
                .OrderByDescending(current => current.CreationDateTime)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ResumeViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت رزومه
        /// </summary>
        /// <param name="resumeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(ResumeViewModel resumeViewModel)
        {
            var resume = Mapper.Map<Resume>(resumeViewModel);
            _resumes.Add(resume);

            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(resume.Id);

            return clientResult;
        }



        /// <summary>
        /// حذف رزومه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var resumeViewModel = GetById(id);
            if (resumeViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var resume = Mapper.Map<Resume>(resumeViewModel);
            _uow.MarkAsDeleted(resume);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
