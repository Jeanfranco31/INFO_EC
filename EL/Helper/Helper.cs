using EL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Helper
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


    }
}
