using ProyectoFinalProgIII.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalProgIII.Services
{
    public interface Interfaces
    {
        IEnumerable<Facturacion> GetMostSold();
    }
}
