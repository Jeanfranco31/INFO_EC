using BL.Common;
using EL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helper
{
    public class Helper
    {
        ClienteDTO clienteDTO = new();
        
        public static bool ValidarCamposVacios(ClienteDTO cliente) { 
            bool validarCampos = false;

            if (!string.IsNullOrWhiteSpace(cliente.Nombre)) {
                if (!string.IsNullOrWhiteSpace(cliente.Apellido)) {
                    if (!string.IsNullOrWhiteSpace(cliente.Cedula)) {
                        if (!string.IsNullOrWhiteSpace(cliente.Correo)) {
                            if (!string.IsNullOrWhiteSpace(cliente.Direccion)) {
                                if (!string.IsNullOrWhiteSpace(cliente.Telefono)) {
                                    if (cliente.Edad >= 18) {
                                        validarCampos = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return validarCampos;
        }

        public static EmpleadoDTO ObtenerReader(SqlDataReader reader)
        {
            EmpleadoDTO data = new EmpleadoDTO();

            if (reader.Read())
            {
                data = new EmpleadoDTO
                {
                    NombreRol = reader[ColumnNames.empleadoColumnNombreRol] != DBNull.Value ? reader[ColumnNames.empleadoColumnNombreRol].ToString() : null,
                    NombreEmpleado = reader[ColumnNames.empleadoColumnNombre] != DBNull.Value ? reader[ColumnNames.empleadoColumnNombre].ToString() : null,
                    ApellidoEmpleado = reader[ColumnNames.empleadoColumnApellido] != DBNull.Value ? reader[ColumnNames.empleadoColumnApellido].ToString() : null,
                    CedulaEmpleado = reader[ColumnNames.empleadoColumnCedula] != DBNull.Value ? reader[ColumnNames.empleadoColumnCedula].ToString() : null,
                    TelefonoEmpleado = reader[ColumnNames.empleadoColumnTelefono] != DBNull.Value ? reader[ColumnNames.empleadoColumnTelefono].ToString() : null,
                    CorreoEmpleado = reader[ColumnNames.empleadoColumnCorreo] != DBNull.Value ? reader[ColumnNames.empleadoColumnCorreo].ToString() : null,
                    DireccionEmpleado = reader[ColumnNames.empleadoColumnDireccion] != DBNull.Value ? reader[ColumnNames.empleadoColumnDireccion].ToString() : null,
                    EdadEmpleado = reader[ColumnNames.empleadoColumnEdad] != DBNull.Value ? Convert.ToInt32(reader[ColumnNames.empleadoColumnEdad]) : 0
                };
            }
            return data;
        }

        public static ClienteDTO GenerarReader(SqlDataReader reader)
        {
            while (reader != null)
            {
                return new ClienteDTO
                {
                    Nombre = reader[Common.ColumnNames.clienteColumnNombre].ToString()! ?? string.Empty,
                    Apellido = reader[Common.ColumnNames.clienteColumnApellido].ToString()! ?? string.Empty,
                    Cedula = reader[Common.ColumnNames.clienteColumnCedula].ToString()! ?? string.Empty,
                    Correo = reader[Common.ColumnNames.clienteColumnCorreo].ToString()! ?? string.Empty,
                    Direccion = reader[Common.ColumnNames.clienteColumnDireccion].ToString()! ?? string.Empty,
                    Telefono = reader[Common.ColumnNames.clienteColumnTelefono].ToString()! ?? string.Empty,
                    Edad = reader[Common.ColumnNames.clienteColumnEdad] != DBNull.Value ? Convert.ToInt32(reader[Common.ColumnNames.clienteColumnEdad]) : 0
                };
            }
            return new ClienteDTO();
        }

        public static ProductoDto GenerateReaderProduct(SqlDataReader reader)
        {
            ProductoDto producto = new();
            if (reader != null)
            {
                producto = new ProductoDto
                {
                    Id_Producto = Convert.ToInt32(reader["Id_Producto"]),
                    Nombre = reader["Nombre"].ToString()!,
                    ImagenPath = reader["Imagen"].ToString()!,
                    Precio = Convert.ToDecimal(reader["Precio"]),
                    NombreCategoria = reader["Nombre_Categoria"].ToString()!,
                    NombreMarca = reader["Nombre_Marca"].ToString()!
                };
            }
            return producto;
        }

        public static string Encrypt(string data)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            StringBuilder builder = new();
            for(int i=0; i<bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("X2"));
            }
            return builder.ToString();
        }
    }
}
