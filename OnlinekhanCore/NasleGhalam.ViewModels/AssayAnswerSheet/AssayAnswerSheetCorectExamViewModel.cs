using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.AssayAnswerSheet
{
    public class AssayAnswerSheetCorectExamViewModel
    {

        public Tashih Tashih { get; set; }

        public int NumberOfQuestion { get; set; }
        public string CorrectAnswer { get; set; }


        public string Path { get; set; }

        public string AnswerPath { get; set; }

        
    }
}