using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.Common.ForQuestionMaking;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.Lesson;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.ViewModels.Writer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NasleGhalam.WindowsApp
{
    public partial class QuestionAnswerGroup : Form
    {
        private readonly string FilePath = System.Windows.Forms.Application.StartupPath + "\\content\\";

        private readonly LessonService _lessonService;
        private readonly EducationTreeService _educationTreeService;
        private readonly WriterService _writerService;
        private readonly WebService _webService;
        private readonly QuestionGroupService _questionService;
        private readonly QuestionService _questionsService;
        private List<LessonViewModel> lessons;
        private List<EducationTreeViewModel> educationTrees;
        private List<WriterViewModel> writers;

        private List<string> answersFileNames;
        private List<int> questionIds;
        public QuestionAnswerGroup(LessonService lessonService, EducationTreeService educationTreeService, WebService WebService, WriterService writerService,QuestionGroupService questionService,QuestionService questionsService)
        {
            _lessonService = lessonService;
            _educationTreeService = educationTreeService;
            _webService = WebService;
            _writerService = writerService;
            _questionService = questionService;
            _questionsService = questionsService;
            questionIds = new List<int>();
            InitializeComponent();

        }

        private void QuestionAnswerGroup_Load(object sender, EventArgs e)
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

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var bindingSource1 = new BindingSource();
                bindingSource1.DataSource = lessonsList.Where(x => x.Name.StartsWith(comboBox1.Text)).ToList();
                if (bindingSource1.Count != 0)
                    comboBox1.DataSource = bindingSource1.DataSource;
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null || comboBox1.SelectedValue.ToString() != "")
            {
                var questionGroups = _questionService.GetAll().Where(x => x.LessonId == (int)comboBox1.SelectedValue);
                foreach (var item in questionGroups)
                {
                    string[] row = {item.Title, item.InsertTime.ToString() };
                    var listViewItem = new ListViewItem(row);
                    listViewItem.Tag = item.Id;
                    listView1.Items.Add(listViewItem);
                    
                }
            }
            else
            {
                MessageBox.Show("یک درس را انتخاب کنید");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            questionIds = new List<int>();
            questionIds.AddRange(_questionsService.GetAllQuestionsByQuestionGroupId((int)listView1.SelectedItems[0].Tag).Select(x => x.Id));
            tabControl1.SelectTab(1);
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            var bindingSource1 = new BindingSource();
            bindingSource1.DataSource = writers.Where(x => x.Name.StartsWith(comboBox_writer.Text)).ToList();
            if (bindingSource1.Count != 0)
                comboBox_writer.DataSource = bindingSource1.DataSource;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            progressBar4.Maximum = 100;
            progressBar4.Step = 1;
            progressBar4.Value = 0;
            backgroundWorker4.WorkerReportsProgress = true;
            backgroundWorker4.RunWorkerAsync();


        }
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox_wordfileAnswers.Text = openFileDialog1.FileName;
            }
        }
        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {

        
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

            if (questoinAnswerCount != questionIds.Count)
            {
                MessageBox.Show($"تعداد سوالات با تعداد جواب سوالات برابر نیست!\nتعداد سوالات{questionIds.Count} است.");
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

                    backgroundWorker4.ReportProgress((numberOfQ * 100) / questionIds.Count);
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

                this.Invoke(new MethodInvoker(delegate () { tabControl1.SelectTab(2); }));





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

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
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
                        backgroundWorker2.ReportProgress((numberOfQ * 100) / questionIds.Count);




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
    }
}
