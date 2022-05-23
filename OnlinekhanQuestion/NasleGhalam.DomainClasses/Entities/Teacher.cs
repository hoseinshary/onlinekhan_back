using System.Collections.Generic;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Teacher
    {
        public Teacher()
        {

        }
        
        public int Id { get; set; }

        public string FatherName { get; set; }

        public string Address { get; set; }

        

        public User User { get; set; }

      
    }
}
