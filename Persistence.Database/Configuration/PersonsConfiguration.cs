using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Configuration
{
    public class PersonsConfiguration
    {
        public PersonsConfiguration(EntityTypeBuilder<Persons> entityTypeBuilder) 
        {
            entityTypeBuilder.HasKey(x => x.ID);
            entityTypeBuilder.Property(x => x.Name).IsRequired();
            entityTypeBuilder.Property(x => x.Lastname).IsRequired();
            entityTypeBuilder.Property(x => x.NumberDocument).IsRequired();
            entityTypeBuilder.Property(x => x.Email).IsRequired();

        }

    }
}
