using Dapper;
using examenOptativoP.Modelos;
using Npgsql;
using Repository.Data.ConfiguracionesDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Pedidos
{
    public class PedidoRepository : IPedidoRepository
    {
        IDbConnection connection;
        private string? conexionString;

        public PedidoRepository(string conexionString)
        {
            this.conexionString = conexionString;
            connection = new ConexionDB(conexionString).OpenConnection();
        }

        public bool add(PedidoModel pedidoModel)
        {
            try
            {
                var queryPedidoCompra = @"INSERT INTO pedido_compra(id_proveedor, id_sucursal, fecha_hora, total) 
                                          VALUES(@id_proveedor, @id_sucursal, @fecha_hora, @total) RETURNING id";

                var id_pedido = connection.QuerySingle<int>(queryPedidoCompra, new
                {
                    pedidoModel.id_proveedor,
                    pedidoModel.id_sucursal,
                    pedidoModel.fecha_hora,
                    pedidoModel.total
                });

                foreach (var detalle in pedidoModel.detalle_pedido)
                {
                    connection.Execute("INSERT INTO detalle_pedido(id_pedido, id_producto, cantidad_producto, subtotal) " +
                        "VALUES(@id_pedido, @id_producto, @cantidad_producto, @subtotal)", new
                        {
                            id_pedido = id_pedido,
                            detalle.id_producto,
                            detalle.cantidad_producto,
                            detalle.subtotal
                        });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<PedidoModel> GetAll()
        {
            try
            {
                var pedidoCompraDictionary = new Dictionary<int, PedidoModel>();
                var query = @"SELECT 
                              pc.id, pc.id_proveedor, pc.id_sucursal, pc.fecha_hora, pc.total,
                              dp.id AS id_detalle, dp.id_producto, dp.cantidad_producto, dp.subtotal
                              FROM pedido_compra pc
                              LEFT JOIN detalle_pedido dp ON pc.id = dp.id_pedido";

                var pedidoCompra = connection.Query<PedidoModel, DetallePedidoModel, PedidoModel>(query, (pedidoCompra, detalle_pedido) =>
                {
                    if (!pedidoCompraDictionary.TryGetValue(pedidoCompra.id, out var pedidoCompraActual))
                    {
                        pedidoCompraActual = pedidoCompra;
                        pedidoCompraActual.detalle_pedido = new List<DetallePedidoModel>();
                        pedidoCompraDictionary.Add(pedidoCompraActual.id, pedidoCompraActual);
                    }

                    if (detalle_pedido != null)
                    {
                        pedidoCompraActual.detalle_pedido.Add(detalle_pedido);
                    }

                    return pedidoCompraActual;
                }, splitOn: "id_detalle");

                return pedidoCompra.Distinct();
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
                connection.Execute("DELETE FROM detalle_pedido WHERE id_pedido = @id", new { Id = id });
                connection.Execute("DELETE FROM pedido_compra WHERE id = @id", new { Id = id });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool update(PedidoModel pedidoModel)
        {
            try
            {
                var queryPedidoCompra = @"UPDATE pedido_compra SET 
                                          id_proveedor=@id_proveedor, 
                                          id_sucursal=@id_sucursal,
                                          fecha_hora=@fecha_hora, 
                                          total=@total 
                                          WHERE id = @id";

                var queryDetallePedido = @"UPDATE detalle_pedido SET
                                           id_producto=@id_producto,
                                           cantidad_producto=@cantidad_producto,
                                           subtotal=@subtotal 
                                           WHERE id = @id";

                connection.Execute(queryPedidoCompra, pedidoModel);

                foreach (var detalle in pedidoModel.detalle_pedido)
                {
                    connection.Execute(queryDetallePedido, detalle);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PedidoModel get(int id)
        {
            using (var connection = new NpgsqlConnection(conexionString))
            {
                connection.Open();

                string query = @"SELECT 
                                 pc.id, pc.id_proveedor, pc.id_sucursal, pc.fecha_hora, pc.total,
                                 dp.id AS id_detalle, dp.id_producto, dp.cantidad_producto, dp.subtotal
                                 FROM pedido_compra pc
                                 LEFT JOIN detalle_pedido dp ON pc.id = dp.id_pedido
                                 WHERE pc.id = @Id";

                var pedidoCompraDictionary = new Dictionary<int, PedidoModel>();

                var pedidoCompra = connection.Query<PedidoModel, DetallePedidoModel, PedidoModel>(query, (pedidoCompra, detalle_pedido) =>
                {
                    if (!pedidoCompraDictionary.TryGetValue(pedidoCompra.id, out var pedidoCompraActual))
                    {
                        pedidoCompraActual = pedidoCompra;
                        pedidoCompraActual.detalle_pedido = new List<DetallePedidoModel>();
                        pedidoCompraDictionary.Add(pedidoCompraActual.id, pedidoCompraActual);
                    }

                    if (detalle_pedido != null)
                    {
                        pedidoCompraActual.detalle_pedido.Add(detalle_pedido);
                    }

                    return pedidoCompraActual;
                }, new { Id = id }, splitOn: "id_detalle").FirstOrDefault();

                return pedidoCompra;
            }
        }
    }
}
