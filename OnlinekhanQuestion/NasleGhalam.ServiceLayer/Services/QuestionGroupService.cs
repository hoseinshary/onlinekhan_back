using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionGroup;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using NasleGhalam.Common.ForQuestionMaking;
using NasleGhalam.ViewModels.Question;
using Newtonsoft.Json;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionGroupService
    {
        private const string Title = "سوال گروهی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionGroup> _questionGroups;
        private readonly IDbSet<Writer> _writers;
        private readonly Lazy<QuestionUpdateService> _questionUpdateService;

        public QuestionGroupService(IUnitOfWork uow, Lazy<QuestionUpdateService> questionUpdate)
        {
            _uow = uow;
            _questionGroups = uow.Set<QuestionGroup>();
            _writers = uow.Set<Writer>();
            _questionUpdateService = questionUpdate;
        }


        /// <summary>
        /// گرفتن  سوال گروهی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionGroupViewModel GetById(int id)
        {
            return _questionGroups
                .Where(x => x.IsDeleted == false)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه سوال گروهی ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionGroupViewModel> GetAll()
        {
            return _questionGroups
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .ToList();
        }


        /// <summary>
        ///  ثبت سوال گروهی برای برنامه ویندوزی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <param name="word"></param>
        /// <param name="excel"></param>
        /// <returns></returns>
        public ClientMessageResult CreateForWindowsApp(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word, HttpPostedFile excel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);

            _questionGroups.Add(questionGroup);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = questionGroup.Id;

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.File) && !string.IsNullOrEmpty(questionGroupViewModel.File))
            {
                word.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".docx");
                excel.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".xlsx");
            }

            var returnVal = Mapper.Map<ClientMessageResult>(msgRes);
            returnVal.Obj = Mapper.Map<QuestionGroupViewModel>(questionGroup);
            return returnVal;
        }
        //Update Wrong Question's Writer via Excel File (Name Tarah => WriterId)
        public ClientMessageResult UpdateQuestionsWriter()
        {
            var questiongroups = _questionGroups.Where(x => x.Questions.FirstOrDefault().WriterId == 1).Include(x => x.Questions).ToList();

            foreach (var item in questiongroups)
            {
                string excelFilename = SitePath.GetQuestionGroupAbsPath(item.File) + ".xlsx";
                var xlApp = new Microsoft.Office.Interop.Excel.Application();
                var xlWorkbook = xlApp.Workbooks.Open(excelFilename, 0, false, 5, "", "", true, XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                var xlWorksheet = (_Worksheet)xlWorkbook.Sheets[1];
                var xlRange = xlWorksheet.UsedRange;
                var rowCount = xlRange.Rows.Count;
                var colCount = xlRange.Columns.Count;
                var dt = new System.Data.DataTable();
                for (var k = 1; k <= rowCount; k++)
                {
                    var dr = dt.NewRow();
                    for (var j = 1; j <= colCount; j++)
                    {
                        if (k == 1)
                        {
                            dt.Columns.Add(Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range)?.Value2));
                        }
                        else
                        {
                            dr[j - 1] = Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range)?.Value2);
                        }

                    }
                    if (k != 1)
                        dt.Rows.Add(dr);
                }

                

                int count = 0;
                foreach(var question in item.Questions.OrderBy(x=>x.Id))
                {
                    try
                    {
                        if (dt.Rows[count]["شماره طراح"] == DBNull.Value && dt.Rows[count]["نام طراح"] != DBNull.Value)
                        {
                            string tarah = dt.Rows[count]["نام طراح"].ToString();
                            var writer = _writers.Where(x => x.Name == tarah).First();
                            if (writer != null)
                            {
                                question.WriterId = writer.Id;
                                _uow.ExcludeFieldsFromUpdate(question, x => x.Context);
                                _uow.MarkAsChanged(question);
                                xlWorksheet.Cells[count + 2, 16] = writer.Id.ToString();
                                xlApp.Application.ActiveWorkbook.Save();
                            }
                        }
                    }
                    catch { }
                    count++;
                }
                xlWorkbook.Close();
                xlApp.Quit();
                
            }


            _uow.ValidateOnSaveEnabled(false);
            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            var returnVal = Mapper.Map<ClientMessageResult>(msgRes);
            return returnVal;
        }

        /// <summary>
        /// ثبت سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <param name="word"></param>
        /// <param name="excel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word, HttpPostedFile excel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);

            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".docx";
            var excelFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".xlsx";

            //save Doc and excel file in temp memory
            word.SaveAs(wordFilename);
            excel.SaveAs(excelFilename);

            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
           

            var source = app.Documents.Open(wordFilename);

            var missing = Type.Missing;

            //read from excel file
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(excelFilename, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorksheet = (_Worksheet)xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var rowCount = xlRange.Rows.Count;
            var colCount = xlRange.Columns.Count;
            var dt = new System.Data.DataTable();
            for (var k = 1; k <= rowCount; k++)
            {
                var dr = dt.NewRow();
                for (var j = 1; j <= colCount; j++)
                {
                    if (k == 1)
                    {
                        dt.Columns.Add(Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range)?.Value2));
                    }
                    else
                    {
                        dr[j - 1] = Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range)?.Value2);
                    }

                }
                if (k != 1)
                    dt.Rows.Add(dr);
            }

            xlWorkbook.Close();
            xlApp.Quit();

            //split question group
            var x = source.Paragraphs.Count;
            var i = 1;
            var numberOfQ = 0;
            while (i <= x)
            {
                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[i].Range.Text))
                {
                    var context = "";

                    numberOfQ++;
                    var target = app.Documents.Add();

                    //تریک درست شدن گزینه ها 
                    source.ActiveWindow.Selection.WholeStory();
                    source.ActiveWindow.Selection.Copy();
                    target.ActiveWindow.Selection.Paste();
                    target.ActiveWindow.Selection.WholeStory();
                    target.ActiveWindow.Selection.Delete();

                    //حذف عدد اول سوال
                    int k = 1;
                    while (k < source.Paragraphs[i].Range.Characters.Count &&
                           source.Paragraphs[i].Range.Characters[k].Text != "-")
                    {
                        source.Paragraphs[i].Range.Characters[k].Delete();
                    }
                    source.Paragraphs[i].Range.Characters[k].Delete();


                    int startOfQuestionIndex = source.Paragraphs[i].Range.Sentences.Parent.Start;

                    context += source.Paragraphs[i].Range.Text;
                    i++;
                    while (i <= x && !QuestionMaking.IsQuestionParagraph(source.Paragraphs[i].Range.Text))
                    {
                        context += source.Paragraphs[i].Range.Text;
                        i++;
                    }

                    int endOfQuestionIndex = source.Paragraphs[i - 1].Range.Sentences.Parent.End;

                    source.Range(startOfQuestionIndex, endOfQuestionIndex).Select();
                    source.ActiveWindow.Selection.Copy();

                    target.ActiveWindow.Selection.Paste();

                    //create single question
                    var newQuestion = new Question();
                    var newGuid = Guid.NewGuid();
                    newQuestion.FileName = newGuid.ToString();
                    newQuestion.Context = context;
                    newQuestion.LookupId_QuestionType = dt.Rows[numberOfQ - 1]["نوع سوال"].ToString() == "تشریحی" ? 7 : 6;
                    newQuestion.QuestionPoint = Convert.ToInt32(dt.Rows[numberOfQ - 1]["بارم سوال"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["بارم سوال"] : 0);
                    newQuestion.AnswerNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["گزینه صحیح"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["گزینه صحیح"] : 0);
                    newQuestion.LookupId_QuestionHardnessType = 1040;
                    //newQuestion.LookupId_AreaType = 1036;
                    newQuestion.LookupId_AuthorType = 1039;
                    newQuestion.LookupId_RepeatnessType = 21;
                    newQuestion.LookupId_QuestionRank = 1063;
                    newQuestion.InsertDateTime = DateTime.Now;
                    //newQuestion.Istandard = dt.Rows[numberOfQ - 1]["درجه استاندارد"].ToString() == "استاندارد";
                    newQuestion.WriterId = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره طراح"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره طراح"] : 1);

                    //newQuestion.Description = dt.Rows[numberOfQ - 1]["توضیحات"].ToString();
                    newQuestion.IsActive = false;
                    newQuestion.ResponseSecond = Convert.ToInt16(dt.Rows[numberOfQ - 1]["زمان سوال"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["زمان سوال"] : 0);
                    newQuestion.UseEvaluation = false;
                    newQuestion.QuestionNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"] : 0);

                    //add supervisor
                    var supervisor = new User() { Id = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره ناظر"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره ناظر"] : 0) };
                    if (supervisor.Id != 0)
                    {
                        _uow.MarkAsUnChanged(supervisor);
                        newQuestion.Supervisors.Add(supervisor);
                    }

                    questionGroup.Questions.Add(newQuestion);

                    var filename2 = SitePath.GetQuestionAbsPath(newGuid.ToString()) + ".docx";
                    var filename3 = SitePath.GetQuestionAbsPath(newGuid.ToString());
                    target.SaveAs(filename2);
                    target.SaveAs(filename3, WdSaveFormat.wdFormatPDF);

                    //while (target.Windows[1].Panes[1].Pages.Count < 0) ;
                    //var bits = target.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                    ImageUtility.SaveImageOfWordPdf(filename3 + ".pdf", filename3);

                    File.Delete(filename3 + ".pdf");

                    //if (newQuestion.AnswerNumber != 0 )
                    //{
                    //    //چرخش گزینه
                    //    Document target2 = app.Documents.Add();
                    //    //تریک درست شدن گزینه ها 
                    //    target.ActiveWindow.Selection.WholeStory();
                    //    target.ActiveWindow.Selection.Copy();
                    //    target2.ActiveWindow.Selection.Paste();
                    //    target2.ActiveWindow.Selection.WholeStory();
                    //    target2.ActiveWindow.Selection.Delete();

                    //    target2.ActiveWindow.Selection.Paste();

                    //    QuestionService.SaveOptionsOfQuestions(target, target2,newQuestion.FileName,newQuestion.AnswerNumber);
                    //    target2.Close();
                    //}
                    target.Close();
                }
                else
                {
                    i++;
                }
            }


            source.Close();
            app.Quit();

            File.Delete(wordFilename);
            File.Delete(excelFilename);
            /////////////////////////////////

            _questionGroups.Add(questionGroup);
            _uow.ValidateOnSaveEnabled(false);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = questionGroup.Id;

            if (msgRes.MessageType == MessageType.Success)
            {

                foreach (var item in questionGroup.Questions)
                {
                    _questionUpdateService.Value.Create(new ViewModels.QuestionUpdate.QuestionUpdateViewModel
                    {
                        QuestionId = item.Id,
                        UserId = questionGroupViewModel.UserId,
                        DateTime = DateTime.Now,
                        QuestionActivity = QuestionActivity.Import,
                        Description = JsonConvert.SerializeObject(Mapper.Map<QuestionViewModel>(item), Formatting.Indented)
                    });
                }

            }


            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.File) && !string.IsNullOrEmpty(questionGroupViewModel.File))
            {
                word.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".docx");
                excel.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".xlsx");
            }

            var returnVal = Mapper.Map<ClientMessageResult>(msgRes);
            returnVal.Obj = Mapper.Map<QuestionGroupViewModel>(questionGroup);
            return returnVal;
        }

        public ClientMessageResult PreCreate(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word)
        {
            var returnGuidList = new List<string>();
            var missing = Type.Missing;
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File);
            //save Doc and excel file in temp memory
            word.SaveAs(wordFilename);
            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
            //app.Visible = true;
            var source = app.Documents.Open(wordFilename, Visible: true);


            //split question group
            var x = source.Paragraphs.Count;
            var i = 1;
            while (i <= x)
            {

                if (QuestionMaking.IsQuestionParagraph(source.Paragraphs[i].Range.Text))
                {
                    var target = app.Documents.Add(Visible: true);
                    //تریک درست شدن گزینه ها 
                    source.ActiveWindow.Selection.WholeStory();
                    source.ActiveWindow.Selection.Copy();
                    target.ActiveWindow.Selection.Paste();
                    //target.ActiveWindow.Selection.PasteSpecial(Microsoft.Office.Interop.Word.WdPasteOptions.wdKeepTextOnly);
                    target.ActiveWindow.Selection.WholeStory();
                    target.ActiveWindow.Selection.Delete();

                    int startOfQuestionIndex = source.Paragraphs[i].Range.Sentences.Parent.Start;

                    i++;

                    while (i <= x && !QuestionMaking.IsQuestionParagraph(source.Paragraphs[i].Range.Text))
                    {
                        i++;
                    }

                    int endOfQuestionIndex = source.Paragraphs[i - 1].Range.Sentences.Parent.End;

                    source.Range(startOfQuestionIndex, endOfQuestionIndex).Select();
                    source.ActiveWindow.Selection.Copy();
                    target.ActiveWindow.Selection.Paste();
                    //target.ActiveWindow.Selection.WholeStory();
                    //target.ActiveWindow.Selection.Paragraphs.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
                    //target.ActiveWindow.Selection.Paste();

                    var newGuid = Guid.NewGuid();
                    var newEntry = $"/content/questionGroupTemp/{newGuid}.png".ToFullRelativePath();
                    returnGuidList.Add(newEntry);
                    var filename2 = SitePath.GetQuestionGroupTempAbsPath(newGuid.ToString());

                    target.SaveAs(filename2 + ".pdf", WdSaveFormat.wdFormatPDF);

                    //while (target.Windows[1].Panes[1].Pages.Count < 0) ;

                    //var bits = target.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                    ImageUtility.SaveImageOfWordPdf(filename2 + ".pdf", filename2);
                    target.Close(WdSaveOptions.wdDoNotSaveChanges);

                    File.Delete(filename2 + ".pdf");
                }
                else
                {
                    i++;
                }


            }

            source.Close();
            app.Quit();

            File.Delete(wordFilename);
            /////////////////////////////////

            var msgRes = new ClientMessageResult { MessageType = MessageType.Success, Obj = returnGuidList };
            return msgRes;
        }


        /// <summary>
        /// ویرایش سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            var questionGroup = _questionGroups
                .FirstOrDefault(current => current.Id == questionGroupViewModel.Id);

            if (questionGroup == null)
                return ClientMessageResult.NotFound();

            if (!string.IsNullOrEmpty(questionGroupViewModel.Title))
                questionGroup.Title = questionGroupViewModel.Title;

            if (questionGroupViewModel.LessonId != 0)
                questionGroup.LessonId = questionGroupViewModel.LessonId;

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }


        /// <summary>
        /// حذف سوال گروهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public ClientMessageResult Delete(int id)
        {
            var questionGroup = _questionGroups
                .Include(current => current.Questions)
                .Where(current => current.Id == id).First();

            if (questionGroup == null)
                return ClientMessageResult.NotFound();

            //remove questions relation
            var questions = questionGroup.Questions.ToList();
            foreach (var item in questions)
            {
                item.Deleted = true;
                _uow.MarkAsChanged(item);
            }

            questionGroup.IsDeleted = true;
            _uow.MarkAsChanged(questionGroup);
            _uow.ValidateOnSaveEnabled(false);
            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);

            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;

        }
        //public ClientMessageResult Delete(int id)
        //{
        //    var questionGroup = _questionGroups
        //        .Include(current => current.Questions)
        //        .Include(current => current.Questions.Select(x => x.QuestionAnswers))
        //        .Include(current => current.Questions.Select(x => x.Supervisors))
        //        .Include(current => current.Questions.Select(x=>x.QuestionUpdates))
        //        .First(current => current.Id == id);

        //    if (questionGroup == null)
        //        return ClientMessageResult.NotFound();

        //    //remove questions relation
        //    var questions = questionGroup.Questions.ToList();
        //    var questionAnswers = questionGroup.Questions.Select(x => x.QuestionAnswers.ToList()).ToList();
        //    var supervisors = questionGroup.Questions.Select(x => x.Supervisors.ToList()).ToList();
        //    var questionUpdates = questionGroup.Questions.Select(x => x.QuestionUpdates.ToList()).ToList();


        //    int i = 0;
        //    foreach (var item in questions)
        //    {
        //        foreach (var answer in questionAnswers[i])
        //        {
        //            questionGroup.Questions.Where(x => x.Id == item.Id).First().QuestionAnswers.Remove(answer);
        //            _uow.MarkAsDeleted(answer);
        //        }

        //        foreach (var update in questionUpdates[i])
        //        {
        //            questionGroup.Questions.Where(x => x.Id == item.Id).First().QuestionUpdates.Remove(update);
        //            _uow.MarkAsDeleted(update);
        //        }

        //        foreach (var supervisor in supervisors[i])
        //        {
        //            questionGroup.Questions.Where(x => x.Id == item.Id).First().Supervisors.Remove(supervisor);
        //            _uow.MarkAsDeleted(supervisor);
        //        }

        //        questionGroup.Questions.Remove(item);
        //        _uow.MarkAsDeleted(item);
        //        if (item.AnswerNumber != 0)
        //        {
        //            QuestionService.DeleteOptionsOfQuestion(item.FileName);
        //        }


        //        i++;
        //    }



        //    _uow.MarkAsDeleted(questionGroup);

        //    var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
        //    if (msgRes.MessageType == MessageType.Success)
        //    {
        //        //remove questions file
        //        int j = 0;
        //        foreach (var item in questions)
        //        {
        //            File.Delete(SitePath.GetQuestionAbsPath(item.FileName) + ".docx");
        //            File.Delete(SitePath.GetQuestionAbsPath(item.FileName) + ".png");

        //            foreach (var answer in questionAnswers[j])
        //            {
        //                if (File.Exists(SitePath.GetQuestionAnswerAbsPath(answer.FilePath) + ".docx"))
        //                {
        //                    File.Delete(SitePath.GetQuestionAnswerAbsPath(answer.FilePath) + ".docx");
        //                }

        //                if (File.Exists(SitePath.GetQuestionAnswerAbsPath(answer.FilePath) + ".png"))
        //                {
        //                    File.Delete(SitePath.GetQuestionAnswerAbsPath(answer.FilePath) + ".png");
        //                }
        //            }
        //            j++;
        //        }




        //        File.Delete(SitePath.GetQuestionGroupAbsPath(questionGroup.File) + ".docx");
        //        File.Delete(SitePath.GetQuestionGroupAbsPath(questionGroup.File) + ".xlsx");
        //    }

        //    var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
        //    if (clientResult.MessageType == MessageType.Success)
        //        clientResult.Obj = id;
        //    return clientResult;
        //}





        /// <summary>
        /// وجود سوالی در سوال گروهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsInQuestionGroup(int questionId)
        {
            return _questionGroups.Any(x => x.Questions.Any(y => y.Id == questionId));

        }



    }
}