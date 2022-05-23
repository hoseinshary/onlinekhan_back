using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.Common.ForQuestionMaking;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;

using NasleGhalam.ViewModels.Topic;
using Newtonsoft.Json;




namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionService
    {
        private const string Title = "سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Question> _questions;
        private readonly IDbSet<User> _users;
   

        private readonly Lazy<TopicService> _topicService;

        public QuestionService(IUnitOfWork uow, Lazy<TopicService> topicService)
        {
            _uow = uow;
            _questions = uow.Set<Question>();
           
            _users = uow.Set<User>();
      
            _topicService = topicService;



        }

        /// <summary>
        /// گرفتن  سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionViewModel GetById(int id)
        {
            //var question = _questions
            //    .Include(current => current.QuestionOptions)
            //    .Include(current => current.Topics)
            //    .Include(current => current.Tags)
            //    .Include(current => current.Lookup_AreaType)
            //    .Include(current => current.Writer)
            //    .Include(current => current.Supervisors)
            //    .Where(current => current.Id == id)
            //    .AsNoTracking()
            //    .AsEnumerable()
            //    .FirstOrDefault();
            return _questions
                .Where(x=>x.Deleted == false)
                .Include(current => current.Topics)
                .Include(current => current.Topics.Select(x => x.Lesson))
                .Include(current => current.Tags)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Writer)
                .Include(current => current.QuestionAnswers)
                .Include(current => current.Supervisors)

                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .FirstOrDefault();

            //returnVal.TopicAnswer.Clear();
            //var tempStringTopicAnswer = question.TopicAnswer.Split(',');
            //foreach (var item in tempStringTopicAnswer)
            //{
            //    returnVal.TopicAnswer.Add(item);
            //}

            //return returnVal;
        }

  


     



     


        public IList<QuestionViewModel> GetAllByTopicIdsNoAnswer(IEnumerable<int> ids)
        {

            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .Where(x => !x.QuestionAnswers.Any())
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();

        }

        public IList<QuestionViewModel> GetAllByTopicIdsActive(IEnumerable<int> ids)
        {

            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .Where(x => x.IsActive)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();

        }


     


        /// <summary>
        /// گرفتن همه سوال های مباحث
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByTopicIds(IEnumerable<int> ids)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


  



       

        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllActiveByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.IsActive)
                .Where(current => current.Topics.Any(x => x.LessonId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


  

        /// <summary>
        /// تعداد همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public int CountAllActive()
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Count(current => current.IsActive);
        }


        /// <summary>
        /// تعداد همه سوالات 
        /// </summary>
        /// <returns></returns>
        public int CountAll()
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Count();
        }





        /// <summary>
        /// گرفتن همه سوال های مباحث
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByTopicIdsForAssay(List<int> ids, int Page, int count)
        {
            var newIds = _topicService.Value.GetAllChildren(ids);
            

            return _questions
                .Where(x => x.Deleted == false)
                .Include(current => current.Writer)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Lookup_QuestionRank)
                .Include(current => current.Lookup_RepeatnessType)
                .Include(current => current.Lookup_QuestionHardnessType)
                .Where(current => current.Topics.Any(x =>
                    newIds.Contains(x.Id)))
                .OrderBy(x => Guid.NewGuid())
                .Skip(count * (Page - 1)).Take(count)
                //.Take(count)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();


            //var returnVal = new List<QuestionViewModel>();
            //foreach (var id in ids)
            //{
            //    var newIds = _topicService.Value.GetAllChildren(id).Select(x => x.Id);


            //    var questions = _questions
            //        .Include(current => current.Writer)
            //        .Include(current => current.Lookup_AreaTypes)
            //        .Include(current => current.Lookup_QuestionRank)
            //        .Include(current => current.Lookup_RepeatnessType)
            //        .Include(current => current.Lookup_QuestionHardnessType)
            //        .Where(current => current.Topics.Any(x =>
            //            newIds.Contains(x.Id)))
            //        .OrderBy(x => Guid.NewGuid())
            //        .Skip(count * (Page - 1)).Take(count)
            //        //.Take(count)
            //        .AsNoTracking()
            //        .AsEnumerable()
            //        .Select(Mapper.Map<QuestionViewModel>)
            //        .ToList();
            //    returnVal.AddRange(questions);
            //}



            //return returnVal;
        }

    

  

   


 








        public QuestionStatus GetQuestionStatus(int questionId)
        {
            var question = GetById(questionId);

            if (question.Topics.Count == 0)
                return QuestionStatus.Imported;
            else if (question.IsActive)
                return QuestionStatus.JudgedActive;
            else
                return QuestionStatus.JudgedInActive;


        }





    }
}
