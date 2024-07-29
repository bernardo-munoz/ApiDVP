using Microsoft.Data.SqlClient;
using Persistence.Database;
using Response;
using Service.Queries.BO;
using Service.Queries.DTO;
using Service.Queries.Repositoty.Interface.IPersons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class PersonQueryServices : IReadPersons
    {

        private readonly ApplicationDbContext _context;
        public PersonsBO personObj;
        public PersonQueryServices(ApplicationDbContext context)
        {
            _context = context;
            personObj = new PersonsBO(_context);
        }

        public RequestResult GetAllPersons()
        {
            try
            {
                List<PersonsDTO> listPerson = personObj.GetAllPersons();

                if(listPerson != null && listPerson.Count > 0)
                {
                    return new RequestResult {  Success = true, Message = "Registros encontrados", Result = listPerson };
                }
                else
                {
                    return new RequestResult { Success = false, Message = "Registros no encontrados" };
                }
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

        public RequestResult GetPersonByNumberDocument(int numberDocument)
        {
            try
            {
                PersonsDTO? person = personObj.GetPersonByNumberDocument(numberDocument);

                if (person != null)
                {
                    return new RequestResult { Success = true, Message = "Registros encontrados", Result = person };
                }
                else
                {
                    return new RequestResult { Success = false, Message = "Registros no encontrados" };
                }
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
