using EL.DTO;
using EL.Messages;
using EL.Response;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Producto
{
    public class ProductService : IProducto
    {
        private string _connection;
        private Response response = new();
        private ProductoDto productoDTO;
        private List<ProductoDto> productsList = new();

        public ProductService(IConfiguration configuration) => _connection = configuration.GetConnectionString(Common.ADOquery.cadenaConexion)!;


        public async Task<Response> getAllProducts()
        {
            using (SqlConnection conn = new(_connection)) 
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_GET_PRODUCTS, conn);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    
                    while(await reader.ReadAsync())
                    {
                        productoDTO = Helper.Helper.GenerateReaderProduct(reader);        
                    }
                    productsList.Add(productoDTO);
                    response.Data = productsList;
                    response.message = Message.productoObtenido;
                }
                catch(Exception ex)
                {
                    response.Data = string.Empty;
                    response.message = ex.Message;
                }
                finally 
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> getProductById(int id)
        {
            using (SqlConnection conn = new(_connection)) 
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_GET_PRODUCT_BY_ID, conn);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.param_IdProducto, SqlDbType.Int) { Value = id });

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync()) 
                    {
                        productoDTO = Helper.Helper.GenerateReaderProduct(reader);
                        if(productoDTO != null)
                        {
                            response.Data = productoDTO;
                            response.message = Message.productExists;
                        }
                    }
                    else
                    {
                        response.Data = string.Empty;
                        response.message = Message.productNotExists;
                    }                   
                }
                catch(Exception ex)
                {
                    response.Data = string.Empty;
                    response.message = ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }


        public async Task<Response> addProduct(ProductoDto product)
        {
            using (SqlConnection conn = new(_connection))
            {
                try 
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_ADD_PRODUCT,conn);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.param_NombreProducto, SqlDbType.VarChar) { Value = product.Nombre});
                    command.Parameters.Add(new SqlParameter(Common.Parameters.paramImagenPath, SqlDbType.VarChar) {Value = product.ImagenPath});
                    command.Parameters.Add(new SqlParameter(Common.Parameters.paramPrecio, SqlDbType.Decimal) {Value = product.Precio });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.paramNombreCategoria, SqlDbType.VarChar) {Value = product.NombreCategoria });
                    command.Parameters.Add(new SqlParameter(Common.Parameters.paramNombreMarca, SqlDbType.VarChar) {Value = product.NombreMarca });

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if(await reader.ReadAsync()) 
                    {
                        response.Data = Helper.Helper.GenerateReaderProduct(reader);
                        response.message = "Producto agregado correctamente";
                    }
                }
                catch(Exception ex)
                {
                    response.Data = string.Empty;
                    response.message = "Ocurrio un error al agregar el producto, "+ex.ToString();
                }
                finally { await conn.CloseAsync(); }
            }
            return response;
        }

        public async Task<Response> removeProduct(int id)
        {
            using (SqlConnection conn = new(_connection)) 
            {
                try
                {
                    await conn.OpenAsync();
                    SqlCommand command = new(Common.ADOquery.SP_DELETE_PRODUCT,conn);
                    command.Parameters.Add(new SqlParameter(Common.Parameters.param_IdProducto, SqlDbType.Int) { Value = id });

                    int row = await command.ExecuteNonQueryAsync();

                    if(row > 0) 
                    {
                        response.Data = string.Empty;
                        response.message = "Producto eliminado";
                    }
                }
                catch(Exception ex)
                {
                    response.Data = string.Empty;
                    response.message = "Ocurrio un error al tratar de eliminar el dato. " + ex.Message;
                }
                finally
                {
                    await conn.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> updateProduct(ProductoDto product)
        {
            return response;
        }


    }
}
