using ProyectoFinalProgIII.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalProgIII.VIewModels
{
    public class FacturaListVM
    {
        public Guid FacturacionId { get; set; }
        public string TipoFactura { get; set; }
        public string Cantidad { get; set; }

        public decimal Itbis { get; set; }
        public decimal Precio { get; set; }
        public string NombreCliente { get; set; }
        public Guid ClientesId { get; set; }
        public Guid ProductosId { get; set; }
        public string NombreUsuario { get; set; }

        public string NombreProducto { get; set; }
        public string NombreServicio { get; set; }
        public IEnumerable<Facturacion> Facturaciones { get; set; }
    }
}
