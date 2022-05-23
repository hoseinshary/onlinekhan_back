using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.StudentMajorlist;

namespace NasleGhalam.ServiceLayer.Services
{
    public class StudentMajorlistService
    {
        private const string Title = "انتخاب رشته";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<StudentMajorlist> _stduentMajorlists;
        private readonly IDbSet<Student> _students;
        private readonly IDbSet<Majors> _majors;
        private readonly IDbSet<StudentMajorList_Major> _studentmajorlist_major;


        public StudentMajorlistService(IUnitOfWork uow)
        {
            _uow = uow;
            _stduentMajorlists = uow.Set<StudentMajorlist>();
            _students = uow.Set<Student>();
            _studentmajorlist_major = uow.Set<StudentMajorList_Major>();
            _majors = uow.Set<Majors>();
        }

        /// <summary>
        /// گرفتن  انتخاب رشته با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentMajorlistViewModel GetById(int id)
        {
            var stdn = _stduentMajorlists
                .Where(current => current.Id == id)
                .Include(current => current.Student.User)
                .Include(current => current.Majors)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentMajorlistViewModel>)
                .FirstOrDefault();
            stdn.Majors = _studentmajorlist_major.Where(x => x.StudentMajorListId == id).Include(x => x.Major).AsNoTracking().AsEnumerable().OrderBy(x=>x.Priority).Select(x => x.Major).ToList().Select(Mapper.Map<MajorViewModel>).ToList();
            return stdn;
        }
        public IList<StudentMajorlistViewModel> GetStudentById(int id, byte roles)
        {
            if (roles < 3)
            {
                var std = _stduentMajorlists
                    .Include(current => current.Student.User)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<StudentMajorlistViewModel>)
                    .ToList();
                //foreach (var item in std)
                //{
                //    item.Majors = _studentmajorlist_major.Where(x => x.StudentMajorListId == item.Id).Include(x => x.Major).AsNoTracking().AsEnumerable().OrderBy(x => x.Priority).Select(x => x.Major).ToList().Select(Mapper.Map<MajorViewModel>).ToList();
                //}
                return std;
            }
            else
            {
                var std = _stduentMajorlists
                    .Include(current => current.Student.User)
                    .Where(current => current.StudentId == id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<StudentMajorlistViewModel>)
                    .ToList();
                //foreach (var item in std)
                //{
                //    item.Majors = _studentmajorlist_major.Where(x => x.StudentMajorListId == item.Id).Include(x => x.Major).AsNoTracking().AsEnumerable().OrderBy(x => x.Priority).Select(x => x.Major).ToList().Select(Mapper.Map<MajorViewModel>).ToList();
                //}
                return std;
            }
        }
      

        public IList<MajorViewModel> GetAllMajors()
        {
            return _majors
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        public IList<MajorViewModel> GetMajorById(int id)
        {
            return _majors
                .Where(x => x.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        public IList<MajorViewModel> GetMajorsBySearch(MajorSearchViewModel majorSearch)
        {
            
            return _majors
                .Where(x=> x.MajorTitle.Contains(majorSearch.MajorTitle) || x.University.Contains(majorSearch.MajorTitle))
                .Where(x=> x.Apply == majorSearch.Apply && x.Field == majorSearch.Field)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MajorViewModel>)
                .ToList();
        }
        /// <summary>
        /// گرفتن همه انتخاب رشته ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentMajorlistViewModel> GetAll()
        {
            var std = _stduentMajorlists
                    .Include(current => current.Student.User)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<StudentMajorlistViewModel>)
                    .ToList();
            foreach (var item in std)
            {
                item.Majors = _studentmajorlist_major.Where(x => x.StudentMajorListId == item.Id).Include(x => x.Major).AsNoTracking().AsEnumerable().OrderBy(x => x.Priority).Select(x => x.Major).ToList().Select(Mapper.Map<MajorViewModel>).ToList();
            }
            return std;
        }

        /// <summary>
        /// ثبت انتخاب رشته
        /// </summary>
        /// <param name="stduentMajorlistViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(StudentMajorlistCreateViewModel stduentMajorlistViewModel)
        {
            StudentMajorlist studentMajorlist = new StudentMajorlist();
            studentMajorlist.Title = stduentMajorlistViewModel.Title;
            studentMajorlist.StudentId = stduentMajorlistViewModel.StudentId;
            studentMajorlist.CreationDate = DateTime.Now;
           
            
             
            int priority = 1;
            foreach (var item in stduentMajorlistViewModel.MajorsId)
            {
                var StudentMajorList_Major = new StudentMajorList_Major() {MajorsId = item, Priority = priority };
                studentMajorlist.StudentMajorList_Major.Add(StudentMajorList_Major);
                priority += 1;
            }
            _stduentMajorlists.Add(studentMajorlist);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(studentMajorlist.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش انتخاب رشته
        /// </summary>
        /// <param name="stduentMajorlistViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(StudentMajorlistUpdateViewModel stduentMajorlistViewModel)
        {
            
            var studentmajorlist = _stduentMajorlists
               .Include(current => current.Majors)
               .Include(current =>current.StudentMajorList_Major)
               .First(current => current.Id == stduentMajorlistViewModel.Id);

            studentmajorlist.Title = stduentMajorlistViewModel.Title;

            var majors = _studentmajorlist_major.Where(x => x.StudentMajorListId == stduentMajorlistViewModel.Id).Include(x => x.Major).ToList();
            var deletemajorList = studentmajorlist.StudentMajorList_Major
                 .Where(oldMaj => stduentMajorlistViewModel.MajorsId.All(newMajId => newMajId != oldMaj.Major.Id))
                 .ToList();
            foreach (var item in deletemajorList)
            {
                studentmajorlist.StudentMajorList_Major.Remove(item);
                _uow.MarkAsDeleted(item);
            }
            var addmajorList = stduentMajorlistViewModel.MajorsId
               .Where(oldMajId => studentmajorlist.StudentMajorList_Major.All(newMaj => newMaj.Major.Id != oldMajId))
               .ToList();
            foreach (var item in addmajorList)
            {
                
                var studentmajor = new StudentMajorList_Major() { MajorsId = item,StudentMajorListId = stduentMajorlistViewModel.StudentId,Priority = 1 };
                studentmajorlist.StudentMajorList_Major.Add(studentmajor);
            }

            int priority = 1;
            foreach (var item in stduentMajorlistViewModel.MajorsId)
            {
                var majo = studentmajorlist.StudentMajorList_Major.Where(x => x.MajorsId == item).FirstOrDefault();
                majo.Priority = priority;
                priority += 1;
            }

            _uow.MarkAsChanged(studentmajorlist);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(studentmajorlist.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف انتخاب رشته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var studentmajorlist = _stduentMajorlists
               .Include(current => current.StudentMajorList_Major)
               .First(current => current.Id == id);
            if (studentmajorlist == null)
            {
                return ClientMessageResult.NotFound();
            }
            var stduentMajorlist = Mapper.Map<StudentMajorlist>(studentmajorlist);
            var majors = stduentMajorlist.StudentMajorList_Major.ToList();
            foreach (var item in majors)
            {
                stduentMajorlist.StudentMajorList_Major.Remove(item);
                _uow.MarkAsDeleted(item);

            }
            _uow.MarkAsDeleted(stduentMajorlist);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
