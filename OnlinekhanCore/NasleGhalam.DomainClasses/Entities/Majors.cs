using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Majors
    {
        public Majors()
        {
            StudentMajorlists = new HashSet<StudentMajorlist>();
            StudentMajorList_Major = new HashSet<StudentMajorList_Major>();
        }
        public int Id { get; set; }
        public string Course { get; set; }
        public int Code { get; set; }
        public string MajorTitle { get; set; }
        public int AdmissionFirst { get; set; }
        public int AdmissionSecond { get; set; }
        public string Woman { get; set; }
        public string Man { get; set; }
        public string University { get; set; }
        public int Field { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public string Apply { get; set; }
        public ICollection<StudentMajorList_Major> StudentMajorList_Major { get; set; }
        public ICollection<StudentMajorlist> StudentMajorlists { get; set; }
    }
}
