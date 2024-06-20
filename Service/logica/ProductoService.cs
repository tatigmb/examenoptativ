using examenOptativoP.Modelos;
using Repository.Data.Facturas;
using Repository.Data.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.logica
{
    public class ProductoService : IProductoRepository
    {
        private ProductoRepository productoRepository;
        public ProductoService(string conexionString)
        {
            productoRepository = new ProductoRepository(conexionString);
        }

        public bool add(ProductoModel productos)
        {
            return validarDatos(productos) ? productoRepository.add(productos) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ProductoModel> GetAll()
        {
            return productoRepository.GetAll();
        }

        public bool delete(int id)
        {
            return id > 0 ? productoRepository.delete(id) : false;
        }


        public bool update(ProductoModel productoModel)
        {
            return validarDatos(productoModel) ? productoRepository.update(productoModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ProductoModel productos)
        {
            if (string.IsNullOrEmpty(productos.descripcion) || string.IsNullOrEmpty(productos.categoria) || string.IsNullOrEmpty(productos.marca) || string.IsNullOrEmpty(productos.estado) || productos.cantidad_minima == 0 || productos.cantidad_stock == 0 || productos.precio_compra == 0 || productos.precio_venta == 0)

            {
                return false;
            }

            if (productos.cantidad_minima <= 1)
            {
                return false;
            }

            if (productos.precio_compra <= 0 || productos.precio_venta <= 0)
            {
                return false;
            }

            return true;
        }

        public ProductoModel get(int id)
        {
            return productoRepository.get(id);
        }
    }
}
