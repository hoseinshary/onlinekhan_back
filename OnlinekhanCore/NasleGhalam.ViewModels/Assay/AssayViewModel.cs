using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Assay
{
    public class AssayViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public string Title { get; set; }


        [Display(Name = "")]
        public int Time { get; set; }


        [Display(Name = "")]
        public int LookupId_Importance { get; set; }


        [Display(Name = "")]
        public int LookupId_Type { get; set; }


        [Display(Name = "")]
        public int LookupId_QuestionType { get; set; }


        [Display(Name = "")]
        public bool IsPublic { get; set; }


        [Display(Name = "")]
        public bool IsOnline { get; set; }


        [Display(Name = "")]
        public int UserId { get; set; }


        [Display(Name = "")]
        public DateTime DateTimeCreate { get; set; }


        public AssayVarient NumberOfVarient { get; set; }

        [Display(Name = "فایل")]
        public string File1 { get; set; }

        public string AssayPdfPath1 => $"/Api/Assay/GetPdfFile/{File1}".ToFullRelativePath();

        public string AssayWordPath1 => $"/Api/Assay/GetWordFile/{File1}".ToFullRelativePath();

        public string File2 { get; set; }

        public string AssayPdfPath2 => $"/Api/Assay/GetPdfFile/{File2}".ToFullRelativePath();

        public string AssayWordPath2 => $"/Api/Assay/GetWordFile/{File2}".ToFullRelativePath();
        public string File3 { get; set; }

        public string AssayPdfPath3 => $"/Api/Assay/GetPdfFile/{File3}".ToFullRelativePath();

        public string AssayWordPath3 => $"/Api/Assay/GetWordFile/{File3}".ToFullRelativePath();
        public string File4 { get; set; }

        public string AssayPdfPath4 => $"/Api/Assay/GetPdfFile/{File4}".ToFullRelativePath();

        public string AssayWordPath4 => $"/Api/Assay/GetWordFile/{File4}".ToFullRelativePath();


        public IList<string> QuestionsFile { get; set; }

        public IList<string> QuestionsAnswer { get; set; }


        public IList<string> QuestionsPath { get; set; }




    }
}
