using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.ViewModels.Student;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ViewModels.StudentMajorlist
{
    public class StudentMajorlistCreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Title { get; set; }

        public int StudentId { get; set; }

        public StudentViewModel Student { get; set; }


        public IEnumerable<int> MajorsId { get; set; }
    }
}
