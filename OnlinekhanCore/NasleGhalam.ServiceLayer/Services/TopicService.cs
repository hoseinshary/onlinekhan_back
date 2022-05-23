using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TopicService
    {
        private const string Title = "مبحث";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Topic> _topics;
        private readonly IDbSet<Question> _questions;



        public TopicService(IUnitOfWork uow)
        {
            _uow = uow;
            _topics = uow.Set<Topic>();
            _questions = uow.Set<Question>();

        }

        /// <summary>
        /// گرفتن  مبحث با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopicViewModel GetById(int id)
        {
            return _topics
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه مبحث ها با آی دی درس
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAllByLessonId(int id)
        {
            return _topics
                .Where(current => current.LessonId == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();
            //var lessons = _lessonDepartmentService.Value.GetAllLessonOfDepartment(id);

            //List<TopicViewModel> returnList = new List<TopicViewModel>();

            //foreach (var item in lessons)
            //{
            //    var topics = _topics
            //    .Where(current => current.LessonId == item.Id)
            //    .AsNoTracking()
            //    .AsEnumerable()
            //    .Select(Mapper.Map<TopicViewModel>)
            //    .ToList();

            //    returnList.AddRange(topics);
            //}
            //return returnList;

        }




        /// <summary>
        /// گرفتن همه مبحث ها با آی دی درس
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAllByLessonIdWithCountQuestion(int id)
        {
            var topics = _topics
                .Where(current => current.LessonId == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();
            var questions = _questions.Select(x => new { x.Id, x.Topics })
                .ToList();
            var questions2 = _questions.Select(x => x.Topics.Select(y => y.Id))
                .ToList();
            foreach (var topicViewModel in topics)
            {
                topics.Find(x => x.Id == topicViewModel.Id).Title += " (" + getCountTopic(topicViewModel.Id, topics, questions) + ")";
            }

            return topics;
        }



        private int getCountTopic(int id, List<TopicViewModel> topics, IEnumerable<object> questions)
        {
            var count = _questions.Count(current => current.Topics.Any(x => x.Id == id));
            //var count = questions.Count(current => current.Topics.Any(x => x.Id == id))

            if (count == 0 && topics.Any(x => x.ParentTopicId == id))
            {
                foreach (var childTopic in topics.Where(x => x.ParentTopicId == id))
                {
                    count += getCountTopic(childTopic.Id, topics, questions);
                }

                return count;
            }
            else
            {
                return count;
            }

        }

        /// <summary>
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAll()
        {
            return _topics
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه مبحث ها
        /// </summary>
        /// <returns></returns>
        public IList<int> GetAllChildren(List<int> ids)
        {


            var allTopics = _topics.Where(x => x.LessonId == _topics.FirstOrDefault(y => y.Id == ids.FirstOrDefault()).LessonId).AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();

            var returnIds = new List<int>();

            foreach (var id in ids)
            {
                if (allTopics.Count(x => x.ParentTopicId == id) == 0)
                {
                    returnIds.Add(allTopics.Where(x => x.Id == id).Select(y => y.Id).FirstOrDefault());

                }
                else
                {
                    var topicsChildTemp = new List<TopicViewModel>();

                    topicsChildTemp.AddRange(allTopics.Where(x => x.ParentTopicId == id));
                    while (true)
                    {

                        foreach (var topic in topicsChildTemp.ToList())
                        {
                            if (allTopics.Count(x => x.ParentTopicId == topic.Id) == 0)
                            {
                                returnIds.Add(allTopics.Where(x => x.Id == topic.Id).Select(y => y.Id).FirstOrDefault());
                                topicsChildTemp.Remove(topic);
                            }
                            else
                            {
                                topicsChildTemp.Remove(topic);
                                topicsChildTemp.AddRange(allTopics.Where(x => x.ParentTopicId == topic.Id));
                            }
                        }

                        if (topicsChildTemp.Count == 0)
                            break;
                    }

                }
            }

            return returnIds;
        }

        /// <summary>
        ///  گرفتن همه مبحث ها تا سطح 3
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel> GetAll3Level()
        {
            List<TopicViewModel> returnVal = new List<TopicViewModel>();

            var allTopics = _topics
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();

            var level1 = allTopics
                .Where(x => x.ParentTopicId == null).ToList();


            returnVal.AddRange(level1);


            var level2 = allTopics
                .Where(x => level1.Select(y => y.Id).ToList().Contains(x.ParentTopicId == null ? 0 : Convert.ToInt32(x.ParentTopicId)))
                .ToList();

            returnVal.AddRange(level2);

            var level3 = allTopics
                .Where(x => level2.Select(y => y.Id).ToList().Contains(x.ParentTopicId == null ? 0 : Convert.ToInt32(x.ParentTopicId)))
                .ToList();

            returnVal.AddRange(level3);

            return returnVal;
        }


        /// <summary>
        ///  گرفتن همه مبحث ها تا سطح 3
        /// </summary>
        /// <returns></returns>
        public IList<TopicViewModel>











            GetAll4LevelByLessonId(int id)
        {
            List<TopicViewModel> returnVal = new List<TopicViewModel>();

            var allTopics = _topics.Where(x => x.LessonId == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<TopicViewModel>)
                .ToList();

            var level1 = allTopics
                .Where(x => x.ParentTopicId == null).ToList();


            returnVal.AddRange(level1);


            var level2 = allTopics
                .Where(x => level1.Select(y => y.Id).ToList().Contains(x.ParentTopicId == null ? 0 : Convert.ToInt32(x.ParentTopicId)))
                .ToList();

            returnVal.AddRange(level2);

            var level3 = allTopics
                .Where(x => level2.Select(y => y.Id).ToList().Contains(x.ParentTopicId == null ? 0 : Convert.ToInt32(x.ParentTopicId)))
                .ToList();

            returnVal.AddRange(level3);

            var level4 = allTopics
                .Where(x => level3.Select(y => y.Id).ToList().Contains(x.ParentTopicId == null ? 0 : Convert.ToInt32(x.ParentTopicId)))
                .ToList();

            returnVal.AddRange(level4);

            return returnVal;
        }


       

     

        /// <summary>
        /// حذف مبحث
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var topicViewModel = GetById(id);
            if (topicViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var topic = Mapper.Map<Topic>(topicViewModel);
            _uow.MarkAsDeleted(topic);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }



        /// <summary>
        /// کپی مبجث های یک درس به درس دیگر
        /// </summary>

        public ClientMessageResult CopyTopicsToLesson(int LessonIdSource, int LessonIdTarget)
        {
            var sourceTopic = _topics.Where(x => x.LessonId == LessonIdSource).AsNoTracking().OrderBy(x => x.ParentTopicId).ToList();
            if (sourceTopic == null)
            {
                return ClientMessageResult.NotFound();
            }

            var topic = sourceTopic.Where(x => x.ParentTopicId == null).First();

            if (topic.ParentTopicId == null)
            {
                topic.LessonId = LessonIdTarget;
                Topic root = _topics.Add(topic);
                var childTopics = sourceTopic.Where(x => x.ParentTopicId == root.Id);
                foreach (Topic childTopic in childTopics)
                {
                    childTopic.LessonId = LessonIdTarget;
                    root.ChildrenTopic.Add(childTopic);
                    var childTopics2 = sourceTopic.Where(x => x.ParentTopicId == childTopic.Id);
                    foreach (Topic childTopic2 in childTopics2)
                    {
                        childTopic2.LessonId = LessonIdTarget;
                        childTopic.ChildrenTopic.Add(childTopic2);
                        var childTopics3 = sourceTopic.Where(x => x.ParentTopicId == childTopic2.Id);
                        foreach (Topic childTopic3 in childTopics3)
                        {
                            childTopic3.LessonId = LessonIdTarget;
                            childTopic2.ChildrenTopic.Add(childTopic3);
                            var childTopics4 = sourceTopic.Where(x => x.ParentTopicId == childTopic3.Id);
                            foreach (Topic childTopic4 in childTopics4)
                            {
                                childTopic4.LessonId = LessonIdTarget;
                                childTopic3.ChildrenTopic.Add(childTopic4);
                                var childTopics5 = sourceTopic.Where(x => x.ParentTopicId == childTopic4.Id);
                                foreach (Topic childTopic5 in childTopics5)
                                {
                                    childTopic5.LessonId = LessonIdTarget;
                                    childTopic4.ChildrenTopic.Add(childTopic5);
                                    var childTopics6 = sourceTopic.Where(x => x.ParentTopicId == childTopic5.Id);
                                    foreach (Topic childTopic6 in childTopics6)
                                    {
                                        childTopic6.LessonId = LessonIdTarget;
                                        childTopic5.ChildrenTopic.Add(childTopic6);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }




    }
}
