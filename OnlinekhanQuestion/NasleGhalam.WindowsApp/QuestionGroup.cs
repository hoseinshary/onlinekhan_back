using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ViewModels.Writer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NasleGhalam.Common.ForQuestionMaking;

namespace NasleGhalam.WindowsApp
{
    public partial class QuestionGroup : Form
    {
        private readonly string FilePath = System.Windows.Forms.Application.StartupPath + "\\content\\";



        private readonly LessonService _lessonService;
        private readonly EducationTreeService _educationTreeService;
        private readonly WriterService _writerService;
        private readonly WebService _webService;

        private List<LessonViewModel> lessons;
        private List<EducationTreeViewModel> educationTrees;
        private List<WriterViewModel> writers;

        private List<string> questionsFileNames;
        private List<string> answersFileNames;
        private List<int> questionIds;


        public QuestionGroup(LessonService lessonService, EducationTreeService educationTreeService, WebService WebService, WriterService writerService)
        {
            _lessonService = lessonService;
            _educationTreeService = educationTreeService;
            _webService = WebService;
            _writerService = writerService;

            questionIds = new List<int>();
            InitializeComponent();

        }



        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog2.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox_excel.Text = openFileDialog2.FileName;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox_word.Text = openFileDialog1.FileName;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void QuestionGroup_Load(object sender, EventArgs e)
        {
            lessons = _lessonService.GetAll().ToList();
            educationTrees = _educationTreeService.GetAll().ToList();

            PopulateTreeView();


            var bindingSource1 = new BindingSource();
            writers = _writerService.GetAll().ToList();
            bindingSource1.DataSource = writers;

            comboBox_writer.DataSource = bindingSource1.DataSource;

            comboBox_writer.DisplayMember = "Name";
            comboBox_writer.ValueMember = "Id";

        }

        private void PopulateTreeView()
        {
            var tempEducationTree = educationTrees.Where(x => x.ParentEducationTreeId == null).FirstOrDefault();
            TreeNode treeNode = makeTree(tempEducationTree);
            treeView1.Nodes.Add(treeNode);

        }

        private TreeNode makeTree(EducationTreeViewModel node)
        {
            var childList = educationTrees.Where(x => x.ParentEducationTreeId == node.Id);

            if (childList == null)
            {
                return new TreeNode(node.Name + "(" + node.Lookup_EducationTreeState.Value + ")" + "(" + node.Id + ")");
            }
            else
            {
                List<TreeNode> treeNodes = new List<TreeNode>();
                foreach (var child in childList)
                {
                    treeNodes.Add(makeTree(child));
                }
                return new TreeNode(node.Name + "(" + node.Lookup_EducationTreeState.Value + ")" + "(" + node.Id + ")", treeNodes.ToArray());

            }

        }
        List<LessonViewModel> lessonsList;
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<int> educationTreeIds = new List<int>();
            educationTreeIds.Add(Convert.ToInt32(e.Node.Text.Split('(')[2].Replace(')', '\0')));
            foreach (TreeNode node in e.Node.Nodes)
            {
                educationTreeIds.Add(Convert.ToInt32(node.Text.Split('(')[2].Replace(')', '\0')));
            }

             lessonsList = lessons.Where(x => x.EducationTrees.Any(y => educationTreeIds.Contains(y.Id))).ToList();

            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = lessonsList;

            comboBox1.DataSource = bindingSource1.DataSource;

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";

        }

        private void button3_Click(object sender, EventArgs e)
        {

            progressBar3.Maximum = 100;
            progressBar3.Step = 1;
            progressBar3.Value = 0;
            backgroundWorker3.WorkerReportsProgress = true;
            backgroundWorker3.RunWorkerAsync();




        }





