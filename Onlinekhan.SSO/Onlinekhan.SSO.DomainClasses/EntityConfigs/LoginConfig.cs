using System.Data.Entity.ModelConfiguration;
using Onlinekhan.SSO.DomainClasses.Entities;

namespace Onlinekhan.SSO.DomainClasses.EntityConfigs
{
    public class LoginConfig : EntityTypeConfiguration<Login>
    {
        public LoginConfig()
        {
            HasKey(x => x.Id);
          

    
            HasRequired(x => x.User)
                .WithMany(x => x.Logins)
                .HasForeignKey(x => x.User_id)
                .WillCascadeOnDelete(false);



          
        }
    }
}
