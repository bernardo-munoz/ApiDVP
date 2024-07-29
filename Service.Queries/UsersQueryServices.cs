using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Database;
using Response;
using Service.Queries.BO;
using Service.Queries.DTO;
using Service.Queries.Repositoty.Interface.IUsers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class UsersQueryServices : IReadUsers
    {
        private readonly ApplicationDbContext _context;
        private IConfiguration _configuration;
        public UsersBO usersObj;
        public UsersQueryServices(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            usersObj = new UsersBO(_context);
            _configuration = configuration;
        }
        public RequestResult GetAllUsers()
        {
            try
            {
                List<UsersDTO> listUsers = usersObj.GetAllUsers();

                if (listUsers != null && listUsers.Count > 0)
                {
                    return new RequestResult { Success = true, Message = "Registros encontrados", Result = listUsers };
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

        public RequestResult GetUsersByUser(string user)
        {
            try
            {
                UsersDTO? users = usersObj.GetUsersByUser(user);

                if (users != null)
                {
                    return new RequestResult { Success = true, Message = "Registros encontrados", Result = users };
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


        public RequestResult Login(LoginDTO LoginDTO)
        {
            try
            {
                #region Validations

                if (string.IsNullOrWhiteSpace(LoginDTO.User))
                    return new RequestResult { Success = false, Message = "El usuario es invalido" };

                if (string.IsNullOrWhiteSpace(LoginDTO.Pass))
                    return new RequestResult { Success = false, Message = "La contraseña es invalida" };

                UsersDTO user = usersObj.GetUsersByUser(LoginDTO.User);

                if (user == null)
                    return new RequestResult { Success = false, Message = "Usuario no encontrado" };

                if (user.Pass != ComputeSha1AndMd5(LoginDTO.Pass))
                    return new RequestResult { Success = false, Message = "Contraseña incorrecta" };

                // Generate JWT token
                var token = GenerateJwtToken();

                return new RequestResult { Success = true, Message = "Sesión iniciada", Result = token };

                #endregion

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
                return new RequestResult { Success = false, Message = "Se produjo un error inesperado al procesar el usuario. Por favor, contacta al soporte técnico." };
            }
            catch (Exception ex)
            {
                // Otros tipos de errores
                return new RequestResult { Success = false, Message = $"Intenta nuevamente, error: {ex.Message}" };
            }
        }

        public RequestResult ValidateToken(string token)
        {
            try
            {
                #region Validations

                if (string.IsNullOrWhiteSpace(token))
                    return new RequestResult { Success = false, Message = "El token es inválido" };

                var jwtSettings = _configuration.GetSection("Jwt");
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var key = jwtSettings["Key"];

                if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(key))
                    throw new ArgumentException("La configuración de JWT no está completa");

                #endregion

                // Configurar parámetros de validación del token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };

                // Crear instancia de JwtSecurityTokenHandler para validar el token
                var tokenHandler = new JwtSecurityTokenHandler();

                ClaimsPrincipal claimsPrincipal;
                try
                {
                    claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                }
                catch (Exception ex)
                {
                    return new RequestResult { Success = false, Message = $"Error al validar el token: {ex.Message}", Result = false };
                }

                // Verificar si hay claims mínimos requeridos
                if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
                {
                    return new RequestResult { Success = false, Message = "Token no contiene claims válidos.", Result = false };
                }

                // Si la validación es exitosa, el token es válido
                return new RequestResult { Success = true, Message = "Token válido.", Result = true };
            }
            catch (SecurityTokenExpiredException)
            {
                return new RequestResult { Success = false, Message = "El token ha expirado.", Result = false };
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return new RequestResult { Success = false, Message = "Firma del token inválida.", Result = false };
            }
            catch (SecurityTokenValidationException)
            {
                return new RequestResult { Success = false, Message = "Validación del token fallida.", Result = false };
            }
            catch (ArgumentException)
            {
                return new RequestResult { Success = false, Message = "La configuración de JWT es inválida.", Result = false };
            }
            catch (Exception ex)
            {
                return new RequestResult { Success = false, Message = $"Error al validar el token: {ex.Message}", Result = false };
            }
        }

        private string GenerateJwtToken()
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var key = jwtSettings["Key"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[]
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, user.Document),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.NameIdentifier, user.Document)
            //};

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                //claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
