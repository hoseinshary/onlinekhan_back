using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.StudentMajorlist
{
    public class MajorViewModel
    {
       
        public int Id { get; set; }
        public string Course { get; set; }
        public int Code { get; set; }
        public string MajorTitle { get; set; }
        public int AdmissionFirst { get; set; }
        public int AdmissionSecond { get; set; }
        public string Woman { get; set; }
        public string Man { get; set; }
        public string University { get; set; }

        [Display(Name = "رشته تحصیلی")]
        public Field Field { get; set; }

        public string FieldName => Field.GetDisplayName();

        public string Description { get; set; }

        public string  Apply { get; set; }

    }
}
