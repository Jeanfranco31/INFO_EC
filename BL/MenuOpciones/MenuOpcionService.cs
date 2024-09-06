using EL.DTO;
using EL.Response;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.MenuOpciones
{
    public class MenuOpcionService : IMenuOpcion
    {
        private Response response = new();
        private string _connection;
        private MenuOpcionDTO menuOpcion = new();
        private List<MenuOpcionDTO> listaMenuOpciones = new();
        
        public MenuOpcionService(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString(Common.ADOquery.cadenaConexion)!;
        }
        
        public async Task<Response> getAllOptions()
        {
            using (SqlConnection conn = new(_connection)) 
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_OBTENER_MENU_OPCIONES,conn);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync()) 
                    {
                        menuOpcion = new MenuOpcionDTO
                        {
                            Id_MenuOpcion = Convert.ToInt32(reader["ID_MENU"]),
                            Nombre_Opcion = reader["NOMBRE_OPCION"].ToString()!
                        };
                        listaMenuOpciones.Add(menuOpcion);
                    }
                    response.Data = listaMenuOpciones;
                    response.message = "Menu de opciones obtenido";
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.Data = string.Empty;
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
