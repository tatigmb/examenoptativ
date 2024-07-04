using examenOptativoP.Modelos;
using Repository.Data.Pedidos;
using Repository.Data.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.logica
{
    public class PedidoService : IPedidoRepository
    {
        private PedidoRepository pedidoRepository;
        public PedidoService(string conexionString)
        {
            pedidoRepository = new PedidoRepository(conexionString);
        }

        public bool add(PedidoModel pedido_compra)
        {
            return validarDatos(pedido_compra) ? pedidoRepository.add(pedido_compra) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<PedidoModel> GetAll()
        {
            return pedidoRepository.GetAll();
        }

        public bool delete(int id)
        {
            return id > 0 ? pedidoRepository.delete(id) : false;
        }


        public bool update(PedidoModel pedidoModel)
        {
            return validarDatos(pedidoModel) ? pedidoRepository.update(pedidoModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(PedidoModel pedido_compra)
        {
            if (pedido_compra == null || pedido_compra.detalle_pedido == null || pedido_compra.detalle_pedido.Count == 0)
                return false;

            if (pedido_compra.id_proveedor <= 0 || pedido_compra.id_sucursal <= 0)
                return false;

            return true;
        }

        public PedidoModel get(int id)
        {
            return pedidoRepository.get(id);
        }
    }
}
