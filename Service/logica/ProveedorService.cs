using examenOptativoP.Modelos;
using Repository.Data.Proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.logica
{
    public class ProveedorService : IProveedorRepository
    {
        private ProveedorRepository proveedorRepository;
        public ProveedorService(string conexionString)
        {
            proveedorRepository = new ProveedorRepository(conexionString);
        }

        public bool add(ProveedorModel proveedor)
        {
            return validarDatos(proveedor) ? proveedorRepository.add(proveedor) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ProveedorModel> GetAll()
        {
            return proveedorRepository.GetAll();
        }

        public bool delete(int id)
        {
            return id > 0 ? proveedorRepository.delete(id) : false;
        }


        public bool update(ProveedorModel proveedorModel)
        {
            return validarDatos(proveedorModel) ? proveedorRepository.update(proveedorModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ProveedorModel proveedor)
        {
            if(string.IsNullOrEmpty(proveedor.razon_social) || proveedor.razon_social.Length < 3)
    {
                return false;
            }

            if (string.IsNullOrEmpty(proveedor.tipo_documento) || proveedor.tipo_documento.Length < 3)
            {
                return false;
            }

            if (string.IsNullOrEmpty(proveedor.numero_documento) || proveedor.numero_documento.Length < 3)
            {
                return false;
            }

            if (!Regex.IsMatch(proveedor.celular, @"^\d{10}$"))
            {
                return false;
            }

            return true;

        }

        public ProveedorModel get(int id)
        {
            return proveedorRepository.get(id);
        }
    }
}
