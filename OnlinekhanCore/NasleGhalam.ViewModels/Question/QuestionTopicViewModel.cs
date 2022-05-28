using System;
using System.Collections.Generic;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.Question
{
    public class QuestionTopicViewModel
    {
        public int Id { get; set; }


        public List<int> Topics { get; set; } = new List<int>();

   

    }
}
