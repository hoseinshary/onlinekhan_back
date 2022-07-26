using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.Lookup;
using NasleGhalam.ViewModels.QuestionAnswer;
using NasleGhalam.ViewModels.QuestionJudge;
using NasleGhalam.ViewModels.QuestionOption;
using NasleGhalam.ViewModels.Tag;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.ViewModels.User;
using NasleGhalam.ViewModels.Writer;

namespace NasleGhalam.ViewModels.Question
{
    public class AllQuestionTopicedByUser
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int QuestionTopiced { get; set; }
        public List<int> QuestionTopicedId { get; set; }

    }
}
