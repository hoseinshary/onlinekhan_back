﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.ViewModels.Teacher;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.TeacherGroup
{
    public class TeacherGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TeacherId { get; set; }

        

        public TeacherViewModel Teacher { get; set; }


        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}
