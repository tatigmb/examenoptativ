using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.Clientes;
using Repository.Data.ConfiguracionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Sucursales
{
    public class SucursalRepository : ISucursalRepository
    {
        IDbConnection connection;
        private string? conexionString;

        public SucursalRepository(string conexionString)
        {
            this.conexionString = conexionString;
            connection = new ConexionDB(conexionString).OpenConnection();
        }

        public bool add(SucursalModel sucursalModel)
        {
            try
            {
                connection.Execute("INSERT INTO sucursal(descripcion, direccion, telefono, whatsapp, email, estado) " +
                    $"Values(@descripcion, @direccion, @telefono, @whatsapp, @email, @estado)", sucursalModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SucursalModel> GetAll()
        {
            try
            {
                return connection.Query<SucursalModel>("SELECT * FROM sucursal");
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
                connection.Execute("DELETE FROM sucursal WHERE id = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool update(SucursalModel sucursalModel)
        {
            try
            {
                connection.Execute("UPDATE sucursal SET " +
                    " Descripcion=@descripcion, " +
                    " Direccion=@direccion, " +
                    " Telefono=@telefono, " +
                    " Whatsapp=@whatsapp, " +
                    " email=@email, " +   
                    " estado=@estado " +
                    $" WHERE  id = @id", sucursalModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SucursalModel get(int id)
        {
            using (var connection = new NpgsqlConnection(conexionString))
            {
                connection.Open();

                string query = "SELECT * FROM sucursal WHERE id = @Id";
                var sucursal = connection.QueryFirstOrDefault<SucursalModel>(query, new { Id = id });

                return sucursal;
            }
        }


    }
}

