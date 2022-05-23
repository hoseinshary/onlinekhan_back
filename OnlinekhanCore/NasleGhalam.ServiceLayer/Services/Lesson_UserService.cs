using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lesson;

namespace NasleGhalam.ServiceLayer.Services
{
    public class Lesson_UserService
    {
        //private const string Title = "اختصاص کاربر به درس";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Lesson> _lessons;
        private readonly IDbSet<User> _users;

        public Lesson_UserService(IUnitOfWork uow)
        {
            _uow = uow;
            _lessons = uow.Set<Lesson>();
            _users = uow.Set<User>();
        }

        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllByUserIds(IEnumerable<int> ids)
        {
            return _lessons
                .Where(x => x.Users.Any(y => ids.Contains(y.Id)))
                .Select(x => x.Id)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllByLessonIds(IEnumerable<int> ids)
        {
            return _users
                .Where(x => x.Lessons.Any(y => ids.Contains(y.Id)))
                .Select(x => x.Id)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه اختصاص کاربر به درس ها
        /// </summary>
        /// <returns></returns>
        public IList<LessonViewModel> GetAllMyLesson(int id)
        {
            return _lessons
                .Where(x => x.Users.Any(y => y.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LessonViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت اختصاص کاربر به درس
        /// </summary>
        /// <param name="lesson_UserViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult SubmitChanges(Lesson_UserViewModel lesson_UserViewModel)
        {
            var previousLessons = _lessons.Include(x => x.Users)
                .Where(x => x.Users.Any(y => lesson_UserViewModel.UserIds.Contains(y.Id))).ToList();

            var previousUsers = _users.Include(x => x.Lessons)
                .Where(x => x.Lessons.Any(y => lesson_UserViewModel.LessonIds.Contains(y.Id))).ToList();

            //delete
            foreach (var user in previousUsers)
            {
                foreach (var lesson in previousLessons)
                {
                    if (lesson_UserViewModel.LessonIds.All(x => x != lesson.Id))
                        user.Lessons.Remove(lesson);
                }
            }

            foreach (var lesson in previousLessons)
            {
                foreach (var user in previousUsers)
                {
                    if (lesson_UserViewModel.UserIds.All(x => x != user.Id))
                        lesson.Users.Remove(user);
                }
            }

            //add
            foreach (var userId in lesson_UserViewModel.UserIds)
            {
                var user = _users.First(x => x.Id == userId);
                foreach (var lessonId in lesson_UserViewModel.LessonIds)
                {
                    if (previousLessons.All(x => x.Id != lessonId))
                    {
                        var lesson = _lessons.First(x => x.Id == lessonId);
                        lesson.Users.Add(user);
                    }
                }
            }

            foreach (var lessonId in lesson_UserViewModel.LessonIds)
            {
                var lesson = _lessons.First(x => x.Id == lessonId);
                foreach (var userId in lesson_UserViewModel.UserIds)
                {
                    if (previousUsers.All(x => x.Id != userId))
                    {
                        var user = _users.First(x => x.Id == userId);
                        user.Lessons.Add(lesson);
                    }
                }
            }

            var msgRes = _uow.CommitChanges();
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = lesson_UserViewModel;
            return clientResult;
        }


        /// <summary>
        /// ثبت اختصاص کاربر به درس
        /// </summary>
        /// <param name="lesson_UserViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult BuyLesson(BuyLessonViewModel lesson_UserViewModel)
        {

            var user = _users
                .FirstOrDefault(x => x.Id == lesson_UserViewModel.UserId);

            var lesson = _lessons.FirstOrDefault(x => x.Id == lesson_UserViewModel.LessonId);
            user.Lessons.Add(lesson);

            var msgRes = _uow.CommitChanges();
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = lesson_UserViewModel;
            return clientResult;
        }
    }
}
