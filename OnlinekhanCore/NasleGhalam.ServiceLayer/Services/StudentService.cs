using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.Services
{
    public class StudentService
    {
        private const string Title = "دانش آموز";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Student> _students;
        private readonly Lazy<RoleService> _roleService;
        private readonly Lazy<QuestionService> _questionService;
        private readonly Lazy<TopicService> _topicService;

        public StudentService(IUnitOfWork uow,
            Lazy<RoleService> roleService , Lazy<QuestionService> questionService,Lazy<TopicService> topicService)
        {
            _uow = uow;
            _students = uow.Set<Student>();
            _roleService = roleService;
            _questionService = questionService;
            _topicService = topicService;
        }

        /// <summary>
        /// گرفتن  دانش آموز با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentViewModel GetById(int id)
        {
            return _students
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        public IList<StudentViewModel> GetAll()
        {
            return _students
                .Include(current => current.User.Role)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<StudentViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        public StudentQuestionAssayReportViewModel GetQuestionAssayReportByLessonId(int lessonId , int studentId)
        {
            StudentQuestionAssayReportViewModel returnVal = new StudentQuestionAssayReportViewModel();
            var student = _students
                .Include(x => x.User.Assays.Select(y => y.AssayQuestions))
                .Where(x => x.Id == studentId)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
            returnVal.User = Mapper.Map<UserViewModel>(student.User);
            returnVal.Id = student.Id;
            
            returnVal.NumberOfHomeworkQuestions = student.User.Assays.Where(x => x.LookupId_QuestionType == 1093).Select(y => y.AssayQuestions)
                .Distinct().Count();

            returnVal.NumberOfAssayQuestions = student.User.Assays.Select(y => y.AssayQuestions)
                .Distinct().Count();

            returnVal.NumberOfNewQuestions =
                _questionService.Value.GetAllActiveByLessonId(lessonId).Distinct().Count() -
                returnVal.NumberOfAssayQuestions;

            return returnVal;

        }


        /// <summary>
        /// گرفتن همه دانش آموز ها
        /// </summary>
        /// <returns></returns>
        //public StudentQuestionAssayReportForTopicViewModel GetQuestionAssayReportByLessonIds(int [] lessonIds, int studentId)
        //{
        //    StudentQuestionAssayReportForTopicViewModel returnVal = new StudentQuestionAssayReportForTopicViewModel();
        //    var student = _students
        //        .Include(x => x.User.Assays.Select(y => y.AssayQuestions))
        //        .Where(x => x.Id == studentId)
        //        .AsNoTracking()
        //        .AsEnumerable()
        //        .FirstOrDefault();
        //    returnVal.User = Mapper.Map<UserViewModel>(student.User);

        //    returnVal.LessonReports = new List<LessonReportViewModel>();

        //    foreach (var lessonId in lessonIds)
        //    {
        //        var topicsList = _topicService.Value.GetAll4LevelByLessonId(lessonId);

        //        List<TopicReportViewModel> temp = new List<TopicReportViewModel>();

        //        foreach (var topicViewModel in topicsList)
        //        {
        //            //var childrentopic = _topicService.Value.GetAllChildren(topicViewModel.Id);

        //            //var questions = _questionService.Value.GetAllByTopicIdsActive(childrentopic.Select(x => x.Id));

        //            var NumberOfHomeworkQuestions = student.User.Assays.Where(x => x.LookupId_QuestionType == 1093).Select(y =>
        //                y.AssayQuestions.Any(z => questions.Select(p => p.Id).Contains(z.QuestionId))).Distinct().Count();

        //            var NumberOfAssayQuestions = student.User.Assays
        //                .Select(y => y.AssayQuestions.Any(z => questions.Select(p => p.Id).Contains(z.QuestionId)))
        //                .Distinct().Count();

        //            var NumberOfNewQuestions = questions.Count - NumberOfAssayQuestions;

        //            temp.Add( new TopicReportViewModel()
        //            {
        //                ID = topicViewModel.Id,
        //                NumberOfAssayQuestions = NumberOfAssayQuestions,
        //                NumberOfHomeworkQuestions = NumberOfHomeworkQuestions,
        //                NumberOfNewQuestions = NumberOfNewQuestions
        //            });

        //        }

        //        returnVal.LessonReports.Add(new LessonReportViewModel()
        //        {
        //            Id = lessonId,
        //            TopicReports = temp
        //        });


        //    }
            

        //    return returnVal;

        //}

        /// <summary>
        /// ثبت دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(StudentCreateViewModel studentViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(studentViewModel.User.RoleId, userRoleLevel);
            if (role.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var student = Mapper.Map<Student>(studentViewModel);
            student.User.LastLogin = DateTime.Now;
            _students.Add(student);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(student.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش دانش آموز
        /// </summary>
        /// <param name="studentViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(StudentUpdateViewModel studentViewModel, byte userRoleLevel)
        {
            // سطح نقش باید بزرگتر از سطح نقش کاربر ویرایش کننده باشد
            var role = _roleService.Value.GetById(studentViewModel.User.RoleId, userRoleLevel);
            if (role == null)
            {
                return new ClientMessageResult()
                {
                    Message = "نقش یافت نگردید",
                    MessageType = MessageType.Error
                };
            }

            if (role.Level <= userRoleLevel)
            {
                return new ClientMessageResult()
                {
                    Message = $"سطح نقش باید بزرگتر از ({userRoleLevel}) باشد",
                    MessageType = MessageType.Error
                };
            }

            var student = Mapper.Map<Student>(studentViewModel);
            _uow.MarkAsChanged(student.User);
            _uow.MarkAsChanged(student);

            if (string.IsNullOrEmpty(student.User.Password))
            {
                _uow.ExcludeFieldsFromUpdate(student.User, x => x.Password, x => x.LastLogin);
                _uow.ValidateOnSaveEnabled(false);
            }
            else
            {
                _uow.ExcludeFieldsFromUpdate(student.User, x => x.LastLogin);
            }

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
            {
                clientResult.Obj = GetById(student.Id);
            }
            else if (serverResult.ErrorNumber == 2601 && serverResult.EnMessage.Contains("UK_User_NationalNo"))
            {
                clientResult.Message = "کد ملی تکراری می باشد";
            }
            else if (serverResult.ErrorNumber == 2601 && serverResult.EnMessage.Contains("UK_User_Username"))
            {
                clientResult.Message = "نام کاربری تکراری می باشد";
            }

            return clientResult;
        }

        /// <summary>
        /// حذف دانش آموز
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var studentViewModel = GetById(id);
            if (studentViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var student = Mapper.Map<Student>(studentViewModel);
            var user = student.User;
            _uow.MarkAsDeleted(student);
            _uow.MarkAsDeleted(user);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
