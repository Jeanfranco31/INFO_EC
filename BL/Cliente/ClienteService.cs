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
using INFO_EC_BACKEND.Models;

namespace BL.Cliente
{
    public class ClienteService : ICliente
    {
        private string _connection;
        private Response response = new();
        private List<ClienteDTO> listaClientes= new();
        private ClienteDTO clienteDTO = new ClienteDTO();
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

                        command.Parameters.Add(Common.Parameters.nombreCliente, SqlDbType.VarChar,50).Value = cliente.Nombre;
                        command.Parameters.Add(Common.Parameters.apellidoCliente, SqlDbType.VarChar,50).Value = cliente.Apellido;
                        command.Parameters.Add(Common.Parameters.cedulaCliente, SqlDbType.VarChar,10).Value = cliente.Cedula;
                        command.Parameters.Add(Common.Parameters.correoCliente, SqlDbType.VarChar,150).Value = cliente.Correo;
                        command.Parameters.Add(Common.Parameters.direccionCliente, SqlDbType.VarChar,200).Value = cliente.Direccion;
                        command.Parameters.Add(Common.Parameters.telefonoCliente, SqlDbType.VarChar,10).Value = cliente.Telefono;
                        command.Parameters.Add(Common.Parameters.edadCliente, SqlDbType.Int).Value = cliente.Edad;

                        await command.ExecuteNonQueryAsync();

                        ClienteDTO clienteEncontrado = validarClienteExiste(cliente.Cedula);

                        if (clienteEncontrado != null) {
                            response.Data = clienteEncontrado;
                            response.message = EL.Messages.Message.clienteRegistrado;
                        }                        
                    }
                    catch (Exception ex)
                    {
                        response.message = EL.Messages.Message.clienteNoRegistrado + ex.Message;
                    }
                    finally
                    {
                        await conn.CloseAsync();
                    }
                }
            }
                return response;
        }

        public async Task<Response> BuscarClientePorCedula(string numeroCedula) {
            using (SqlConnection conn = new SqlConnection(_connection)) {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new SqlCommand(Common.ADOquery.SP_BUSCAR_CLIENTE_POR_CEDULA, conn);
                    command.Parameters.Add(Common.Parameters.cedulaCliente,SqlDbType.VarChar).Value = numeroCedula;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        ClienteDTO readerObtenido = GenerarReader(reader);

                        response.message = EL.Messages.Message.clienteExiste;
                        response.Data = readerObtenido;
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
            using (SqlConnection conn = new SqlConnection(_connection))
            {
                try
                {
                    await conn.OpenAsync();

                    SqlCommand command = new SqlCommand(Common.ADOquery.SP_EDITAR_CLIENTE, conn);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(Common.Parameters.nombreCliente, SqlDbType.VarChar,50).Value = cliente.Nombre);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.apellidoCliente, SqlDbType.VarChar,50).Value = cliente.Apellido);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.cedulaCliente, SqlDbType.VarChar,10).Value = cliente.Cedula);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.telefonoCliente, SqlDbType.VarChar,10).Value = cliente.Telefono);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.correoCliente, SqlDbType.VarChar,150).Value = cliente.Correo);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.direccionCliente, SqlDbType.VarChar,200).Value = cliente.Direccion);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.edadCliente, SqlDbType.Int).Value = cliente.Edad);

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync()) {
                        ClienteDTO readerObtenido =  GenerarReader(reader);
                        response.Data = readerObtenido;
                        response.message = EL.Messages.Message.clienteEditado; 
                    }
                }
                catch (Exception ex)
                {
                    response.message = EL.Messages.Message.clienteNoEditado + string.Empty + ex.Message;
                }
                finally 
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> EliminarClientePorCedula(string numeroCedula) {
            using (SqlConnection conn = new SqlConnection(_connection)) 
            {
                await conn.OpenAsync();
                try
                {
                    SqlCommand command = new SqlCommand(Common.ADOquery.SP_ELIMINAR_CLIENTE_POR_CEDULA, conn);
                    command.Parameters.Add(Common.Parameters.cedulaCliente, SqlDbType.VarChar, 10).Value = numeroCedula;

                    ClienteDTO cliente = validarClienteExiste(numeroCedula);

                    if (cliente != null)
                    {
                        int row = await command.ExecuteNonQueryAsync();
                        if (row > 0)
                        {
                            response.message = "Cliente eliminado satisfactoriamente.";
                            response.Data = cliente;
                        }
                    }
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                }
                finally {
                    await conn.CloseAsync();
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
        public ClienteDTO GenerarReader(SqlDataReader reader)
        {
            if (reader != null)
            {
                ClienteDTO clienteDTO = new ClienteDTO
                {
                    Nombre = reader[Common.ColumnNames.clienteColumnNombre].ToString()!,
                    Apellido = reader[Common.ColumnNames.clienteColumnApellido].ToString()!,
                    Cedula = reader[Common.ColumnNames.clienteColumnCedula].ToString()!,
                    Correo = reader[Common.ColumnNames.clienteColumnCorreo].ToString()!,
                    Direccion = reader[Common.ColumnNames.clienteColumnDireccion].ToString()!,
                    Telefono = reader[Common.ColumnNames.clienteColumnTelefono].ToString()!,
                    Edad = Convert.ToInt32(reader[Common.ColumnNames.clienteColumnEdad])
                };
            }
            return clienteDTO;
        }
    }
}
