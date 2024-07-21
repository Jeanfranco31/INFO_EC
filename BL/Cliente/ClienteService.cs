using EL.DTO;
using EL.Response;
using EL.Helper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Cliente
{
    public class ClienteService : ICliente
    {
        private string _connection;
        private Response response = new();
        private List<ClienteDTO> listaClientes= new();
        public ClienteService(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString(Common.ADOquery.cadenaConexion)!;
        }

        public async Task<Response> RegistrarCliente(ClienteDTO cliente)
        {
            using (SqlConnection conn = new SqlConnection(_connection)) 
            {
                await conn.OpenAsync();
                if (Helper.ValidarCamposVacios(cliente))
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(Common.ADOquery.SP_REGISTRAR_CLIENTE, conn);

                        command.Parameters.Add(Common.Parameters.nombreCliente, SqlDbType.VarChar).Value = cliente.Nombre;
                        command.Parameters.Add(Common.Parameters.apellidoCliente, SqlDbType.VarChar).Value = cliente.Apellido;
                        command.Parameters.Add(Common.Parameters.cedulaCliente, SqlDbType.VarChar).Value = cliente.Cedula;
                        command.Parameters.Add(Common.Parameters.correoCliente, SqlDbType.VarChar).Value = cliente.Correo;
                        command.Parameters.Add(Common.Parameters.direccionCliente, SqlDbType.VarChar).Value = cliente.Direccion;
                        command.Parameters.Add(Common.Parameters.telefonoCliente, SqlDbType.VarChar).Value = cliente.Telefono;
                        command.Parameters.Add(Common.Parameters.edadCliente, SqlDbType.Int).Value = cliente.Edad;

                        int row = await command.ExecuteNonQueryAsync();

                        if (row > 0)
                        {
                            ClienteDTO clienteDTO = new ClienteDTO
                            {
                                Nombre = cliente.Nombre,
                                Apellido = cliente.Apellido,
                                Cedula = cliente.Cedula,
                                Correo = cliente.Correo,
                                Direccion = cliente.Direccion,
                                Telefono = cliente.Telefono,
                                Edad = cliente.Edad
                            };

                            listaClientes.Add(clienteDTO);

                            response.message = EL.Messages.Message.clienteRegistrado;
                            response.Data = listaClientes;

                            await conn.CloseAsync();
                        }

                    }
                    catch (Exception ex)
                    {
                        response.message = EL.Messages.Message.clienteNoRegistrado + ex.Message;
                    }
                }
            }
                return response;
        }

        public async Task<Response> BuscarClientePorCedula(string numeroCedula) {
            using (SqlConnection conn = new SqlConnection(_connection)) {
                await conn.OpenAsync();
                try
                {
                    SqlCommand command = new SqlCommand(Common.ADOquery.SP_BUSCAR_CLIENTE_POR_CEDULA, conn);
                    command.Parameters.Add(Common.Parameters.cedulaCliente,SqlDbType.VarChar).Value = numeroCedula;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        ClienteDTO clienteEncontrado = new ClienteDTO
                        {
                            Nombre = reader[Common.ColumnNames.clienteColumnNombre].ToString()!,
                            Apellido = reader[Common.ColumnNames.clienteColumnApellido].ToString()!,
                            Cedula = reader[Common.ColumnNames.clienteColumnCedula].ToString()!,
                            Telefono = reader[Common.ColumnNames.clienteColumnTelefono].ToString()!,
                            Correo = reader[Common.ColumnNames.clienteColumnCorreo].ToString()!,
                            Direccion = reader[Common.ColumnNames.clienteColumnDireccion].ToString()!,
                            Edad = Convert.ToInt32(reader[Common.ColumnNames.clienteColumnEdad])
                        };
                        listaClientes.Add(clienteEncontrado);
                        response.message = EL.Messages.Message.clienteExiste;
                        response.Data = listaClientes;
                    }
                    else 
                    {
                        response.message = EL.Messages.Message.clienteNoExiste;
                    }
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                }
            }
            return response;
        }

        public async Task<Response> ActualizarClientePorCedula(ClienteDTO cliente) {

            return response;
        }

        public async Task<Response> EliminarClientePorCedula(string numeroCedula) {
            using (SqlConnection conn = new SqlConnection(_connection)) 
            {
                await conn.OpenAsync();
                try 
                {
                    SqlCommand command = new SqlCommand(Common.ADOquery.SP_ELIMINAR_CLIENTE_POR_CEDULA, conn);
                    command.Parameters.Add(Common.Parameters.cedulaCliente,SqlDbType.VarChar).Value = numeroCedula;

                    ClienteDTO cliente = validarClienteExiste(numeroCedula);

                    await command.ExecuteNonQueryAsync();
                   
                    response.message = "Cliente eliminado satisfactoriamente.";
                    response.Data = cliente;
          
                    await conn.CloseAsync();

                }
                catch(Exception ex)
                {
                    response.message = ex.Message;
                }
            }
            return response;
        }

        private ClienteDTO validarClienteExiste(string numeroCedula) {
            using (SqlConnection conn = new SqlConnection(_connection)) {
                conn.Open();
                SqlCommand command = new SqlCommand(Common.ADOquery.SP_BUSCAR_CLIENTE_POR_CEDULA, conn);
                command.Parameters.Add(Common.Parameters.cedulaCliente, SqlDbType.VarChar).Value = numeroCedula;

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    ClienteDTO clienteExiste = new ClienteDTO
                    {
                        Nombre = reader[Common.ColumnNames.clienteColumnNombre].ToString()!,
                        Apellido = reader[Common.ColumnNames.clienteColumnApellido].ToString()!,
                        Cedula = reader[Common.ColumnNames.clienteColumnCedula].ToString()!,
                        Correo = reader[Common.ColumnNames.clienteColumnCorreo].ToString()!,
                        Direccion = reader[Common.ColumnNames.clienteColumnDireccion].ToString()!,
                        Telefono = reader[Common.ColumnNames.clienteColumnTelefono].ToString()!,
                        Edad = Convert.ToInt32(reader[Common.ColumnNames.clienteColumnEdad])
                    };
                    return clienteExiste;
                }
                conn.Close();
            }
            return null!;
        }

    }
}
