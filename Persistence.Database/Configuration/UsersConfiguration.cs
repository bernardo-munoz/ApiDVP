using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Configuration
{
    public class UsersConfiguration
    {
        public UsersConfiguration(EntityTypeBuilder<Users> entityTypeBuilder) {
            entityTypeBuilder.HasKey(x => x.ID);
            entityTypeBuilder.Property(x => x.User).IsRequired();
            entityTypeBuilder.Property(x => x.Pass).IsRequired();
        }
    }
}
