using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.ConfiguracionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Repository.Data.Productos
{
    public class ProductoRepository : IProductoRepository
    {
        IDbConnection connection;
        private string? conexionString;

        public ProductoRepository(string conexionString)
        {
            this.conexionString = conexionString;
            connection = new ConexionDB(conexionString).OpenConnection();
        }

        public bool add(ProductoModel productoModel)
        {
            try
            {
                connection.Execute("INSERT INTO productos(descripcion, cantidad_minima, cantidad_stock, precio_compra, precio_venta, categoria, marca, estado) " +
                    $"Values(@descripcion, @cantidad_minima, @cantidad_stock, @precio_compra ,@precio_venta, @categoria, @marca, @estado)", productoModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProductoModel> GetAll()
        {
            try
            {
                return connection.Query<ProductoModel>("SELECT * FROM productos");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool delete(int id)
        {
            try
            {
                connection.Execute("DELETE FROM productos WHERE id = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool update(ProductoModel productoModel)
        {
            try
            {
                connection.Execute("UPDATE productos SET " +
                    " descripcion=@descripcion, " +
                    " cantidad_minima=@cantidad_minima, " +
                    " cantidad_stock=@cantidad_stock, " +
                    " precio_compra=@precio_compra, " +
                    " precio_venta=@precio_venta, " +
                    " categoria=@categoria, " +
                    " marca=@marca, " +
                    " estado=@estado " +
                    $" WHERE  id = @id", productoModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductoModel get(int id)
        {
            using (var connection = new NpgsqlConnection(conexionString))
            {
                connection.Open();

                string query = "SELECT * FROM productos WHERE id = @Id";
                var productos = connection.QueryFirstOrDefault<ProductoModel>(query, new { Id = id });

                return productos;
            }
        }
    }
}
