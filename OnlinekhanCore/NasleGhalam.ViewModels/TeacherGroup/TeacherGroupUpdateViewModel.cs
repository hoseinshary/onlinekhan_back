using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ViewModels.TeacherGroup
{
    public class TeacherGroupUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TeacherId { get; set; }

        public IEnumerable<int> StudentsId { get; set; }
    }
}
