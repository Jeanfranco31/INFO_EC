using BL.Common;
using EL.DTO;
using EL.Response;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using INFO_EC_BACKEND.Models;

namespace BL.Empleado
{
    public class EmpleadoService : IEmpleado
    {
        private string _connection;
        private Response response = new();

        public EmpleadoService(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString(ADOquery.cadenaConexion)!;
        }


        public async Task<Response> ObtenerEmpleados()
        {
            List<EmpleadoDTO> lista = new();
            EmpleadoDTO empleado = new();
            using (SqlConnection conn = new(_connection)) {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_OBTENER_TODOS_EMPLEADOS,conn);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        empleado = Helper.Helper.ObtenerReader(reader);
                        lista.Add(empleado);
                    }
                    response.Data = lista;
                    response.message = EL.Messages.Message.empleadoObtenido;
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> RegistrarEmpleado(EmpleadoDTO empleado)
        {
            using (SqlConnection conn = new(_connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(ADOquery.SP_REGISTRAR_EMPLEADO, conn);

                    command.Parameters.Add(new SqlParameter(Common.Parameters.claveEmpleado, SqlDbType.VarChar) { Value = empleado.clave });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.nombreRol.ToUpper(), SqlDbType.VarChar, 30) { Value = empleado.NombreRol });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.nombreEmpleado, SqlDbType.VarChar, 50) { Value = empleado.NombreEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.apellidoEmpleado, SqlDbType.VarChar, 50) { Value = empleado.ApellidoEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.cedulaEmpleado, SqlDbType.VarChar, 10) { Value = empleado.CedulaEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.telefonoEmpleado, SqlDbType.VarChar, 10) { Value = empleado.TelefonoEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.correoEmpleado, SqlDbType.VarChar, 150) { Value = empleado.CorreoEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.direccionEmpleado, SqlDbType.VarChar, 200) { Value = empleado.DireccionEmpleado });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.edadEmpleado, SqlDbType.Int) { Value = empleado.EdadEmpleado });

                    int row = await command.ExecuteNonQueryAsync();

                    if (row >= 0)
                    {
                        EmpleadoDTO empleadoObtenido = ValidarEmpleadoExiste(empleado.CedulaEmpleado!);
                        response.message = EL.Messages.Message.empleadoRegistrado;
                        response.Data = empleadoObtenido;
                    }
                    else
                    {
                        response.message = EL.Messages.Message.empleadoNoRegistrado;
                    }
                }
                catch (Exception ex)
                {
                    response.message = EL.Messages.Message.empleadoNoRegistrado + ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> EditarDatosEmpleado(EmpleadoDTO empleado)
        {
            using (SqlConnection conn = new(_connection)) {
                try
                {
                    if (ValidarEmpleadoExiste(empleado.CedulaEmpleado) != null)
                    {
                        await conn.OpenAsync();
                        SqlCommand command = new(Common.ADOquery.SP_EDITAR_EMPLEADO, conn);
                        command.Parameters.Add(new SqlParameter(Common.Parameters.nombreRol.ToUpper(), SqlDbType.VarChar, 30) { Value = empleado.NombreRol });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.nombreEmpleado, SqlDbType.VarChar, 50) { Value = empleado.NombreEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.apellidoEmpleado, SqlDbType.VarChar, 50) { Value = empleado.ApellidoEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.cedulaEmpleado, SqlDbType.VarChar, 10) { Value = empleado.CedulaEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.telefonoEmpleado, SqlDbType.VarChar, 10) { Value = empleado.TelefonoEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.correoEmpleado, SqlDbType.VarChar, 150) { Value = empleado.CorreoEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.direccionEmpleado, SqlDbType.VarChar, 200) { Value = empleado.DireccionEmpleado });
                        command.Parameters.Add(new SqlParameter(Common.Parameters.edadEmpleado, SqlDbType.Int) { Value = empleado.EdadEmpleado });

                        int rowAffected = await command.ExecuteNonQueryAsync();
                        if (rowAffected > 0)
                        {
                            EmpleadoDTO datosActualizados = ValidarEmpleadoExiste(empleado.CedulaEmpleado);
                            response.Data = datosActualizados;
                            response.message = "Informacion del empleado actualizada";
                        }
                    }
                    else 
                    {
                        response.message = "Al parecer no existe un empleado con el numero de cedula ingresado";
                        response.Data = String.Empty;
                    }
                }
                catch (Exception ex)
                {
                    response.message = ex.Message;
                    response.Data = String.Empty;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> EliminarEmpleado(string numeroCedula)
        {
            using (SqlConnection conn = new(_connection)) {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_ELIMINAR_EMPLEADO_POR_CEDULA,conn);
                    command.Parameters.Add(new SqlParameter(Parameters.cedulaEmpleado, SqlDbType.VarChar, 10) { Value = numeroCedula});

                    EmpleadoDTO empleadoEliminado = ValidarEmpleadoExiste(numeroCedula);

                    int row = await command.ExecuteNonQueryAsync();

                    if (row >= 0) {
                        response.Data = empleadoEliminado;
                        response.message = EL.Messages.Message.empleadoEliminado;
                    }
                }
                catch (Exception ex) 
                {
                    response.message = EL.Messages.Message.empleadoNoEliminado + ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> ObtenerEmpleadoPorCedula(string numeroCedula)
        {
            using (SqlConnection conn = new(_connection))
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(ADOquery.SP_BUSCAR_EMPLEADO_POR_CEDULA, conn);
                    command.Parameters.Add(new SqlParameter(Parameters.cedulaEmpleado, SqlDbType.VarChar, 10) { Value = numeroCedula });

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        EmpleadoDTO readerObtenido = Helper.Helper.ObtenerReader(reader);
                        response.message = EL.Messages.Message.empleadoEncontrado;
                        response.Data = readerObtenido;
                    }
                }
                catch (Exception ex)
                {
                    response.message = EL.Messages.Message.empleadoNoEncontrado + ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public EmpleadoDTO ValidarEmpleadoExiste(string cedulaEmpleadoABuscar)
        {
            EmpleadoDTO empleado = new();
            EmpleadoDTO empleadoExiste = new();

            using (SqlConnection conn = new(_connection))
            {
                conn.Open();
                SqlCommand command = new(Common.ADOquery.SP_BUSCAR_EMPLEADO_POR_CEDULA, conn);
                command.Parameters.Add(new SqlParameter(Common.Parameters.cedulaEmpleado, SqlDbType.VarChar, 10) { Value = cedulaEmpleadoABuscar });
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    empleadoExiste = Helper.Helper.ObtenerReader(reader);
                }
                conn.Close();
            }          
            return empleadoExiste;
        }

    }
}
