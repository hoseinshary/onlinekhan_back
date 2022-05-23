using System;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Log
    {

        public int Id { get; set; }
        public string TableName { get; set; }
        public CrudType CrudType { get; set; }
        public DateTime DateTime { get; set; }
        public int ObjectId { get; set; }
        public string ObjectValue { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
