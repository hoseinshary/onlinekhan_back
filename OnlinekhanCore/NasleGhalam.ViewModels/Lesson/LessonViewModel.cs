using System.Collections.Generic;
using System.Security.AccessControl;
using NasleGhalam.Common;
using NasleGhalam.ViewModels.EducationTree;
using NasleGhalam.ViewModels.LessonDepartment;

namespace NasleGhalam.ViewModels.Lesson
{
    public class LessonViewModel
    {
     
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public bool IsMain { get; set; }
        
        public int LookupId_Nezam { get; set; }

        public int NumberOfJudges { get; set; }

        public string File { get; set; }
        public string FilePath => $"/Api/Lesson/GetPictureFile/{File}".ToFullRelativePath();

        public IEnumerable<EducationTreeViewModel> EducationTrees { get; set; }


        public IEnumerable<LessonDepartmentViewModel> LessonDepartments { get; set; }
    }
}
