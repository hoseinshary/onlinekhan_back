using NasleGhalam.DomainClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ViewModels.StudentMajorlist
{
    public class StudentMajorlistGetStudentMajorsViewModel
    {
        public ICollection<Majors> Majors { get; set; }
    }
}
