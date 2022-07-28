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

      
        

        

     


       

        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllActiveByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.IsActive)
                //.Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
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

       
        
        
        

        /// <summary>
        /// ذخیره عکس فایل ورد
        /// </summary>
        public static void SaveOptionsOfQuestions(Document source, Document target, string FileName, int answer)
        {
            //تریک درست شدن گزینه ها 
            source.ActiveWindow.Selection.WholeStory();
            source.ActiveWindow.Selection.Copy();
            target.ActiveWindow.Selection.Paste();
            target.ActiveWindow.Selection.WholeStory();
            target.ActiveWindow.Selection.Delete();
            target.ActiveWindow.Selection.Paste();

            int j = source.Paragraphs.Count;
            int k = 0;
            Paragraph p1 = null, p2 = null, p3 = null, p4 = null;
            int i1 = 0, i2 = 0, i3 = 0, i4 = 0;
            while (j > 0 && k < 4)
            {
                if (source.Paragraphs[j].Range.Text != "\f" && source.Paragraphs[j].Range.Text != "\f\r"
                                                            && source.Paragraphs[j].Range.Text != "\r")
                {
                    k++;
                    switch (k)
                    {
                        case 1:
                            p4 = source.Paragraphs[j];
                            i4 = j;
                            break;
                        case 2:
                            p3 = source.Paragraphs[j];
                            i3 = j;
                            break;
                        case 3:
                            p2 = source.Paragraphs[j];
                            i2 = j;
                            break;
                        case 4:
                            p1 = source.Paragraphs[j];
                            i1 = j;
                            break;
                    }


                }
                j--;
            }

            var filename3 = Encryption.Base64Encode(Encryption.Encrypt(answer + "-" + FileName));
            source.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");
        }




        /// <summary>
        /// پاک کردن فایل های گزینه یک سوال 
        /// </summary>
        public static void DeleteOptionsOfQuestion(string FileName)
        {
            var filename1 = Encryption.Base64Encode(Encryption.Encrypt(1 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(2 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(3 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(0 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
        }


        
        





    }
}
