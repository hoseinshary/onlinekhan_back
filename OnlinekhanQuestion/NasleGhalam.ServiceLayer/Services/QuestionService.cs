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
using NasleGhalam.ViewModels.Report;
using NasleGhalam.ViewModels.QuestionUpdate;
using NasleGhalam.ViewModels.QuestionJudge;
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
        private readonly IDbSet<QuestionJudge> _questionJudges;
        private readonly IDbSet<QuestionAnswerJudge> _questionAnswerJudges;

        private readonly Lazy<QuestionGroupService> _questionGroupService;
        private readonly Lazy<QuestionUpdateService> _questionUpdateService;
        private readonly Lazy<TopicService> _topicService;

        public QuestionService(IUnitOfWork uow, Lazy<QuestionGroupService> questionGroupService, Lazy<QuestionUpdateService> questionUpdate ,Lazy<TopicService> topicService)
        {
            _uow = uow;
            _questions = uow.Set<Question>();
            _questionJudges = uow.Set<QuestionJudge>();
            _questionAnswerJudges = uow.Set<QuestionAnswerJudge>();
            _users = uow.Set<User>();
            _questionGroupService = questionGroupService;
            _questionUpdateService = questionUpdate;
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
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Topics.Select(x => x.Lesson))
                .Include(current => current.Tags)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Writer)
                .Include(current => current.QuestionAnswers)
                .Include(current => current.Supervisors)
                .Include(current => current.QuestionJudges)

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

        public IList<AllUsersReporQuestionViewModel> GetAllUsersReport()
        {

            return _users.Where(x => x.Role.Level == 3 || x.Role.Level == 6).Where(x => x.IsActive == true)
                .Select(x => new AllUsersReporQuestionViewModel
                {
                    Name = x.Name,
                    Family = x.Family,
                    NumberOfQuestionAnswerJudged = x.QuestionAnswerJudges.Where(f=>f.QuestionAnswer.Question.Deleted == false).Count(),
                    NumberOfQuestionJudged = x.QuestionJudges.Where(f=> f.Question.Deleted == false).Count(),
                    NumberOfQuestionTopiced = x.QuestionsUpdates.Where(current =>current.Question.Deleted == false && current.QuestionActivity == QuestionActivity.Topic).Count(),
                    NumberOfSupervisorQuestion = x.SupervisorQuestions.Where(current => current.Deleted==false).Count(),
                    NumberOfWriteQuestion = _questions.Where(current => current.Deleted == false).Count(y => y.Writer.User.Id == x.Id),
                    Department = x.Lessons.FirstOrDefault().LessonDepartments.FirstOrDefault().Name
                }).ToList();

        }


        public IList<QuestionReportViewModel> GetAllQuestionsReport(FilterQuestionReportViewModel filterQuestionReport)
        {


            return _questions
                .Where(x => x.Deleted == false)
                .Include(x => x.Topics)
                .Include(x => x.Topics.Select(y => y.Lesson))
                .Include(x => x.QuestionJudges)
                .Include(x => x.QuestionUpdates)
                .Include(x => x.QuestionUpdates.Select(y => y.User))
                .Include(x => x.QuestionAnswers)
                .Include(x => x.QuestionAnswers.Select(y => y.Writer))
                .Include(x => x.QuestionAnswers.Select(y => y.QuestionAnswerJudges))
                .Include(x => x.Lookup_AuthorType)
                .Include(x => x.Lookup_QuestionType)
                .Include(x => x.Supervisors)
                .Include(x => x.Writer)
                .Where(x => x.QuestionGroups.Any(y => y.LessonId == filterQuestionReport.LessonId))
                .AsNoTracking()
                .AsEnumerable()
                .Select(z => new QuestionReportViewModel
                {
                    Id = z.Id,
                    IsActive = z.IsActive,
                    UserId = z.QuestionUpdates.Where(p => p.QuestionActivity == QuestionActivity.Import).Select(q => q.UserId).FirstOrDefault(),
                    IsDelete = z.IsDelete,
                    IsUpdate = z.IsUpdate,
                    LookupId_AuthorType = z.LookupId_AuthorType,
                    WriterId = z.WriterId,
                    AuthorTypeName = z.Lookup_AuthorType.Value,
                    SupervisorUserId = z.Supervisors.Count == 0 ? 0 : z.Supervisors.FirstOrDefault() == null ? 0 : z.Supervisors.FirstOrDefault().Id,
                    SupervisorName = z.Supervisors.Count == 0 ? "" : z.Supervisors.FirstOrDefault().Name + z.Supervisors.FirstOrDefault().Family,
                    WriterName = z.Writer.Name,
                    QuestionJudges = z.QuestionJudges.Select(Mapper.Map<QuestionJudgeViewModel>).ToList(),
                    Topics = z.Topics.Select(Mapper.Map<TopicViewModel>).ToList(),
                    NumberOfAnswers = z.QuestionAnswers.Count,
                    QuestionJudgedState1 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(0).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(0).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    QuestionJudgedState2 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(1).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(1).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    QuestionJudgedState3 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(2).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(2).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    EditorUserId = z.QuestionUpdates.Count == 0 ? 0 : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).UserId,
                    EditorUserName = z.QuestionUpdates.Count == 0 ? "" : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).User.Name + z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).User.Family,
                    LookupId_QuestionType = z.LookupId_QuestionType,
                    QuestionTypeName = z.Lookup_QuestionType.Value,
                    TopicUserId = z.QuestionUpdates.Count == 0 ? 0 : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).UserId,
                    TopicUserName = z.QuestionUpdates.Count == 0 ? "" : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).User.Name + z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).User.Family,
                    AnswerWriterId = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).WriterId,
                    AnswerWriterName = z.QuestionAnswers.Count == 0 ? "" : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).Writer.Name,
                    QuestionAnswerType = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).QuestionAnswerType,
                    HaveOnlinekhanAnswer = z.QuestionAnswers.Count(p => p.IsMaster) != 0,
                    AnswerJudgedState1 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(0).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(0).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    AnswerJudgedState2 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(1).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(1).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    AnswerJudgedState3 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(2).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(2).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,





                }
            ).ToList();

        }

        public string GetAllQuestionsReportExcel(FilterQuestionReportViewModel filterQuestionReport)
        {


            var returnVal = _questions
                .Where(x => x.Deleted == false)
                .Include(x => x.Topics)
                .Include(x => x.Topics.Select(y => y.Lesson))
                .Include(x => x.QuestionJudges)
                .Include(x => x.QuestionUpdates)
                .Include(x => x.QuestionUpdates.Select(y => y.User))
                .Include(x => x.QuestionAnswers)
                .Include(x => x.QuestionAnswers.Select(y => y.Writer))
                .Include(x => x.QuestionAnswers.Select(y => y.QuestionAnswerJudges))
                .Include(x => x.Lookup_AuthorType)
                .Include(x => x.Lookup_QuestionType)
                .Include(x => x.Supervisors)
                .Include(x => x.Writer)
                .Where(x => x.QuestionGroups.Any(y => y.LessonId == filterQuestionReport.LessonId))
                .AsNoTracking()
                .AsEnumerable()
                .Select(z => new QuestionReportViewModel
                {
                    Id = z.Id,
                    IsActive = z.IsActive,
                    UserId = z.QuestionUpdates.Where(p => p.QuestionActivity == QuestionActivity.Import).Select(q => q.UserId).FirstOrDefault(),
                    IsDelete = z.IsDelete,
                    IsUpdate = z.IsUpdate,
                    LookupId_AuthorType = z.LookupId_AuthorType,
                    WriterId = z.WriterId,
                    AuthorTypeName = z.Lookup_AuthorType.Value,
                    SupervisorUserId = z.Supervisors.Count == 0 ? 0 : z.Supervisors.FirstOrDefault() == null ? 0 : z.Supervisors.FirstOrDefault().Id,
                    SupervisorName = z.Supervisors.Count == 0 ? "" : z.Supervisors.FirstOrDefault().Name + z.Supervisors.FirstOrDefault().Family,
                    WriterName = z.Writer.Name,
                    QuestionJudges = z.QuestionJudges.Select(Mapper.Map<QuestionJudgeViewModel>).ToList(),
                    Topics = z.Topics.Select(Mapper.Map<TopicViewModel>).ToList(),
                    NumberOfAnswers = z.QuestionAnswers.Count,
                    QuestionJudgedState1 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(0).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(0).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    QuestionJudgedState2 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(1).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(1).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    QuestionJudgedState3 = z.QuestionJudges.Count < 3 ? 0 : z.QuestionJudges.OrderByDescending(p => p.Id).Skip(2).Take(1).FirstOrDefault().IsActiveQuestion ? QuestionJudgedState.Active :
                        z.QuestionJudges.OrderByDescending(p => p.Id).Skip(2).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    EditorUserId = z.QuestionUpdates.Count == 0 ? 0 : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).UserId,
                    EditorUserName = z.QuestionUpdates.Count == 0 ? "" : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).User.Name + z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.UpdateEditor).User.Family,
                    LookupId_QuestionType = z.LookupId_QuestionType,
                    QuestionTypeName = z.Lookup_QuestionType.Value,
                    TopicUserId = z.QuestionUpdates.Count == 0 ? 0 : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).UserId,
                    TopicUserName = z.QuestionUpdates.Count == 0 ? "" : z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).User.Name + z.QuestionUpdates.OrderByDescending(p => p.Id).FirstOrDefault(p => p.QuestionActivity == QuestionActivity.Topic).User.Family,
                    AnswerWriterId = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).WriterId,
                    AnswerWriterName = z.QuestionAnswers.Count == 0 ? "" : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).Writer.Name,
                    QuestionAnswerType = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster == true).QuestionAnswerType,
                    HaveOnlinekhanAnswer = z.QuestionAnswers.Count(p => p.IsMaster) != 0,
                    AnswerJudgedState1 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(0).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(0).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    AnswerJudgedState2 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(1).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(1).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,
                    AnswerJudgedState3 = z.QuestionAnswers.Count == 0 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.Count < 3 ? 0 : z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(2).Take(1).FirstOrDefault().IsActiveQuestionAnswer ? QuestionJudgedState.Active :
                        z.QuestionAnswers.FirstOrDefault(p => p.IsMaster).QuestionAnswerJudges.OrderByDescending(q => q.Id).Skip(2).Take(1).FirstOrDefault().IsUpdate ? QuestionJudgedState.Update : QuestionJudgedState.Delete,





                }
            ).ToList();

            var questionList = Mapper.Map<List<QuestionReportExcelViewModel>>(returnVal);
            System.Data.DataTable dt = Utility.ConvertToDataTable<QuestionReportExcelViewModel>(questionList);

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt);

            // create a excel app along side with workbook and worksheet and give a name to it  
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            // excelApp.Visible = true;
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = excelWorkBook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            foreach (System.Data.DataTable table in dataSet.Tables)
            {
                //Add a new worksheet to workbook with the Datatable name  
                Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                // add all the columns  
                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                // add all the rows  
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            // excelApp.Save(SitePath.GetQuestionGroupTempAbsPath("reportResult") + ".xlsx");
            var filename = SitePath.GetQuestionGroupTempAbsPath("reportResult_") + Guid.NewGuid() + ".xlsx";
            excelWorkBook.SaveAs(filename);

            excelWorkBook.Close();
            excelApp.Quit();
            return filename;


        }



        public IList<QuestionViewModel> GetAllByTopicIdsNoJudge(IEnumerable<int> ids, int userid, int rollLevel)
        {
            if (rollLevel < 3)
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    .Where(x => x.QuestionJudges.Count < x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }
            else
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    .Where(current =>
                        current.QuestionJudges.All(x => x.UserId != userid) /*|| !current.QuestionJudges.Any()*/)
                    //اگه 3 کارشناسی داریم نفر 4ام دیگر نبیند سوال کارشناسی را 
                    //  .Where(current => current.QuestionJudges.Count >= current.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }
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


        public IList<QuestionViewModel> GetAllByTopicIdsNoAnswerJudge(IEnumerable<int> ids, int userid, int rollLevel)
        {

            if (rollLevel < 3)
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    //.Where(x => x.QuestionAnswers.All(y => y.QuestionAnswerJudges.Any()))
                    .Where(x => x.QuestionAnswers.Any(y => y.QuestionAnswerJudges.Count < x.Topics.FirstOrDefault().Lesson.NumberOfJudges))

                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }
            else
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    .Where(x => x.QuestionAnswers.All(y => y.QuestionAnswerJudges.All(z => z.UserId != userid)))
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }

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
        /// تعداد همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public int CountAllJudgedByUserId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.QuestionJudges.Any(x => x.UserId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Count();
        }



        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر مربوط به درس
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllJudgedByUserIdByLessonId(int userId, int rolllevel, int lessonId)
        {
            if (rolllevel < 3)
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(x => x.QuestionGroups.Any(y => y.LessonId == lessonId))
                    .Where(x => x.QuestionJudges.Count >= x.QuestionGroups.FirstOrDefault().Lesson.NumberOfJudges)
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();

            }
            else
            {
                return _questions
                    .Where(x => x.Deleted == false)
                    .Where(current => current.QuestionJudges.Any(x => x.UserId == userId))
                    .Where(x => x.QuestionGroups.Any(y => y.LessonId == lessonId))
                    //.Where(x => x.QuestionJudges.Count >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();


            }
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
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllUnActiveByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => !current.IsActive)
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .Where(x => x.QuestionJudges.Count >= x.QuestionGroups.FirstOrDefault().Lesson.NumberOfJudges)
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
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllJudgedByUserId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.QuestionJudges.Any(x => x.UserId == id))
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
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
        /// گرفتن همه سوال های یک درس که مبحث ندارند
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllNoTopicByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .Where(x => !x.Topics.Any())
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های یک درس
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های یک درس کارشناسی شده کامل به اندازه تعداد کارشناسان آن درس
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllJudgedByLessonId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .Where(x => x.QuestionJudges.Count >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه سوال های سوال گروهی
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByQuestionGroupId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Include(current => current.QuestionAnswers)
                //.Include(current => current.QuestionAnswers.OrderBy(y => y.IsMaster))
                .Where(current => current.QuestionGroups.Any(x => x.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه سوال های سوال گروهی
        /// </summary>
        /// <returns></returns>
        public IList<Question> GetAllQuestionsByQuestionGroupId(int id)
        {
            return _questions
                .Where(x => x.Deleted == false)
                .Include(current => current.QuestionAnswers)
                .Where(current => current.QuestionGroups.Any(x => x.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }


        /// <summary>
        /// ثبت سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionCreateViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);

            string wordFilename = null;
            ServerMessageResult serverResult = null;
            if (questionViewModel.FileBytes.Length > 0)
            {
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";

                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
                // Open a doc file.
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                Document source = app.Documents.Open(wordFilename);

                //حذف عدد اول سوال
                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }

                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

                foreach (var topicId in questionViewModel.TopicIds)
                {
                    var topic = new Topic() { Id = topicId };
                    _uow.MarkAsUnChanged(topic);
                    question.Topics.Add(topic);
                }

                foreach (var tagId in questionViewModel.TagIds)
                {
                    var tag = new Tag() { Id = tagId };
                    _uow.MarkAsUnChanged(tag);
                    question.Tags.Add(tag);
                }

                foreach (var option in questionViewModel.Options)
                {
                    var newOption = new QuestionOption()
                    {
                        Context = option.Context,
                        IsAnswer = option.IsAnswer,
                    };

                    question.QuestionOptions.Add(newOption);
                }

                if (questionViewModel.SupervisorUserId != 0)
                {
                    var supervisor = new User() { Id = questionViewModel.SupervisorUserId };
                    _uow.MarkAsUnChanged(supervisor);
                    question.Supervisors.Add(supervisor);
                }

                

                _questions.Add(question);
                _uow.ValidateOnSaveEnabled(false);
                serverResult = _uow.CommitChanges(CrudType.Create, Title);


                if (serverResult.MessageType == MessageType.Success)
                {
                    _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                    {
                        QuestionId = question.Id,
                        UserId = questionViewModel.UserId,
                        DateTime = DateTime.Now,
                        QuestionActivity = QuestionActivity.Import,
                        Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                    });
                }


                if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName))
                {
                    using (var ms = new MemoryStream(questionViewModel.FileBytes))
                    {
                        using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                        {
                            ms.WriteTo(file);
                        }
                    }
                    source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                    //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                    //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                    ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                    File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");


                    ////save Options

                    // var target = app.Documents.Add();
                    //SaveOptionsOfQuestions(source, target, question.FileName, question.AnswerNumber);
                    //target.Close();

                }

                source.Close();
                app.Quit();
                File.Delete(wordFilename);

            }
            else
            {
                serverResult = new ServerMessageResult()
                {
                    MessageType = MessageType.Error,
                    FaMessage = "فایل وارد نشده است!"
                };
            }



            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// ثبت سوال برای برنامه ویندوزی
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// /// <param name="png"></param>
        /// <returns></returns>
        public ClientMessageResult CreateForWindowsApp(QuestionCreateWindowsViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);

            if (questionViewModel.SupervisorUserId > 0)
            {
                var supervisor = new User() { Id = questionViewModel.SupervisorUserId };
                _uow.MarkAsUnChanged(supervisor);
                question.Supervisors.Add(supervisor);
            }

            //var questionGroup = _questionGroupService.Value.GetById(questionViewModel.QuestionGroupId);

            if (questionViewModel.QuestionGroupId > 0)
            {
                var questionGroup = new QuestionGroup() { Id = questionViewModel.QuestionGroupId };
                _uow.MarkAsUnChanged(questionGroup);
                question.QuestionGroups.Add(questionGroup);
            }

            _questions.Add(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                {
                    QuestionId = question.Id,
                    UserId = questionViewModel.UserId,
                    DateTime = DateTime.Now,
                    QuestionActivity = QuestionActivity.Import,
                    Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                });
            }

            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName)
            && questionViewModel.WordFileBytes.Length > 0 &&  questionViewModel.PngFileBytes.Length > 0)
            {
                
                using (var ms = new MemoryStream(questionViewModel.WordFileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
            
                using (var ms = new MemoryStream(questionViewModel.PngFileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".png", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
              
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
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




        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Update(ViewModels.Question.QuestionUpdateViewModel questionViewModel)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Supervisors)
                .First(current => current.Id == questionViewModel.Id);



            var previousFileName = questionViewModel.FileName;
            Microsoft.Office.Interop.Word.Application app = null;
            Document source = null;
            string wordFilename = null;
            var haveFileUpdate = false;
            if (questionViewModel.FileBytes.Length > 0)
            {
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                haveFileUpdate = true;
                questionViewModel.FileName = Guid.NewGuid().ToString();


                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);

                //حذف عدد اول سوال
                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }

                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

            }

            question.WriterId = questionViewModel.WriterId;
            question.Description = questionViewModel.Description;
            question.InsertDateTime = DateTime.Now;
            question.IsActive = questionViewModel.IsActive;
            question.IsStandard = questionViewModel.IsStandard;
            //question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionHardnessType = questionViewModel.LookupId_QuestionHardnessType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.LookupId_RepeatnessType = questionViewModel.LookupId_RepeatnessType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.QuestionPoint = questionViewModel.QuestionPoint;
            question.ResponseSecond = questionViewModel.ResponseSecond;
            question.UseEvaluation = questionViewModel.UseEvaluation;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.IsDelete = questionViewModel.IsDelete;
            question.IsHybrid = questionViewModel.IsHybrid;
            question.TopicAnswer = questionViewModel.TopicAnswer;
            question.FileName = questionViewModel.FileName;


            //delete areaTypes
            var deleteAreaTypes = question.Lookup_AreaTypes.Where(oldArea => questionViewModel.LookupId_AreaTypes.All(newAreaId => newAreaId != oldArea.Id)).ToList();

            foreach (var area in deleteAreaTypes)
            {
                question.Lookup_AreaTypes.Remove(area);
            }

            //add areaTypes
            var addAreaTypeList = questionViewModel.LookupId_AreaTypes
                .Where(oldAreaId => question.Lookup_AreaTypes.All(newArea => newArea.Id != oldAreaId))
                .ToList();
            foreach (var area in addAreaTypeList)
            {
                var newArea = new Lookup() { Id = area };
                _uow.MarkAsUnChanged(newArea);
                question.Lookup_AreaTypes.Add(newArea);
            }


            //delete topics
            var deleteTopicList = question.Topics
                .Where(oldTopic => questionViewModel.TopicIds.All(newTopicId => newTopicId != oldTopic.Id))
                .ToList();
            foreach (var topic in deleteTopicList)
            {
                question.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = questionViewModel.TopicIds
                .Where(oldTopicId => question.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            ////delete supervisor
            //var supervisorsList = question.Supervisors.ToList();
            //foreach (var user in supervisorsList)
            //{
            //    question.Supervisors.Remove(user);
            //}

            //add supervisor
            if (questionViewModel.SupervisorUserId != 0 && questionViewModel.SupervisorUserId != question.Supervisors.FirstOrDefault().Id)
            {
                question.Supervisors.Remove(question.Supervisors.FirstOrDefault());
                var supervisor = new User() { Id = questionViewModel.SupervisorUserId };

                _uow.MarkAsUnChanged(supervisor);
                question.Supervisors.Add(supervisor);
            }

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            serverResult.Id = question.Id;
            if (serverResult.MessageType == MessageType.Success)
            {
                _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                {
                    QuestionId = question.Id,
                    UserId = questionViewModel.UserId,
                    DateTime = DateTime.Now,
                    QuestionActivity = QuestionActivity.UpdateAdmin,
                    Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                });
            }

            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) &&
                !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }

                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                DeleteOptionsOfQuestion(previousFileName);


                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
                while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");


                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }
            else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            {
                //save Doc file in temp memory

                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);

                DeleteOptionsOfQuestion(previousFileName);

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// ویرایش سوال بعد از ورود گروهی سوال 
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult UpdateImport(QuestionUpdateImportViewModel questionViewModel)
        {

            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.Supervisors)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = question.FileName;
            Microsoft.Office.Interop.Word.Application app = null;
            Document source = null;
            string wordFilename = null;
            var haveFileUpdate = false;
            if (questionViewModel.FileBytes.Length > 0)
            {
                questionViewModel.FileName = Guid.NewGuid().ToString();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                haveFileUpdate = true;



                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);


                //حذف عدد اول سوال
                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }
                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

            }

            question.WriterId = questionViewModel.WriterId;
            question.Description = questionViewModel.Description;
            //question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.FileName = questionViewModel.FileName;
            question.IsDelete = questionViewModel.IsDelete;
            question.IsHybrid = questionViewModel.IsHybrid;
            question.IsActive = questionViewModel.IsActive;

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }


            //add supervisor
            if (questionViewModel.SupervisorUserId != 0 && questionViewModel.SupervisorUserId != question.Supervisors.FirstOrDefault().Id)
            {
                question.Supervisors.Remove(question.Supervisors.FirstOrDefault());
                var supervisor = new User() { Id = questionViewModel.SupervisorUserId };

                _uow.MarkAsUnChanged(supervisor);
                question.Supervisors.Add(supervisor);
            }

            _uow.MarkAsChanged(question);
            if (question.FileName == null)
            {
                _uow.ExcludeFieldsFromUpdate(question, x => x.FileName);
            }
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                {
                    QuestionId = question.Id,
                    UserId = questionViewModel.UserId,
                    DateTime = DateTime.Now,
                    QuestionActivity = QuestionActivity.UpdateImport,
                    Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                });
            }

            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }

                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                DeleteOptionsOfQuestion(previousFileName);

                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }
            else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            {
                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);

                DeleteOptionsOfQuestion(previousFileName);

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }


        /// <summary>
        /// ویرایش سوال ویراستار نهایی 
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult UpdateFinalImport(QuestionUpdateImportViewModel questionViewModel)
        {

            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.Supervisors)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = question.FileName;
            Microsoft.Office.Interop.Word.Application app = null;
            Document source = null;
            string wordFilename = null;
            var haveFileUpdate = false;
            if (questionViewModel.FileBytes.Length > 0)
            {
                questionViewModel.FileName = Guid.NewGuid().ToString();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                haveFileUpdate = true;



                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);


                //حذف عدد اول سوال
                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }
                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

            }



            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.FileName = questionViewModel.FileName;


            question.IsActive = questionViewModel.IsActive;

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }


            _uow.MarkAsChanged(question);
            if (question.FileName == null)
            {
                _uow.ExcludeFieldsFromUpdate(question, x => x.FileName);
            }
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                {
                    QuestionId = question.Id,
                    UserId = questionViewModel.UserId,
                    DateTime = DateTime.Now,
                    QuestionActivity = QuestionActivity.UpdateEditor,
                    Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                });
            }

            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }

                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                DeleteOptionsOfQuestion(previousFileName);

                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }
            else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            {
                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Microsoft.Office.Interop.Word.Application();
                source = app.Documents.Open(wordFilename);

                DeleteOptionsOfQuestion(previousFileName);

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }



        /// <summary>
        /// ویرایش سوال انتساب مبحث
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult UpdateTopic(QuestionUpdateTopicViewModel questionViewModel, int userUpdateId)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)

                .First(current => current.Id == questionViewModel.Id);


            //Application app = null;
            //Document source = null;
            //string wordFilename = null;
            //var haveFileUpdate = false;
            //if (word != null && word.ContentLength > 0)
            //{
            //    wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
            //    haveFileUpdate = true;
            //    questionViewModel.FileName = Guid.NewGuid().ToString();


            //    //save Doc file in temp memory
            //    word.SaveAs(wordFilename);

            //    // Open a doc file.
            //    app = new Application();
            //    source = app.Documents.Open(wordFilename);

            //    //حذف عدد اول سوال
            //    if (QuestionGroupService.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
            //    {
            //        int i = 1;
            //        while (i < source.Paragraphs[1].Range.Characters.Count &&
            //               source.Paragraphs[1].Range.Characters[i].Text != "-")
            //        {
            //            source.Paragraphs[1].Range.Characters[i].Delete();
            //        }
            //        source.Paragraphs[1].Range.Characters[i].Delete();
            //    }
            //    foreach (Paragraph paragraph in source.Paragraphs)
            //    {
            //        question.Context += paragraph.Range.Text;
            //    }

            //}

            //question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            //foreach (var item in questionViewModel.TopicAnswer)
            //{
            //    question.TopicAnswer += item;
            //    question.TopicAnswer += "@";
            //}
            question.TopicAnswer = questionViewModel.TopicAnswer;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.IsHybrid = questionViewModel.IsHybrid;


            //delete topics
            var deleteTopicList = question.Topics
                    .Where(oldTopic => questionViewModel.TopicIds.All(newTopicId => newTopicId != oldTopic.Id))
                    .ToList();
            foreach (var topic in deleteTopicList)
            {
                question.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = questionViewModel.TopicIds
                .Where(oldTopicId => question.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            ////delete tag
            //var deleteTagList = question.Tags
            //    .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
            //    .ToList();
            //foreach (var tag in deleteTagList)
            //{
            //    question.Tags.Remove(tag);
            //}

            ////add tag
            //var addTagList = questionViewModel.TagIds
            //    .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
            //    .ToList();
            //foreach (var tagId in addTagList)
            //{
            //    var tag = new Tag { Id = tagId };
            //    _uow.MarkAsUnChanged(tag);
            //    question.Tags.Add(tag);
            //}

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);

            if (serverResult.MessageType == MessageType.Success)
            {
                _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                {
                    QuestionId = question.Id,
                    UserId = questionViewModel.UserId,
                    DateTime = DateTime.Now,
                    QuestionActivity = QuestionActivity.Topic,
                    Description = JsonConvert.SerializeObject(questionViewModel, Formatting.Indented)
                });
            }
            //if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            //{
            //    if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
            //    {
            //        File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
            //    }

            //    if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
            //    {
            //        File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
            //    }

            //    DeleteOptionsOfQuestion(previousFileName);

            //    word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
            //    while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //    source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
            //    //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //    //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            //    ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

            //    File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");

            //    var target = app.Documents.Add();
            //    SaveOptionsOfQuestions(source, target, questionViewModel.FileName, question.AnswerNumber);

            //    target.Close();
            //    File.Delete(wordFilename);
            //    source.Close();
            //    app.Quit();
            //}
            //else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            //{
            //    //save Doc file in temp memory
            //    word.SaveAs(wordFilename);

            //    // Open a doc file.
            //    app = new Application();
            //    source = app.Documents.Open(wordFilename);

            //    DeleteOptionsOfQuestion(previousFileName);

            //    var target = app.Documents.Add();
            //    SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

            //    target.Close();
            //    File.Delete(wordFilename);
            //    source.Close();
            //    app.Quit();
            //}



            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public ClientMessageResult Delete(int id)
        {
            var question = _questions.Where(x => x.Id == id).First();
            if (question == null)
                return ClientMessageResult.NotFound();
            question.Deleted = true;
            _uow.ValidateOnSaveEnabled(false);
            _uow.MarkAsChanged(question);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);

            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
            //public ClientMessageResult Delete(int id)
            //{
            //    var question = _questions
            //        .Include(current => current.Topics)
            //        .Include(current => current.Tags)
            //        .Include(current => current.QuestionOptions)
            //        .Include(current => current.Supervisors)
            //        .Include(current => current.QuestionGroups)
            //        .Include(current => current.QuestionUpdates)
            //        .First(current => current.Id == id);

            //    if (question == null)
            //        return ClientMessageResult.NotFound();

            //    //remove topics
            //    var topics = question.Topics.ToList();
            //    foreach (var item in topics)
            //    {
            //        question.Topics.Remove(item);
            //    }

            //    //remove tags
            //    var tags = question.Tags.ToList();
            //    foreach (var item in tags)
            //    {
            //        question.Tags.Remove(item);
            //    }

            //    //delete supervisor
            //    foreach (var user in question.Supervisors.ToList())
            //    {
            //        question.Supervisors.Remove(user);
            //    }


            //    //delete questionGroup
            //    foreach (var questiongroup in question.QuestionGroups.ToList())
            //    {
            //        question.QuestionGroups.Remove(questiongroup);
            //    }

            //    //remove options
            //    var options = question.QuestionOptions.ToList();
            //    foreach (var item in options)
            //    {
            //        _uow.MarkAsDeleted(item);
            //    }

            //    //remove questionupdates
            //    var questionUpdates = question.QuestionUpdates.ToList();
            //    foreach (var item in questionUpdates)
            //    {
            //        _uow.MarkAsDeleted(item);
            //    }

            //    _uow.MarkAsDeleted(question);

            //    var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            //    if (msgRes.MessageType == MessageType.Success)
            //    {
            //        if (File.Exists(SitePath.GetQuestionAbsPath(question.FileName) + ".docx"))
            //        {
            //            File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".docx");
            //        }

            //        if (File.Exists(SitePath.GetQuestionAbsPath(question.FileName) + ".png"))
            //        {
            //            File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".png");
            //        }

            //        DeleteOptionsOfQuestion(question.FileName);
            //    }

            //    var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            //    if (clientResult.MessageType == MessageType.Success)
            //        clientResult.Obj = id;
            //    return clientResult;
            //}







            /// <summary>
            /// برگشت تعدادکارشناس سوال
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            /// todo:   check with ali
            public int GetNumberOfjudges(int questionId)
        {

            if (_questionGroupService.Value.IsInQuestionGroup(questionId))
            {
                return _questions
                    .Include(x => x.QuestionGroups.Select(y => y.Lesson))
                    .First(x => x.Id == questionId).QuestionGroups.First().Lesson.NumberOfJudges;


            }
            else
            {
                return _questions
                    .Include(x => x.Topics.Select(y => y.Lesson))
                    .First(x => x.Id == questionId).Topics.First().Lesson.NumberOfJudges;
            }
        }

        public QuestionStatus GetQuestionStatus(int questionId)
        {
            var question = GetById(questionId);

            if (question.Topics.Count == 0)
                return QuestionStatus.Imported;
            else if (question.QuestionJudges.Count == 0)
                return QuestionStatus.Topiced;
            else if (question.QuestionJudges.Count < question.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                return QuestionStatus.JudgedInComplete;
            else if (question.IsActive)
                return QuestionStatus.JudgedActive;
            else
                return QuestionStatus.JudgedInActive;


        }





    }
}
