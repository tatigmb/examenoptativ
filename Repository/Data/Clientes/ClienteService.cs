using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Clientes
{
    public class ClienteService: IClienteRepository
    {
        private ClienteRepository clienteRepository;
        public ClienteService(string conexionString)
        {
            clienteRepository = new ClienteRepository(conexionString);
        }

        public bool add(ClienteModel cliente)
        {
            return validarDatos(cliente) ? clienteRepository.add(cliente) : throw new Exception("Error en la validación de datos, corroborar");
        }

        public IEnumerable<ClienteModel> GetAll()
        {
            return clienteRepository.GetAll();
        }

        public bool delete(int id)
        {
            return id > 0 ? clienteRepository.delete(id) : false;
        }


        public bool update(ClienteModel clienteModel)
        {
            return validarDatos(clienteModel) ? clienteRepository.update(clienteModel) : throw new Exception("Error en la validación de datos, corroborar");
        }

        private bool validarDatos(ClienteModel cliente)
        {
            if (cliente == null)
                return false;
            if (string.IsNullOrEmpty(cliente.nom))
                return false;
            if (string.IsNullOrEmpty(cliente.ape) && cliente.nom.Length < 2)
                return false;
            if (string.IsNullOrEmpty(cliente.docu))
                return false;

            return true;
        }
        public bool get(int id)
        {
            return true;
        }
    }
}

