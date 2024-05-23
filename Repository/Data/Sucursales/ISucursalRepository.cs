using examenOptativoP.Modelos;
using Repository.Data.Facturas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Sucursales
{
    public interface ISucursalRepository
    { 
    bool add(SucursalModel sucursalModel);
    bool update(SucursalModel sucursalModel);
    bool delete(int id);
    SucursalModel get(int id);
    IEnumerable<SucursalModel> GetAll();
    
    }
}
