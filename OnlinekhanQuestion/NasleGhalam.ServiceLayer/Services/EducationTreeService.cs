using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ServiceLayer.Services
{
    public class EducationTreeService
    {
        private const string Title = "درخت آموزش";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<EducationTree> _educationTrees;

        public EducationTreeService(IUnitOfWork uow)
        {
            _uow = uow;
            _educationTrees = uow.Set<EducationTree>();
        }

        /// <summary>
        /// گرفتن  درخت آموزش با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EducationTreeViewModel GetById(int id)
        {
            return _educationTrees
                .Include(current => current.Lookup_EducationTreeState)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationTreeViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه درخت آموزش ها
        /// </summary>
        /// <returns></returns>
        public IList<EducationTreeViewModel> GetAll()
        {
            return _educationTrees
                .Include(current => current.Lookup_EducationTreeState)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<EducationTreeViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه درخت آموزش ها با درس
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllByLessonId(int lessonId)
        {
            return _educationTrees
                .Where(current => current.Lessons.Any(lesson => lesson.Id == lessonId))
                .AsNoTracking()
                .AsEnumerable()
                .Select(current => current.Id)
                .ToList();
        }

        /// <summary>
        /// ثبت درخت آموزش
        /// </summary>
        /// <param name="educationTreeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(EducationTreeCreateViewModel educationTreeViewModel)
        {
            if (educationTreeViewModel.ParentEducationTreeId == 0 ||
                educationTreeViewModel.ParentEducationTreeId == null)
            {
                return new ClientMessageResult
                {
                    MessageType = MessageType.Error,
                    Message = "ریشه انتخاب نشده است"
                };
            }

            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _educationTrees.Add(educationTree);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationTree.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش درخت آموزش
        /// </summary>
        /// <param name="educationTreeViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(EducationTreeUpdateViewModel educationTreeViewModel)
        {
            if (educationTreeViewModel.ParentEducationTreeId == 0 ||
                educationTreeViewModel.ParentEducationTreeId == null)
            {
                return new ClientMessageResult
                {
                    MessageType = MessageType.Error,
                    Message = "ریشه انتخاب نشده است"
                };
            }

            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _uow.MarkAsChanged(educationTree);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(educationTree.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف درخت آموزش
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var educationTreeViewModel = GetById(id);
            if (educationTreeViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var educationTree = Mapper.Map<EducationTree>(educationTreeViewModel);
            _uow.MarkAsDeleted(educationTree);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
