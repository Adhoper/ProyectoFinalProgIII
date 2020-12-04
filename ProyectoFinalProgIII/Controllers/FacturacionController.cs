using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalProgIII.Data;
using ProyectoFinalProgIII.Models;
using ProyectoFinalProgIII.VIewModels;
using Rotativa.AspNetCore;

namespace ProyectoFinalProgIII.Controllers
{
    [Authorize]
    public class FacturacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Facturacion
        public async Task<IActionResult> Index()
        {
            var result = (await _context.Facturacion.Where(f => f.UsuarioId == Models.UtilityModel.UserId).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                NombreCliente = _context.Clientes.Where(c => c.ClienteId == s.ClienteId).FirstOrDefault().Nombre,
                NombreProducto = _context.Productos.Where(c => c.ProductosId == s.ProductosId).FirstOrDefault().NombreP,
                NombreServicio = _context.Servicios.Where(c => c.ServiciosId == s.ServiciosId).FirstOrDefault().NombreS,
                NombreUsuario = _context.Usuarios.Where(c => c.Id.Equals(UtilityModel.UserId.ToString())).FirstOrDefault().Nombre,
                TipoFactura = s.TipoFactura

            }).ToListAsync());

            if (TempData["FacturaType"] == "Productos")
            {
                TempData["FacturaType"] = "Servicios";
            }
            else
            {
                TempData["FacturaType"] = "Productos";
            }

            //if (result.Count>0)
            //{
            return View(result);

            //}
            //else{
            //    return View(new List<FacturaVM>());
            //}

        }

        public IActionResult ChangeFacturaType([Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId,UsuarioId,ProductosId,ServiciosId")] Facturacion facturacion)
        {
            

            var result = new FacturaVM
            {
                Productos = _context.Productos.ToList(),
                Clientes = _context.Clientes.ToList(),
                Servicios = _context.Servicios.ToList(),
                Facturacion = facturacion
            };


            return RedirectToAction("Create", result);
        }

        // GET: Facturacion/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var facturacion = (await _context.Facturacion.Where(f => f.FacturacionId == id).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                NombreCliente = _context.Clientes.Where(c => c.ClienteId == s.ClienteId).FirstOrDefault().Nombre,
                NombreProducto = _context.Productos.Where(c => c.ProductosId == s.ProductosId).FirstOrDefault().NombreP,
                NombreServicio = _context.Servicios.Where(c => c.ServiciosId == s.ServiciosId).FirstOrDefault().NombreS,
                NombreUsuario = _context.Usuarios.Where(c => c.Id.Equals(UtilityModel.UserId.ToString())).FirstOrDefault().Nombre,
                TipoFactura = s.TipoFactura

            }).ToListAsync());

            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // GET: Facturacion/PDF/5
        public async Task<IActionResult> PDF(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var facturacion = (await _context.Facturacion.Where(f => f.FacturacionId == id).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                NombreCliente = _context.Clientes.Where(c => c.ClienteId == s.ClienteId).FirstOrDefault().Nombre,
                NombreProducto = _context.Productos.Where(c => c.ProductosId == s.ProductosId).FirstOrDefault().NombreP,
                NombreServicio = _context.Servicios.Where(c => c.ServiciosId == s.ServiciosId).FirstOrDefault().NombreS,
                NombreUsuario = _context.Usuarios.Where(c => c.Id.Equals(UtilityModel.UserId.ToString())).FirstOrDefault().Nombre,
                TipoFactura = s.TipoFactura

            }).ToListAsync());

            if (facturacion == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("PDF", facturacion)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.Legal
            };
        }

        // GET: Facturacion/Create
        public IActionResult Create(FacturaVM fvm)
        {
            var result = new FacturaVM
            {
                Productos = _context.Productos.ToList(),
                Clientes = _context.Clientes.ToList(),
                Servicios = _context.Servicios.ToList(),
                Facturacion = fvm.Facturacion,
            };
            if(result.Facturacion == null)
            {
                result.Facturacion = new Facturacion();
                result.Facturacion.TipoFactura = "Productos";
            }
            return View(result);
        }

        // POST: Facturacion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId,UsuarioId,ProductosId,ServiciosId")] Facturacion facturacion)
        {
            if (ModelState.IsValid)
            {
                var producto = _context.Productos.Where(p => p.ProductosId.Equals(facturacion.ProductosId)).FirstOrDefault();
                if(int.Parse(producto.Cantidad) - int.Parse(facturacion.Cantidad) < 1)
                {
                    return RedirectToAction(nameof(Index));
                }

                facturacion.FacturacionId = Guid.NewGuid();
                    facturacion.UsuarioId = UtilityModel.UserId;
                    _context.Add(facturacion);
                    await _context.SaveChangesAsync();

                producto.Cantidad = (int.Parse(producto.Cantidad) - int.Parse(facturacion.Cantidad)).ToString();
                var servicios = _context.Servicios.Where(p => p.ServiciosId.Equals(facturacion.ServiciosId)).FirstOrDefault();
                var clientes = _context.Clientes.Where(p => p.ClienteId.Equals(facturacion.ClienteId)).FirstOrDefault();

                //Vendidos Productos
                if (producto.CantVendidos == null)
                {
                    producto.CantVendidos = "0";
                    producto.CantVendidos = (int.Parse(producto.CantVendidos) + int.Parse(facturacion.Cantidad)).ToString();
                }
                else
                {
                    producto.CantVendidos = (int.Parse(producto.CantVendidos) + int.Parse(facturacion.Cantidad)).ToString();
                }

                //Vendidos Servicios
                if (servicios.CantVendidos == null)
                {
                    servicios.CantVendidos = "0";
                    servicios.CantVendidos = (int.Parse(servicios.CantVendidos) + int.Parse(facturacion.Cantidad)).ToString();
                }
                else
                {
                    servicios.CantVendidos = (int.Parse(servicios.CantVendidos) + int.Parse(facturacion.Cantidad)).ToString();
                }

                //Dar ITBIS Producto
                facturacion.Itbis = ((int.Parse(facturacion.Cantidad) * int.Parse(producto.Valor))*0.18).ToString();
                // Dar ITBS Servicios
                //facturacion.Itbis = ((int.Parse(facturacion.Cantidad) * int.Parse(servicios.Valor)) * 0.18).ToString();

                //Sumar Comrpas
                if (clientes.CantCompras == null)
                {
                    clientes.CantCompras = "0";
                    clientes.CantCompras = (int.Parse(clientes.CantCompras) + int.Parse("1")).ToString();
                }
                else
                {
                    clientes.CantCompras = (int.Parse(clientes.CantCompras) + int.Parse("1")).ToString();
                }

                _context.Clientes.Update(clientes);
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
    
            }
            return View(facturacion);
        }

        // GET: Facturacion/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = await _context.Facturacion.FindAsync(id);
            if (facturacion == null)
            {
                return NotFound();
            }
            return View(facturacion);
        }

        // POST: Facturacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FacturacionId,TipoFactura,Cantidad,Itbis,ClienteId,UsuarioId,ProductosId,ServiciosId")] Facturacion facturacion)
        {
            if (id != facturacion.FacturacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturacionExists(facturacion.FacturacionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(facturacion);
        }

        // GET: Facturacion/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturacion = (await _context.Facturacion.Where(f => f.FacturacionId == id).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                NombreCliente = _context.Clientes.Where(c => c.ClienteId == s.ClienteId).FirstOrDefault().Nombre,
                NombreProducto = _context.Productos.Where(c => c.ProductosId == s.ProductosId).FirstOrDefault().NombreP,
                NombreServicio = _context.Servicios.Where(c => c.ServiciosId == s.ServiciosId).FirstOrDefault().NombreS,
                NombreUsuario = _context.Usuarios.Where(c => c.Id.Equals(UtilityModel.UserId.ToString())).FirstOrDefault().Nombre,
                TipoFactura = s.TipoFactura

            }).ToListAsync());

            if (facturacion == null)
            {
                return NotFound();
            }

            return View(facturacion);
        }

        // POST: Facturacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var facturacion = await _context.Facturacion.FindAsync(id);
            _context.Facturacion.Remove(facturacion);
            await _context.SaveChangesAsync();

            var producto = _context.Productos.Where(p => p.ProductosId.Equals(facturacion.ProductosId)).FirstOrDefault();
            var servicios = _context.Servicios.Where(p => p.ServiciosId.Equals(facturacion.ServiciosId)).FirstOrDefault();
            var clientes = _context.Clientes.Where(p => p.ClienteId.Equals(facturacion.ClienteId)).FirstOrDefault();
            producto.Cantidad = (int.Parse(producto.Cantidad)+int.Parse(facturacion.Cantidad)).ToString();
            
            //Productos Vendidos Borrar
            if (producto.CantVendidos == null)
            {
                producto.CantVendidos = "0";
                producto.CantVendidos = (int.Parse(producto.CantVendidos) - int.Parse(facturacion.Cantidad)).ToString();
            }
            else
            {
                producto.CantVendidos = (int.Parse(producto.CantVendidos) - int.Parse(facturacion.Cantidad)).ToString();
            }
            //Servicios Vendidos Borrar
            if (servicios.CantVendidos == null)
            {
                servicios.CantVendidos = "0";
                servicios.CantVendidos = (int.Parse(servicios.CantVendidos) - int.Parse(facturacion.Cantidad)).ToString();
            }
            else
            {
                servicios.CantVendidos = (int.Parse(servicios.CantVendidos) - int.Parse(facturacion.Cantidad)).ToString();
            }

            if (clientes.CantCompras == null)
            {
                clientes.CantCompras = "0";
                clientes.CantCompras = (int.Parse(clientes.CantCompras) - int.Parse("1")).ToString();
            }
            else
            {
                clientes.CantCompras = (int.Parse(clientes.CantCompras) - int.Parse("1")).ToString();
            }



            _context.Productos.Update(producto);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool FacturacionExists(Guid id)
        {
            return _context.Facturacion.Any(e => e.FacturacionId == id);
        }
    }
}
