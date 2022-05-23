using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class City
    {
        public City()
        {
            Users = new HashSet<User>();
            Resumes = new HashSet<Resume>();
            HistoryEducations = new HashSet<HistoryEducation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }

        public  Province Province { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Resume> Resumes { get; set; }
        public ICollection<HistoryEducation> HistoryEducations { get; set; }


    }
}
