using MediatR;
using Microsoft.Data.SqlClient;
using Persistence.Database;
using Response;
using Service.EventHandler.Commands;
using Service.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventHandler
{
    public class PersonCreateEventHandler : IRequestHandler<PersonCreateCommand, RequestResult>
    {
        private readonly ApplicationDbContext _context;
        public PersonCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResult> Handle(PersonCreateCommand command, CancellationToken cancellationToken)
        {
            #region Validations

            var personBD = _context.Persons.Where(u => u.NumberDocument == command.NumberDocument).FirstOrDefault();

            if (personBD != null)
                return new RequestResult { Success = false, Message = "Ya existe un registro con este mismo numero de documento." };

            #endregion

            try
            {
                var person = new Domain.Persons
                {
                    ID = Guid.NewGuid(),
                    NumberDocument = command.NumberDocument,
                    TypeDocument = command.TypeDocument,
                    Name = command.Name,
                    Lastname = command.Lastname,
                    Email = command.Email,
                    FullName = command.Name + " " + command.Lastname,
                    NumberDocumentTypeDocument = command.NumberDocument + " " + command.TypeDocument,
                    AddAt = DateTime.Now,
                };

                await _context.AddAsync(person);
                await _context.SaveChangesAsync(cancellationToken);

                var personDTO = new PersonsDTO
                {
                    ID = person.ID,
                    Name = person.Name,
                    Lastname = person.Lastname,
                    Email = person.Email,
                    FullName = person.Name + " " + person.Lastname,
                    AddAt = DateTime.Now,
                    NumberDocumentTypeDocument = person.NumberDocumentTypeDocument,
                    TypeDocument = person.TypeDocument,
                    NumberDocument = person.NumberDocument
                };

                return new RequestResult { Success = true, Message = "Persona registrada exitosamente.", Result = personDTO };
            }
            catch (SqlException ex) when (ex.Number == -1)
            {
                // Error de conexión a la base de datos
                return new RequestResult { Success = false, Message = "Error de conexión a la base de datos. Por favor, verifica tu conexión y vuelve a intentarlo." };
            }
            catch (SqlException ex)
            {
                // Otros errores relacionados con SQL
                return new RequestResult { Success = false, Message = $"Error de base de datos: {ex.Message}" };
            }
            catch (NullReferenceException)
            {
                // Error de referencia nula
                return new RequestResult { Success = false, Message = "Se produjo un error inesperado al procesar la información. Por favor, contacta al soporte técnico." };
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                return new RequestResult { Success = false, Message = $"Intenta nuevamente, error: {ex.Message}" };
            }
        }

    }
}
