using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Repository.Data.ConfiguracionesDB
{
    public class ConexionDB
    {
        private string conexionString;

        public ConexionDB(string _conexionString)
        {
            this.conexionString = _conexionString;
        }

        public NpgsqlConnection OpenConnection()
        {
            try
            {
                var conn = new NpgsqlConnection(conexionString);
                conn.Open();
                return conn;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
