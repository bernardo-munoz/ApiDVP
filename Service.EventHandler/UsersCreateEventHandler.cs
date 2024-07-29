using MediatR;
using Microsoft.Data.SqlClient;
using Persistence.Database;
using Response;
using Service.EventHandler.Commands;
using Service.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventHandler
{
    public class UsersCreateEventHandler : IRequestHandler<UsersCreateCommand, RequestResult>
    {
        private readonly ApplicationDbContext _context;
        public UsersCreateEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RequestResult> Handle(UsersCreateCommand command, CancellationToken cancellationToken)
        {
            #region Validations

            var userBD = _context.Users.Where(u => u.User== command.User).FirstOrDefault();

            if (userBD != null)
                return new RequestResult { Success = false, Message = "El usuario ya se encuentra registrado." };

            #endregion

            try
            {
                var user = new Domain.Users
                {
                    ID = Guid.NewGuid(),
                    User = command.User,
                    Pass = ComputeSha1AndMd5(command.Pass),
                    AddAt = DateTime.Now,
                };

                await _context.AddAsync(user);
                await _context.SaveChangesAsync(cancellationToken);

                var userDTO = new UsersDTO
                {
                    ID = user.ID,
                    User = user.User,
                    Pass = user.Pass,
                    AddAt = DateTime.Now
                };

                return new RequestResult { Success = true, Message = "Usuario registrado exitosamente.", Result = userDTO };
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

        public static string ComputeSha1(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        public static string ComputeMd5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }

        public static string ComputeSha1AndMd5(string input)
        {
            string sha1Hash = ComputeSha1(input);
            return ComputeMd5(sha1Hash);
        }
    }
}
