using ProyectoFinalProgIII.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalProgIII.Services
{
    public class IServices : Interfaces
    {
        public ApplicationDbContext _context;
        public IServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Facturacion> GetMostSold()
        {
            return _context.Facturacion.GroupBy(x => new
            {
                x.ProductosId,
                x.Precio
            })
            .Select(p => new Facturacion
            {
                ProductosId = p.Key.ProductosId,
                Precio = p.Key.Precio
            })
            .OrderByDescending(p => p.Precio);
      
        }
    }
}
