using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examenOptativoP.Modelos
{
    public class PedidoModel
    {
        public int id { get; set; }
        public int id_proveedor { get; set; }
        public int id_sucursal { get; set; }
        public DateTime fecha_hora { get; set; }
        public int total { get; set; }
        public List<DetallePedidoModel> detalle_pedido { get; set; }
    }
}
