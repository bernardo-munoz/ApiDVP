using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Configuration
{
    public class TypeDocumentsConfiguration
    {
        public TypeDocumentsConfiguration(EntityTypeBuilder<TypeDocuments> entityTypeBuilder) {
            entityTypeBuilder.HasKey(x => x.ID);
            entityTypeBuilder.Property(x => x.Type).IsRequired();
            entityTypeBuilder.Property(x => x.Abbreviation).IsRequired();

            var random = new Random();

            var typeDocList = new List<TypeDocuments>
            {
                new TypeDocuments
                {
                    ID = Guid.NewGuid(),
                    Type = "Cédula Ciudadanía",
                    Abbreviation = "CC",
                    State = 1
                },
                new TypeDocuments
                {
                    ID = Guid.NewGuid(),
                    Type = "Cédula Extranjería",
                    Abbreviation = "CE",
                    State = 1
                },
                new TypeDocuments
                {
                    ID = Guid.NewGuid(),
                    Type = "Pasaporte",
                    Abbreviation = "PA",
                    State = 1
                }
            };

            entityTypeBuilder.HasData(typeDocList);
        }
    }
}
