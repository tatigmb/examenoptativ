using examenOptativoP.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Pedidos
{
    public interface IPedidoRepository
    {
        bool add(PedidoModel pedidoModel);
        bool update(PedidoModel pedidoModel);
        bool delete(int id);
        PedidoModel get(int id);
        IEnumerable<PedidoModel> GetAll();
    }
}
