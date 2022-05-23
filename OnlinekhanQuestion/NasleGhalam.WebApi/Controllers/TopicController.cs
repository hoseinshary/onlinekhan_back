using System.Collections.Generic;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.08.20
    /// </author>
    public class TopicController : ApiController
    {
        private readonly TopicService _topicService;
        private readonly LogService _logService;
        public TopicController(TopicService topicService, LogService logService)
        {
            _topicService = topicService;
            _logService = logService;
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_topicService.GetAllByLessonId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllByLessonIdWithCountQuestion(int id)
        {
            return Ok(_topicService.GetAllByLessonIdWithCountQuestion(id));
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_topicService.GetAll());
        }
        

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllChildren(List<int> ids)
        {
            return Ok(_topicService.GetAllChildren(ids));
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll3Level()
        {
            return Ok(_topicService.GetAll3Level());
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var topic = _topicService.GetById(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TopicCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TopicCreateViewModel topicViewModel)
        {
            var msgRes = _topicService.Create(topicViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Topic", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TopicUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TopicUpdateViewModel topicViewModel)
        {
            var msgRes = _topicService.Update(topicViewModel);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Update, "Topic", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpPost, CheckUserAccess(ActionBits.TopicDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _topicService.Delete(id);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Delete, "Topic", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }

        [HttpGet ,CheckUserAccess(ActionBits.TopicCreateAccess)]
        public IHttpActionResult CopyTopicsToLesson(int sourceId , int targetID)
        {
            var msgRes = _topicService.CopyTopicsToLesson(sourceId, targetID);
            if (msgRes.MessageType == MessageType.Success)
            {
                _logService.Create(CrudType.Create, "Topic-CopyToLesson", msgRes.Obj, Request.GetUserId());
            }
            return Ok(msgRes);
        }
    }
}
