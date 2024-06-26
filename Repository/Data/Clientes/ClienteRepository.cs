﻿using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
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
        private string? conexionString;

        public ClienteRepository(string conexionString)
        {
            this.conexionString = conexionString;
            connection = new ConexionDB(conexionString).OpenConnection();
        }

        public bool add(ClienteModel clienteModel)
        {
            try
            {
                connection.Execute("INSERT INTO cliente(id_banco, nombre, apellido, documento, direccion, mail, celular, estado) " +
                    $"Values(@id_banco, @nombre, @apellido, @documento, @direccion, @mail, @celular, @estado)", clienteModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            try
            {
                return connection.Query<ClienteModel>("SELECT * FROM cliente");
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
                connection.Execute("DELETE FROM cliente WHERE id = @Id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool update(ClienteModel clienteModel)
        {
            try
            {
                connection.Execute("UPDATE cliente SET " +
                    " id_banco=@id_banco, " +
                    " nombre=@nombre, " +
                    " apellido=@apellido, " +
                    " documento=@documento, " +
                    " direccion=@direccion, " +
                    " mail=@mail, " +
                    " celular=@celular, " +
                    " estado=@estado " +
                    $" WHERE  id = @id", clienteModel);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ClienteModel get(int id)
        {
            using (var connection = new NpgsqlConnection(conexionString))
            {
                connection.Open();

                string query = "SELECT * FROM cliente WHERE id = @Id";
                var cliente = connection.QueryFirstOrDefault<ClienteModel>(query, new { Id = id });

                return cliente;
            }
        }


    }
}

