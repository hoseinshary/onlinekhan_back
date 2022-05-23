using NasleGhalam.ViewModels.EducationTree;

namespace NasleGhalam.ViewModels.EducationSubGroup
{
    public class EducationSubGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EducationTreeId { get; set; }

        public EducationTreeViewModel EducationTree { get; set; }
    }
}
