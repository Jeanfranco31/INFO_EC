using EL.DTO;
using EL.Response;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Auth.Login
{
    public class LoginService
    {
        private Response response = new();
        private string connection;
        public LoginService(IConfiguration _configuration)
        {
            connection = _configuration.GetConnectionString(Common.ADOquery.cadenaConexion)!;
        }

        public async Task<Response> validateLogin(LoginDTO login) {
            using (SqlConnection conn = new(connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_VALIDAR_LOGIN, conn);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.cedula, SqlDbType.VarChar, 10) { Value = login.cedula });

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        LoginDTO loginData = new LoginDTO
                        {
                            password = reader[Common.ColumnNames.employeePass].ToString()!
                        };
                        if (loginData.password.Equals(Helper.Helper.Encrypt(login.password)))
                        {
                            response.Data = string.Empty;
                            response.message = EL.Messages.Message.loginSuccess;
                        }
                        else
                        {
                            response.Data = "ERROR";
                            response.message = "FALLO AL INICIAR SESION";
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.Data = string.Empty;
                    response.message = EL.Messages.Message.loginError;                 
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }
    }
}
