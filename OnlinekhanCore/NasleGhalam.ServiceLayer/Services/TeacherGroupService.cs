using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.TeacherGroup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TeacherGroupService
    {
        private const string Title = "گروه دانش آموزی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<TeacherGroup> _teacherGroups;
        private readonly IDbSet<Student> _students;


        public TeacherGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _teacherGroups = uow.Set<TeacherGroup>();
        }

        /// <summary>
        /// گرفتن  انتخاب رشته با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TeacherGroupViewModel GetById(int id)
        {
            var stdn = _teacherGroups
                .Where(current => current.Id == id)
                .Include(current => current.Students)
                .Include(x => x.Students.Select(q => q.User))
                .Include(current => current.Teacher.User)

                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TeacherGroupViewModel>)
                .FirstOrDefault();
            return stdn;
        }
        
      

        /// <summary>
        /// گرفتن همه انتخاب رشته ها
        /// </summary>
        /// <returns></returns>
        public IList<TeacherGroupViewModel> GetAll(int rollId ,  int userId)
        {
            if (rollId == 6)
            {
                var std = _teacherGroups
                    .Include(current => current.Students.Select(x => x.User))
                    .Include(current => current.Teacher.User)
                    .Where(current => current.TeacherId == userId)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<TeacherGroupViewModel>)
                    .ToList();

                return std;
            }
            else
            {
                var std = _teacherGroups
                    .Include(current => current.Students.Select(x => x.User))
                    .Include(current => current.Teacher.User)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<TeacherGroupViewModel>)
                    .ToList();

                return std;
            }
        }

        /// <summary>
        /// ثبت انتخاب رشته
        /// </summary>
        /// <param name="teacherGroupViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(TeacherGroupCreateViewModel teacherGroupViewModel)
        {
            var teacherGroup = Mapper.Map<TeacherGroup>(teacherGroupViewModel);
            foreach (var item in teacherGroupViewModel.StudentsId)
            {
                Student student = new Student() { Id = item };
                
                teacherGroup.Students.Add(student);
                _uow.MarkAsUnChanged(student);
            }
            _teacherGroups.Add(teacherGroup);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(teacherGroup.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش انتخاب رشته
        /// </summary>
        /// <param name="teacherGroupViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(TeacherGroupUpdateViewModel teacherGroupViewModel)
        {
            Mapper.Map<TeacherGroup>(teacherGroupViewModel);
            var teachergroup = _teacherGroups
               .Include(current => current.Students)
               .Include(x => x.Students.Select(q => q.User))
                    .Include(current => current.Teacher.User)
               .First(current => current.Id == teacherGroupViewModel.Id);

            teachergroup.Name = teacherGroupViewModel.Name;

            var deletestudentList = teachergroup.Students
                 .Where(oldMaj => teacherGroupViewModel.StudentsId.All(newMajId => newMajId != oldMaj.User.Id))
                 .ToList();
            foreach (var item in deletestudentList)
            {
                teachergroup.Students.Remove(item);
                _uow.MarkAsUnChanged(item);
            }
            var addstudentList = teacherGroupViewModel.StudentsId
               .Where(oldMajId => teachergroup.Students.All(newMaj => newMaj.User.Id != oldMajId))
               .ToList();
            foreach (var item in addstudentList)
            {
                
                var student = new Student() { Id= item };
                teachergroup.Students.Add(student);
                _uow.MarkAsUnChanged(student);

            }



            _uow.MarkAsChanged(teachergroup);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(teachergroup.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف انتخاب رشته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var teachergrouplist = _teacherGroups
               .Include(current => current.Students)
               .Include(x => x.Students.Select(q => q.User))
                    .Include(current => current.Teacher.User)
               .First(current => current.Id == id);
            if (teachergrouplist == null)
            {
                return ClientMessageResult.NotFound();
            }
            foreach (var item in teachergrouplist.Students)
            {

                teachergrouplist.Students.Add(item);
                _uow.MarkAsUnChanged(item);

            }
            _uow.MarkAsDeleted(teachergrouplist);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
