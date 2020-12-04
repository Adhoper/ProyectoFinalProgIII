using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalProgIII.Data;
using ProyectoFinalProgIII.Models;
using ProyectoFinalProgIII.VIewModels;

namespace ProyectoFinalProgIII.Controllers
{
    [Authorize]
    public class ReportesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reportes/ProdList
        public ActionResult ProdList()
        {
            return View(_context.Productos);
        }

        // GET: Reportes/ListVendidos
        public ActionResult ListVendidos()
        {
            return View(_context.Productos);
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ProductosMasVendidos()
        {
            var result = (await _context.Facturacion.Where(f => f.UsuarioId == Models.UtilityModel.UserId).Select(s => new FacturaListVM
            {
                Cantidad = s.Cantidad,
                FacturacionId = s.FacturacionId,
                Itbis = s.Itbis,
                Precio = s.Precio,
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
        // GET: Reportes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reportes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reportes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}