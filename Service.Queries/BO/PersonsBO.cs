using Domain;
using Persistence.Database;
using Service.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.BO
{
    public class PersonsBO
    {
        private readonly ApplicationDbContext _context;
        public PersonsBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PersonsDTO> GetAllPersons()
        {
            //Se hace un IQueryable para aplanar la consulta lo mas posible
            IQueryable<Persons> listPersons = _context.Persons
                .Select(x => new Persons
                {
                    ID = x.ID,
                    Name = x.Name,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    NumberDocument = x.NumberDocument,
                    FullName = x.FullName,
                    TypeDocument = x.TypeDocument,
                    NumberDocumentTypeDocument = x.NumberDocumentTypeDocument,
                    AddAt = x.AddAt
                });

            //Con el Tolist nos traemos los datos a memoria y hacemos posprocesamiento...
            return listPersons.Select(x => new PersonsDTO
            {
                ID = x.ID,
                Name = x.Name,
                Lastname = x.Lastname,
                Email = x.Email,
                NumberDocument = x.NumberDocument,
                FullName = x.Name + " " + x.Lastname,
                TypeDocument = x.TypeDocument,
                NumberDocumentTypeDocument = x.NumberDocument + " " + x.TypeDocument,
                AddAt = x.AddAt
            }).ToList();

        }

        public PersonsDTO? GetPersonByNumberDocument(int numberDocument)
        {
            //Se hace un IQueryable para aplanar la consulta lo mas posible
            IQueryable<Persons> person = _context.Persons.Where(x => x.NumberDocument == numberDocument)
                .Select(x => new Persons
                {
                    ID = x.ID,
                    Name = x.Name,
                    Lastname = x.Lastname,
                    Email = x.Email,
                    NumberDocument = x.NumberDocument,
                    FullName = x.FullName,
                    TypeDocument = x.TypeDocument,
                    NumberDocumentTypeDocument = x.NumberDocumentTypeDocument,
                    AddAt = x.AddAt
                });

            
            return person.Select(x => new PersonsDTO
            {
                ID = x.ID,
                Name = x.Name,
                Lastname = x.Lastname,
                Email = x.Email,
                NumberDocument = x.NumberDocument,
                FullName = x.Name + " " + x.Lastname,
                TypeDocument = x.TypeDocument,
                NumberDocumentTypeDocument = x.NumberDocument + " " + x.TypeDocument,
                AddAt = x.AddAt
            }).FirstOrDefault();
        }
    }
}