        private  void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private async void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate () { richTextBox1.Text = "شروع فرایند ورود سوالات...\n"; }));
                string title = "";
                this.Invoke(new MethodInvoker(delegate () { title = textBox_title.Text; }));
                string wordText = "";
                this.Invoke(new MethodInvoker(delegate () { wordText = textBox_word.Text; }));
                string excelText = "";
                this.Invoke(new MethodInvoker(delegate () { excelText = textBox_excel.Text; }));
                string comboboxText = "";
                this.Invoke(new MethodInvoker(delegate () { comboboxText = comboBox1.Text; }));
                object comboboxValue = null;
                this.Invoke(new MethodInvoker(delegate () { comboboxValue = comboBox1.SelectedValue; }));

                if (title != "" && wordText != "" && excelText != "" && comboboxText != "")
                {

                    var questionGroup = new QuestionGroupCreateViewModel();
                    questionGroup.Title = title;
                    questionGroup.LessonId = Convert.ToInt32(comboboxValue);
                    questionGroup.QuestionGroupWordPath = wordText;
                    questionGroup.QuestionGroupExcelPath = excelText;

                    var result = await _webService.QuestionGrounCreate(questionGroup);

                    if (result.MessageType != MessageType.Success)
                    {
                        MessageBox.Show(result.Message);

                    }
                    else
                    {
                        this.Invoke(new MethodInvoker(delegate () { richTextBox1.Text += "شروع فرایند ثبت سوالات تک تک ...\n"; }));
                        var missing = Type.Missing;

                        var sourceExecleFilename = excelText;
                        var destExecleFilename = FilePath + Guid.NewGuid() + ".xlsx";
                        File.Copy(sourceExecleFilename, destExecleFilename);
                        //read from excel file
                        var xlApp = new Microsoft.Office.Interop.Excel.Application();
                        var xlWorkbook = xlApp.Workbooks.Open(destExecleFilename, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
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
                                var sTemp = xlRange.Cells[k, j].Value2;
                                if (sTemp != null)
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

                            }
                            if (k != 1)
                                dt.Rows.Add(dr);
                        }

                        xlWorkbook.Close();
                        xlApp.Quit();


                        File.Delete(destExecleFilename);
                        this.Invoke(new MethodInvoker(delegate () { richTextBox1.Text += "فایل اکسل خوانده شد...\n"; }));


                        // Open a doc file.
                        var app = new Microsoft.Office.Interop.Word.Application();

                        var numberOfQ = 0;
                        foreach (var questionName in questionsFileNames)
                        {
                            var newQuestionNameFile = Guid.NewGuid();
                            numberOfQ++;
                            var source = app.Documents.Open(questionName + ".docx");

                            QuestionCreateWindowsViewModel question = new QuestionCreateWindowsViewModel();

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

                            string filename2 = FilePath + "questionGroupTemp//" + newQuestionNameFile;
                            source.SaveAs(filename2 + ".pdf", WdSaveFormat.wdFormatPDF);
                            source.SaveAs(filename2 + ".docx");
                            ImageTools.SaveImageOfWordPdf(filename2 + ".pdf", filename2);
                            source.Close(WdSaveOptions.wdDoNotSaveChanges);

                            File.Delete(filename2 + ".pdf");

                            question.FilePath = FilePath + "questionGroupTemp//" + newQuestionNameFile;

                            question.LookupId_QuestionType = dt.Rows[numberOfQ - 1]["نوع سوال"].ToString() == "تشریحی" ? 7 : 6;
                            question.QuestionPoint = Convert.ToInt32(dt.Rows[numberOfQ - 1]["بارم سوال"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["بارم سوال"] : 0);
                            question.AnswerNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["گزینه صحیح"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["گزینه صحیح"] : 0);
                            question.LookupId_QuestionHardnessType = 1040;
                            //newQuestion.LookupId_AreaType = 1036;
                            question.LookupId_AuthorType = 1039;
                            question.LookupId_RepeatnessType = 21;
                            question.LookupId_QuestionRank = 1063;
                            //newQuestion.Istandard = dt.Rows[numberOfQ - 1]["درجه استاندارد"].ToString() == "استاندارد";
                            question.WriterId = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره طراح"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره طراح"] : 1);
                            //newQuestion.Description = dt.Rows[numberOfQ - 1]["توضیحات"].ToString();
                            question.IsActive = false;
                            question.ResponseSecond = Convert.ToInt16(dt.Rows[numberOfQ - 1]["زمان سوال"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["زمان سوال"] : 0);
                            question.UseEvaluation = false;
                            question.QuestionNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"] : 0);
                            question.SupervisorUserId = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره ناظر"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره ناظر"] : 0);

                            question.QuestionGroupId = result.Id;

                            Byte[] bytes = File.ReadAllBytes(filename2 + ".docx");
                            String file = Convert.ToBase64String(bytes);

                            Byte[] bytes2 = File.ReadAllBytes(filename2 + ".png");
                            String file2 = Convert.ToBase64String(bytes2);



                            question.WordBase64File = file;
                            question.PngBase64File = file2;
                            var result2 = await _webService.QuestionCreate(question);

                            if (result2.MessageType != MessageType.Success)
                            {
                                MessageBox.Show(" مشکل در ثبت سوال : \n" + result2.Message);
                                app.Quit();
                                break;
                            }


                            this.Invoke(new MethodInvoker(delegate () { richTextBox1.Text += $"سوال {numberOfQ} با موفقیت وارد شد...\n"; }));
                            backgroundWorker1.ReportProgress((numberOfQ * 100) / questionsFileNames.Count);
                            questionIds.Add(result2.obj.Id);



                        }
                        app.Quit();

                        this.Invoke(new MethodInvoker(delegate () { richTextBox1.Text += "تمام سوالات با موفقیت وارد شده اند!\n"; }));

                        this.Invoke(new MethodInvoker(delegate () { label5.Text = "فایل با موفقیت وارد شد !"; }));


                    }
                }
                else
                {
                    MessageBox.Show("مقادیر ورودی به صورت کامل وارد نشده اند!");
                }
            }
            catch (Exception error)
            {
                LogWriter.LogException(error);
                MessageBox.Show(error.Message);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBar1.Minimum && e.ProgressPercentage <= progressBar1.Maximum)
                progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label5.ForeColor = Color.Green;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox_wordfileAnswers.Text = openFileDialog1.FileName;
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = writers.Where(x => x.Name.StartsWith(comboBox_writer.Text)).ToList();
            if (bindingSource1.Count != 0)
                comboBox_writer.DataSource = bindingSource1.DataSource;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            progressBar4.Maximum = 100;
            progressBar4.Step = 1;
            progressBar4.Value = 0;
            backgroundWorker4.WorkerReportsProgress = true;
            backgroundWorker4.RunWorkerAsync();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
            progressBar2.Maximum = 100;
            progressBar2.Step = 1;
            progressBar2.Value = 0;
            backgroundWorker2.WorkerReportsProgress = true;
            backgroundWorker2.RunWorkerAsync();
        }

        private async void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                this.Invoke(new MethodInvoker(delegate () { richTextBox2.Text = "شروع فرایند ورود جواب ها...\n"; }));
                string title = "";
                this.Invoke(new MethodInvoker(delegate () { title = textBox_answerTitle.Text; }));
                string wordText = "";
                this.Invoke(new MethodInvoker(delegate () { wordText = textBox_wordfileAnswers.Text; }));

                string comboboxText = "";
                this.Invoke(new MethodInvoker(delegate () { comboboxText = comboBox_writer.Text; }));
                object comboboxValue = null;
                this.Invoke(new MethodInvoker(delegate () { comboboxValue = comboBox_writer.SelectedValue; }));

                if (title != "" && wordText != "" && comboboxText != "")
                {
                    var missing = Type.Missing;

                    // Open a doc file.
                    var app = new Microsoft.Office.Interop.Word.Application();

                    var numberOfQ = 0;
                    foreach (var questionName in answersFileNames)
                    {
                        var newQuestionNameFile = Guid.NewGuid();
                        numberOfQ++;
                        var source = app.Documents.Open(questionName + ".docx");

                        QuestionAnswerCreateViewModel question = new QuestionAnswerCreateViewModel();

                        //حذف عدد اول سوال
                        if (QuestionMaking.IsAnswerParagraph(source.Paragraphs[1].Range.Text))
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

                        string filename2 = FilePath + "questionGroupTemp//" + newQuestionNameFile;
                        source.SaveAs(filename2 + ".pdf", WdSaveFormat.wdFormatPDF);
                        source.SaveAs(filename2 + ".docx");
                        ImageTools.SaveImageOfWordPdf(filename2 + ".pdf", filename2);
                        source.Close(WdSaveOptions.wdDoNotSaveChanges);

                        File.Delete(filename2 + ".pdf");

                        question.FilePath = newQuestionNameFile.ToString();

                        question.IsActive = false;
                        question.IsMaster = true;
                        question.Title = title;
                        question.WriterId = Convert.ToInt32(comboboxValue);
                        question.QuestionId = questionIds[numberOfQ - 1];

                        Byte[] bytes = File.ReadAllBytes(filename2 + ".docx");
                        String file = Convert.ToBase64String(bytes);

                        Byte[] bytes2 = File.ReadAllBytes(filename2 + ".png");
                        String file2 = Convert.ToBase64String(bytes2);


                        question.WordBase64File = file;
                        question.PngBase64File = file2;

                        var result2 = await _webService.QuestionAnswerCreate(question);

                        if (result2.MessageType != MessageType.Success)
                        {
                            MessageBox.Show(" مشکل در ثبت جواب : \n" + result2.Message);
                            app.Quit();
                            break;
                        }


                        this.Invoke(new MethodInvoker(delegate () { richTextBox2.Text += $"سوال {numberOfQ} با موفقیت وارد شد...\n"; }));
                        backgroundWorker2.ReportProgress((numberOfQ * 100) / questionsFileNames.Count);




                    }
                    app.Quit();

                    this.Invoke(new MethodInvoker(delegate () { richTextBox2.Text += "تمام سوالات با موفقیت وارد شده اند!\n"; }));

                    this.Invoke(new MethodInvoker(delegate () { label9.Text = "فایل با موفقیت وارد شد !"; }));


                }

                else
                {
                    MessageBox.Show("مقادیر ورودی به صورت کامل وارد نشده اند!");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBar2.Minimum && e.ProgressPercentage <= progressBar2.Maximum)
                progressBar2.Value = e.ProgressPercentage;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label9.ForeColor = Color.Green;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            var returnGuidList = new List<string>();
            var missing = Type.Missing;
            var sourceWordFilename = textBox_word.Text;
            var destWordFilename = FilePath + Guid.NewGuid() + ".docx";

            //save Doc and excel file in temp memory
            File.Copy(sourceWordFilename, destWordFilename);

            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
          //  app.Visible = true;
            var source = app.Documents.Open(destWordFilename);


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
                    var newEntry = FilePath + $"/questionGroupTemp/{newGuid}";
                    returnGuidList.Add(newEntry);
                    var filename2 = FilePath + $"/questionGroupTemp/{newGuid}";

                    target.SaveAs(filename2 + ".pdf", WdSaveFormat.wdFormatPDF);
                    target.SaveAs(filename2 + ".docx");

                    //while (target.Windows[1].Panes[1].Pages.Count < 0) ;

                    //var bits = target.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                    ImageTools.SaveImageOfWordPdf(filename2 + ".pdf", filename2);
                    target.Close(WdSaveOptions.wdDoNotSaveChanges);

                    File.Delete(filename2 + ".pdf");

                    backgroundWorker3.ReportProgress(returnGuidList.Count * 100 / Convert.ToInt32(textBox_numberOfQuestions.Text));
                }
                else
                {
                    i++;
                }


            }

            source.Close();
            app.Quit();

            File.Delete(destWordFilename);
            /////////////////////////////////

            var msgRes = new ClientMessageResult { MessageType = MessageType.Success };
            if (msgRes.MessageType != MessageType.Success)
            {
                MessageBox.Show(msgRes.Message);
            }
            else
            {
                questionsFileNames = returnGuidList;
                this.Invoke(new MethodInvoker(delegate () { tabControl1.SelectTab(1); }));


                foreach (Control item in panel1.Controls)
                {
                    this.Invoke(new MethodInvoker(delegate () { panel1.Controls.Remove(item); }));

                }

                var height = 0;
                foreach (var item in returnGuidList)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(item + ".png");
                    pb.Size = new Size(pb.Image.Width, pb.Image.Height);

                    pb.Top = height + 30;
                    pb.Left = 50 + 870 - pb.Image.Width;
                    height += pb.Height;
                    pb.BorderStyle = BorderStyle.FixedSingle;

                    this.Invoke(new MethodInvoker(delegate () { panel1.Controls.Add(pb); }));
                    System.Threading.Tasks.Task.Delay(1000);
                }

            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBar3.Minimum && e.ProgressPercentage <= progressBar3.Maximum)
                progressBar3.Value = e.ProgressPercentage;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {

            string title = "";
            this.Invoke(new MethodInvoker(delegate () { title = textBox_title.Text; }));
            string wordText = "";
            this.Invoke(new MethodInvoker(delegate () { wordText = textBox_wordfileAnswers.Text; }));



            var returnGuidList = new List<string>();
            var missing = Type.Missing;
            var sourceWordFilename = wordText;
            var destWordFilename = FilePath + Guid.NewGuid() + ".docx";

            //save Doc and excel file in temp memory
            File.Copy(sourceWordFilename, destWordFilename);

            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
            var source = app.Documents.Open(destWordFilename);


            //بررسی تعداد سوالات با تعداد جواب ها 
            var questoinAnswerCount = 0;
            foreach (Paragraph paragraph in source.Paragraphs)
            {
                if (QuestionMaking.IsAnswerParagraph(paragraph.Range.Text))
                {
                    questoinAnswerCount++;
                }
            }

            if (questoinAnswerCount != questionsFileNames.Count)
            {
                MessageBox.Show($"تعداد سوالات با تعداد جواب سوالات برابر نیست!\nتعداد سوالات{questionsFileNames.Count} است.");
                return;
            }


            //split question Answer
            var x = source.Paragraphs.Count;
            var i = 1;
            var numberOfQ = 0;
            while (i <= x)
            {
                if (QuestionMaking.IsAnswerParagraph(source.Paragraphs[i].Range.Text))
                {
                    numberOfQ++;

                    var target = app.Documents.Add();
                    //تریک درست شدن گزینه ها 
                    source.ActiveWindow.Selection.WholeStory();
                    source.ActiveWindow.Selection.Copy();
                    target.ActiveWindow.Selection.Paste();
                    target.ActiveWindow.Selection.WholeStory();
                    target.ActiveWindow.Selection.Delete();

                    int startOfQuestionIndex = source.Paragraphs[i].Range.Sentences.Parent.Start;

                    i++;
                    while (i <= x && !QuestionMaking.IsAnswerParagraph(source.Paragraphs[i].Range.Text))
                    {
                        i++;
                    }

                    int endOfQuestionIndex = source.Paragraphs[i - 1].Range.Sentences.Parent.End;

                    source.Range(startOfQuestionIndex, endOfQuestionIndex).Select();
                    source.ActiveWindow.Selection.Copy();
                    target.ActiveWindow.Selection.Paste();

                    var newGuid = Guid.NewGuid();
                    var newEntry = FilePath + $"/questionGroupTemp/{newGuid}";
                    returnGuidList.Add(newEntry);
                    var filename2 = FilePath + $"/questionGroupTemp/{newGuid}";

                    target.SaveAs(filename2 + ".pdf", WdSaveFormat.wdFormatPDF);
                    target.SaveAs(filename2 + ".docx");

                    //while (target.Windows[1].Panes[1].Pages.Count < 0) ;

                    //var bits = target.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                    ImageTools.SaveImageOfWordPdf(filename2 + ".pdf", filename2);
                    target.Close(WdSaveOptions.wdDoNotSaveChanges);

                    File.Delete(filename2 + ".pdf");

                    backgroundWorker4.ReportProgress((numberOfQ * 100) / questionsFileNames.Count);
                }
                else
                {
                    i++;
                }


            }

            source.Close();
            app.Quit();
            /////////////////////////////////
            File.Delete(destWordFilename);

            var msgRes = new ClientMessageResult { MessageType = MessageType.Success };
            if (msgRes.MessageType != MessageType.Success)
            {
                MessageBox.Show(msgRes.Message);
            }
            else
            {
                answersFileNames = returnGuidList;

                this.Invoke(new MethodInvoker(delegate () { tabControl1.SelectTab(4); }));



                

                foreach (Control item in panel2.Controls)
                {
                    
                    this.Invoke(new MethodInvoker(delegate () { panel2.Controls.Remove(item); }));
                }

                var height = 0;
                foreach (var item in returnGuidList)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Image.FromFile(item + ".png");
                    pb.Size = new Size(pb.Image.Width, pb.Image.Height);

                    pb.Top = height + 30;
                    pb.Left = 50 + 870 - pb.Image.Width;
                    height += pb.Height;
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    
                    this.Invoke(new MethodInvoker(delegate () { panel2.Controls.Add(pb); }));
                    System.Threading.Tasks.Task.Delay(1000);
                }

            }

        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= progressBar4.Minimum && e.ProgressPercentage <= progressBar4.Maximum)
                progressBar4.Value = e.ProgressPercentage;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                var bindingSource1 = new BindingSource();
                bindingSource1.DataSource = lessonsList.Where(x => x.Name.StartsWith(comboBox1.Text)).ToList();
                if (bindingSource1.Count != 0)
                    comboBox1.DataSource = bindingSource1.DataSource;
            }
        }
    }
}
