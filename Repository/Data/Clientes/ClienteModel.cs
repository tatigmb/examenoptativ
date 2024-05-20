using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Clientes
{
    public class ClienteModel
    {
        public int id_cliente { get; set; }
        public int idBanco { get; set; }
        public string nom { get; set; }
        public string ape { get; set;}
        public string docu { get; set; }
        public string direc { get; set; }
        public string email { get; set;}
        public string celu { get; set; }
        public string estad { get; set; }
    }
}
