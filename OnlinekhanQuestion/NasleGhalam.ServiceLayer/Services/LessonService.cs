using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.Report;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LessonService
    {
        private const string Title = "درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        private readonly IDbSet<Question> _questions;

        private readonly Lazy<EducationTreeService> _educationTreeService;

        public LessonService(IUnitOfWork uow, Lazy<EducationTreeService> educationTreeService)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
            _questions = uow.Set<Question>();

            _educationTreeService = educationTreeService;
        }

        /// <summary>
        /// گرفتن  درس با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LessonViewModel GetById(int id)
        {
            return _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
                .Include(x => x.LessonDepartments)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// اطلاعات آماری در مورد سوالات در درس
        /// </summary>
        /// <returns></returns>
        public IList<AllQuestionOfEachLessonViewModel> GetAllQuestionOfEachLesson()
        {
           var lessons = _lessons
                .Where(current =>  current.Topics.Any(x => x.Questions.Any()) || current.QuestionGroups.Any(x => x.Questions.Any()) )
                .Include(x=> x.Topics)
                .AsNoTracking()
                .AsEnumerable()
                .ToList();

           IList<AllQuestionOfEachLessonViewModel> returnVal = new List<AllQuestionOfEachLessonViewModel>();

           foreach (var lesson in lessons)
           {
               var questions1 =_questions
                   .Where(x => x.QuestionGroups.Any(y => y.LessonId == lesson.Id))
                   .Include(x=>x.Topics)
                   .Include(x => x.Topics.Select(y=> y.Lesson))
                   .Include(x => x.QuestionJudges)
                   .AsNoTracking()
                   .AsEnumerable()
                   .ToList();

               var ids = lesson.Topics.Select(x => x.Id);
               var questions2 = _questions
                   .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                   .Include(x => x.Topics)
                   .Include(x => x.Topics.Select(y=>y.Lesson))
                   .Include(x => x.QuestionJudges)
                   .AsNoTracking()
                   .AsEnumerable()
                   .ToList();

               var questions3 = new List<Question>();
               var questions2Ids = questions2.Select(x => x.Id).ToArray();
               foreach (var question in questions1)
               {
                   if (questions2Ids.Contains(question.Id))
                   {
                        questions3.Add(question);
                   }
               }

               var allQuestionNum = questions1/*.Concat(questions2)*/.Select(x=>x.Id).Distinct().Count();

               var allQuestionTopiced = questions1.Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                   .Count();
                var allQuestionJudged = questions1
                   .Count(x => x.QuestionJudges.Any<QuestionJudge>());

               var allQuestionJudgedFull = questions3.Where(x => x.QuestionJudges.Select(z => z.UserId).Distinct().Count() >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges).Count();
                   

               var allQuestionActived = questions3.Where(x => x.IsActive).Where(x => x.QuestionJudges.Select(z => z.UserId).Distinct().Count() >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges).Count();
                var allQuestionHybrid = questions3/*.Concat(questions2)*/.Where(x => x.IsHybrid == true).Select(x => x.Id).Distinct().Count();

               returnVal.Add( new AllQuestionOfEachLessonViewModel
               {
                   Name = lesson.Name,
                   AllQuestionActived = allQuestionActived,
                   AllQuestionHybrid = allQuestionHybrid,
                   AllQuestionJudged = allQuestionJudged,
                   AllQuestionNum = allQuestionNum,
                   AllQuestionTopiced = allQuestionTopiced,
                   AllQuestionJudgedFull = allQuestionJudgedFull
               });
            }

           return returnVal;

        }

        /// <summary>
        /// گرفتن همه درس ها با آی دی بخش
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAllByDepartmentId(int id)
        {
            return _lessons
                .Where(current => current.LessonDepartments.Any(x => x.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه درس ها با آی دی درخت آموزشی
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAllByEducationTreeIds(IEnumerable<int> ids)
        {
            return _lessons
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
                .Include(current => current.LessonDepartments)
                .Where(current => current.EducationTrees.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه درس ها
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAll()
        {
            return _lessons
                .Include(current => current.Ratios.Select(ratio => ratio.EducationSubGroup))
                .Include(current => current.LessonDepartments)
                .Include(current => current.EducationTrees)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت درس
        /// </summary>
        /// <param name="lessonViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(LessonCreateViewModel lessonViewModel)
        {
            if (lessonViewModel.NumberOfJudges == 0)
            {
                lessonViewModel.NumberOfJudges = 1;
            }

            var educationTrees = _educationTreeService.Value.GetAll()
                .Where(x => x.LookupId_EducationTreeState == 1034)
                .Select(x => x.Id);

            if (lessonViewModel.EducationTreeIds.Any(educationTreeId => !educationTrees.Contains(educationTreeId)))
            {
                return new ClientMessageResult
                {
                    MessageType = MessageType.Error,
                    Message = "تنها مجاز به انتخاب پایه هستید!"
                };
            }
            var lesson = Mapper.Map<Lesson>(lessonViewModel);
            _lessons.Add(lesson);

            foreach (var treeId in lessonViewModel.EducationTreeIds)
            {
                var tree = new EducationTree() { Id = treeId };
                _uow.MarkAsUnChanged(tree);
                lesson.EducationTrees.Add(tree);
            }

            if (lessonViewModel.LessonDepartmentId != 0)
            {
                var department = new LessonDepartment() { Id = lessonViewModel.LessonDepartmentId };
                _uow.MarkAsUnChanged(department);
                lesson.LessonDepartments.Add(department);
            }
            

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lesson.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش درس
        /// </summary>
        /// <param name="lessonUpdateViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(LessonUpdateViewModel lessonUpdateViewModel)
        {
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .Include(x => x.LessonDepartments)
                .First(current => current.Id == lessonUpdateViewModel.Id);

            //var departments = _lessons
            //    .Include(x => x.LessonDepartments)
            //    .AsNoTracking()
            //    .Where(current => current.Id == lessonUpdateViewModel.Id)
            //    .Select(x => x.LessonDepartments)
            //    .First();

            lesson.IsMain = lessonUpdateViewModel.IsMain;
            lesson.Name = lessonUpdateViewModel.Name;
            lesson.LookupId_Nezam = lessonUpdateViewModel.LookupId_Nezam;
            lesson.NumberOfJudges = lessonUpdateViewModel.NumberOfJudges;

            //delete education tree
            var deleteEducationTreeList = lesson.EducationTrees
                .Where(oldEdu => lessonUpdateViewModel.EducationTreeIds.All(newEduId => newEduId != oldEdu.Id))
                .ToList();
            foreach (var educationTree in deleteEducationTreeList)
            {
                lesson.EducationTrees.Remove(educationTree);
            }

            //add education tree
            var addEducationTreeList = lessonUpdateViewModel.EducationTreeIds
                .Where(oldEduId => lesson.EducationTrees.All(newEdu => newEdu.Id != oldEduId))
                .ToList();
            foreach (var educationTreeId in addEducationTreeList)
            {
                var educationTree = new EducationTree { Id = educationTreeId };
                _uow.MarkAsUnChanged(educationTree);
                lesson.EducationTrees.Add(educationTree);
            }

            //delete ratio
            var deleteRatio = lesson.Ratios
                .Where(x => lessonUpdateViewModel.Ratios.All(y => y.Id != x.Id))
                .ToList();
            foreach (var ratio in deleteRatio)
            {
                _uow.MarkAsDeleted(ratio);
            }

            //update ratio
            var updateRatio = lessonUpdateViewModel.Ratios
                .Where(x => lesson.Ratios.Any(y => y.Id == x.Id))
                .ToList();
            foreach (var ratioViewModel in updateRatio)
            {
                var ratio = lesson.Ratios.First(x => x.Id == ratioViewModel.Id);
                ratio.EducationSubGroupId = ratioViewModel.EducationSubGroupId;
                ratio.LessonId = ratioViewModel.LessonId;
                ratio.Rate = ratioViewModel.Rate;
            }

            //add ratio
            var addRatio = lessonUpdateViewModel.Ratios
                .Where(x => lesson.Ratios.All(y => y.Id != x.Id))
                .ToList();
            foreach (var newRatio in addRatio)
            {
                var ratio = Mapper.Map<Ratio>(newRatio);
                lesson.Ratios.Add(ratio);
            }

            if (lessonUpdateViewModel.LessonDepartmentId != 0 &&( lesson.LessonDepartments.FirstOrDefault() == null || lesson.LessonDepartments.FirstOrDefault().Id != lessonUpdateViewModel.LessonDepartmentId))
            {
                foreach (var item in lesson.LessonDepartments.ToList())
                {
                    lesson.LessonDepartments.Remove(item);
                    //_uow.MarkAsDeleted(item);
                }

                //lesson.LessonDepartments.Clear();
                var department = new LessonDepartment() { Id = lessonUpdateViewModel.LessonDepartmentId };
                _uow.MarkAsUnChanged(department);

                lesson.LessonDepartments.Add(department);
            }
            else if (lessonUpdateViewModel.LessonDepartmentId == 0)
            {
                foreach (var item in lesson.LessonDepartments.ToList())
                {
                    lesson.LessonDepartments.Remove(item);
                }
            }

                var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(lesson.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف درس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var lesson = _lessons
                .Include(current => current.EducationTrees)
                .Include(current => current.Ratios)
                .Include(x => x.LessonDepartments)
                .First(current => current.Id == id);

            if (lesson == null)
            {
                return ClientMessageResult.NotFound();
            }

            //remove education tree 
            var educationTrees = lesson.EducationTrees.ToList();
            foreach (var item in educationTrees)
            {
                lesson.EducationTrees.Remove(item);

            }

            //remove ratios
            var ratios = lesson.Ratios.ToList();
            foreach (var item in ratios)
            {
                _uow.MarkAsDeleted(item);
            }

            //remove lesson department
            var lessonDepartments = lesson.LessonDepartments.ToList();
            foreach (var item in lessonDepartments)
            {
                lesson.LessonDepartments.Remove(item);
            }

            _uow.MarkAsDeleted(lesson);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
