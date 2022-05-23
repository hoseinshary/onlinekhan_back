using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Package;

namespace NasleGhalam.ServiceLayer.Services
{
    public class PackageService
    {
        private const string Title = "بسته";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Package> _packages;

        public PackageService(IUnitOfWork uow)
        {
            _uow = uow;
            _packages = uow.Set<Package>();
        }

        /// <summary>
        /// گرفتن  بسته با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PackageViewModel GetById(int id)
        {
            return _packages
                .Include(current => current.Lessons)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PackageViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه بسته ها
        /// </summary>
        /// <returns></returns>
        public IList<PackageViewModel> GetAllByEducationTreeId(IEnumerable<int> ids)
        {
            return _packages
                .Where(current => !current.IsDelete)
                .Where(current => current.Lessons.Any(lesson => lesson.EducationTrees.Any(education => ids.Contains(education.Id))))
                .Include(current => current.Lessons)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PackageViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت بسته
        /// </summary>
        /// <param name="packageViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(PackageCreateViewModel packageViewModel)
        {
            var package = Mapper.Map<Package>(packageViewModel);
            _packages.Add(package);

            foreach (var item in packageViewModel.LessonIds)
            {
                var lesson = new Lesson { Id = item };
                _uow.MarkAsUnChanged(lesson);
                package.Lessons.Add(lesson);
            }

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(package.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش بسته
        /// </summary>
        /// <param name="packageViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(PackageUpdateViewModel packageViewModel)
        {
            var package = _packages
                .Include(current => current.Lessons)
                .FirstOrDefault(current => current.Id == packageViewModel.Id);
            if (package == null)
                return ClientMessageResult.NotFound();

            package.Description = packageViewModel.Description;
            package.ImageFile = packageViewModel.ImageFile;
            package.IsActive = packageViewModel.IsActive;
            package.Name = packageViewModel.Name;
            package.Price = packageViewModel.Price;
            package.TimeDays = packageViewModel.TimeDays;

            //delete lessons
            var deleteLessonsList = package.Lessons
                .Where(oldLesson => packageViewModel.LessonIds.All(newLessonId => newLessonId != oldLesson.Id))
                .ToList();
            foreach (var lesson in deleteLessonsList)
            {
                package.Lessons.Remove(lesson);
            }

            //add lessons
            var addLessonList = packageViewModel.LessonIds
                .Where(oldLessonId => package.Lessons.All(newLesson => newLesson.Id != oldLessonId))
                .ToList();
            foreach (var lessonId in addLessonList)
            {
                var lesson = new Lesson { Id = lessonId };
                _uow.MarkAsUnChanged(lesson);
                package.Lessons.Add(lesson);
            }

            _uow.ExcludeFieldsFromUpdate(package,
                x => x.CreateDateTime,
                x => x.IsDelete);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(package.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف بسته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var packageViewModel = GetById(id);
            if (packageViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            packageViewModel.IsDelete = true;
            var package = Mapper.Map<Package>(packageViewModel);
            _uow.MarkAsChanged(package);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
