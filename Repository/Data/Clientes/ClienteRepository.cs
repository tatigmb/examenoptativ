using Dapper;
using Repository.Data.ConfiguracionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Clientes
{
    public class ClienteRepository : IClienteRepository
    {
        IDbConnection connection;

        public ClienteRepository(string conexionString)
        {
            connection = new ConexionDB(conexionString).OpenConnection();
        }

        public bool add(ClienteModel clienteModel)
        {
            try
            {
                connection.Execute("INSERT INTO cliente(id_banco, nombre, apellido, documento, direccion, mail, celular, estado) " +
                    $"Values(@idBanco, @nom, @ape, @docu, @direc, @email, @celu, @estad)", clienteModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            return connection.Query<ClienteModel>("SELECT * FROM cliente");
        }

        public bool delete(int id)
        {
            connection.Execute($"DELETE FROM cliente WHERE id = {id}");
            return true;
        }

        public bool update(ClienteModel clienteModel)
        {
            try
            {
                connection.Execute("UPDATE cliente SET " +
                    " id_banco=@idBanco, " +
                    " nombre=@nom, " +
                    " apellido=@ape, " +
                    " documento=@docu, " +
                    " direccion=@direc, " +
                    " mail=@email, " +
                    " celular=@celu, " +
                    " estado=@estad " +
                    $" WHERE  id = @id", clienteModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool get(int id) 
        {
            return true;
        }
    }
}

