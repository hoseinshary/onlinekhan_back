using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Media;

namespace NasleGhalam.ServiceLayer.Services
{
    public class MediaService
    {
        private const string Title = "رسانه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Media> _medias;

        public MediaService(IUnitOfWork uow)
        {
            _uow = uow;
            _medias = uow.Set<Media>();
        }

        /// <summary>
        /// گرفتن  رسانه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MediaViewModel GetById(int id)
        {
            return _medias
                .Include(current => current.Topics)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MediaViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه رسانه ها
        /// </summary>
        /// <returns></returns>
        public IList<MediaViewModel> GetAllByTopicIds(IEnumerable<int> ids)
        {
            return _medias                
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MediaViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه رسانه ها
        /// </summary>
        /// <returns></returns>
        public IList<MediaViewModel> GetAll()
        {
            return _medias
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<MediaViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت رسانه
        /// </summary>
        /// <param name="mediaViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(MediaCreateViewModel mediaViewModel, HttpPostedFile word, HttpPostedFile CoverImage)
        {
            
            mediaViewModel.FileName += Path.GetExtension(word.FileName);
            if(CoverImage == null)
            {
                var clientMessageResult = new ClientMessageResult() { Message = "عکس کاور خالی میباشد",MessageType = MessageType.Error };
                
                return clientMessageResult;
            }
            mediaViewModel.CoverImage += Path.GetExtension( CoverImage.FileName);
            var media = Mapper.Map<Media>(mediaViewModel);



            foreach (var topicId in mediaViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                media.Topics.Add(topic);
            }

            _medias.Add(media);
            _uow.ValidateOnSaveEnabled(false);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);

            if (serverResult.MessageType == MessageType.Success && word.FileName != null) 
            {
                word.SaveAs(SitePath.GetMediaAbsPath(mediaViewModel.FileName ) );
                if (CoverImage.FileName != null && CoverImage.FileName != "")
                {
                    CoverImage.SaveAs(SitePath.GetMediaAbsPath(mediaViewModel.CoverImage));
                }
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(media.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش رسانه
        /// </summary>
        /// <param name="mediaViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(MediaUpdateViewModel mediaViewModel, HttpPostedFile word, HttpPostedFile coverImage)
        {
            var mediaPrev = _medias
                .Include(current => current.Topics)
                .First(current => current.Id == mediaViewModel.Id);

            var previousFileName = mediaViewModel.FileName;
            var previousCoverImage = mediaViewModel.CoverImage;


            mediaPrev.Title = mediaViewModel.Title;
            mediaPrev.LookupId_MediaType = mediaViewModel.LookupId_MediaType;
            mediaPrev.Description = mediaViewModel.Description;
           
            mediaPrev.IsActive = mediaViewModel.IsActive;
            mediaPrev.WriterId = mediaViewModel.WriterId;
            mediaPrev.Price = mediaViewModel.Price;

            if (word != null && word.ContentLength > 0)
            {
                mediaPrev.FileName = Guid.NewGuid().ToString() + Path.GetExtension(word.FileName);
            }

            if (coverImage != null && coverImage.ContentLength > 0)
            {
                mediaPrev.CoverImage = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
            }
            


            //delete topics
            var deleteTopicList = mediaPrev.Topics
                .Where(oldTopic => mediaViewModel.TopicIds.All(newTopicId => newTopicId != oldTopic.Id))
                .ToList();
            foreach (var topic in deleteTopicList)
            {
                mediaPrev.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = mediaViewModel.TopicIds
                .Where(oldTopicId => mediaPrev.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                mediaPrev.Topics.Add(topic);
            }


            _uow.MarkAsChanged(mediaPrev);
            _uow.ValidateOnSaveEnabled(false);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);

            if (serverResult.MessageType == MessageType.Success && word != null && word.ContentLength > 0)
            {
                if (File.Exists(SitePath.GetMediaAbsPath(previousFileName)))
                {
                    File.Delete(SitePath.GetMediaAbsPath(previousFileName));
                }

                word.SaveAs(SitePath.GetMediaAbsPath(mediaPrev.FileName));

            }
            if (serverResult.MessageType == MessageType.Success && coverImage != null && coverImage.ContentLength > 0)
            {
                if (File.Exists(SitePath.GetMediaAbsPath(previousCoverImage)))
                {
                    File.Delete(SitePath.GetMediaAbsPath(previousCoverImage));
                }

                coverImage.SaveAs(SitePath.GetMediaAbsPath(mediaPrev.CoverImage));

            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(mediaPrev.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف رسانه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var media = _medias.Include(current => current.Topics).FirstOrDefault(current => current.Id == id);

            if (media == null)
            {
                return ClientMessageResult.NotFound();
            }

            //remove topics
            var topics = media.Topics.ToList();
            foreach (var item in topics)
            {
                media.Topics.Remove(item);
            }

            
            _uow.MarkAsDeleted(media);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            if (msgRes.MessageType == MessageType.Success)
            {
                if (File.Exists(SitePath.GetMediaAbsPath(media.FileName)))
                {
                    File.Delete(SitePath.GetMediaAbsPath(media.FileName));
                }

                if (File.Exists(SitePath.GetMediaAbsPath(media.CoverImage)))
                {
                    File.Delete(SitePath.GetMediaAbsPath(media.CoverImage));
                }
            }


            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
