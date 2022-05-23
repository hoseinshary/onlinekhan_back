using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NasleGhalam.DomainClasses.Entities;

using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ViewModels.StudentMajorlist
{
    public class StudentMajorlistGetStudentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "Required")]
        [MaxLength(200, ErrorMessageResourceType = typeof(ErrorResources), ErrorMessageResourceName = "MaxLen")]
        public string Title { get; set; }

        public int StudentId { get; set; }
        public DateTime CreationDate { get; set; }

        public NasleGhalam.DomainClasses.Entities.Student Student { get; set; }
    }
}
