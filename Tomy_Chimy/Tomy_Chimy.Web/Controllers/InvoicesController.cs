using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tomy_Chimy.Web.Data;
using Tomy_Chimy.Web.Models;
using Tomy_Chimy.Web.ViewsModels;

namespace Tomy_Chimy.Web.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoices.Include(i => i.Client).Include(i => i.PayingMethod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.PayingMethod)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            var invoiceView = new InvoiceView();
            var invoiceDetail = new InvoiceDetail();

            invoiceView.Invoice = await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.PayingMethod)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            var dataOD = _context.InvoiceDetails.Include(od => od.Invoice).Include(od => od.Food).Where(od => od.Invoice_ID.Equals(id)).ToList();

            invoiceView.Artículos = dataOD;

            ViewData["ID_Comidas"] = new SelectList(_context.Comidas, "ID_Comidas", "Descripción", invoiceDetail.ID_Comidas);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Invoice_ID", invoiceDetail.Invoice_ID);
            return View(invoiceView);
            
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["ID_User"] = new SelectList(_context.Clients, "ID_User", "Apellidos");
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Invoice_ID,ID_User,Method_Id,FechaFactura,Subtotal,ValorImpuesto,Total")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ID_User"] = new SelectList(_context.Clients, "ID_User", "Apellidos", invoice.ID_User);
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ID_User"] = new SelectList(_context.Clients, "ID_User", "Apellidos", invoice.ID_User);
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Invoice_ID,ID_User,Method_Id,FechaFactura,Subtotal,ValorImpuesto,Total")] Invoice invoice)
        {
            if (id != invoice.Invoice_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Invoice_ID))
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
            ViewData["ID_User"] = new SelectList(_context.Clients, "ID_User", "Apellidos", invoice.ID_User);
            ViewData["Method_Id"] = new SelectList(_context.PayingMethods, "Method_Id", "FormaDePago", invoice.Method_Id);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.PayingMethod)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Invoice_ID == id);
        }


        public async Task<IActionResult> _AdicionarArticulo([Bind("InvoiceDetail_ID,ID_Comidas,Cantidad,ValorUnitario,ValorTotal,Invoice_ID")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetail);

                int id = invoiceDetail.Invoice_ID;

                int ID_Comidas = invoiceDetail.ID_Comidas;

                Models.Food articulos = _context.Comidas.Find(ID_Comidas);

                decimal preciou = articulos.PrecioUnitario;
                decimal cantidad = invoiceDetail.Cantidad;

                decimal preciot = cantidad * preciou;

                invoiceDetail.ValorUnitario = preciou;
                invoiceDetail.ValorTotal = preciot;

                await _context.SaveChangesAsync();
                Models.Invoice invoice = _context.Invoices.Find(id);
                invoice.Subtotal += preciot;
                invoice.ValorImpuesto = 0;
                invoice.Total += preciot;
                articulos.Cantidad += invoiceDetail.Cantidad;
                _context.Update(articulos);
                _context.SaveChanges();


                return RedirectToAction("Details", new { id = id });
            }
            ViewData["ID_Comidas"] = new SelectList(_context.Comidas, "ID_Comidas", "Descripción", invoiceDetail.ID_Comidas);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Invoice_ID", invoiceDetail.Invoice_ID);
            return View(invoiceDetail);
        }

        public async Task<IActionResult> InvoicePDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Invoices
                .Include(o => o.Client)
                .Include(o => o.PayingMethod)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            if (orden == null)
            {
                return NotFound();
            }
            var InvoiceView = new InvoiceView();
            var InvoiceDetail = new InvoiceDetail();

            InvoiceView.Invoice = await _context.Invoices
                .Include(o => o.Client)
                .Include(o => o.PayingMethod)
                .FirstOrDefaultAsync(m => m.Invoice_ID == id);
            var dataOD = _context.InvoiceDetails.Include(od => od.Invoice).Include(od => od.Food).Where(od => od.Invoice_ID.Equals(id)).ToList();

            InvoiceView.Artículos = dataOD;

            ViewData["PayingMethod"] = new SelectList(_context.Invoices, "PayingMethod", "PayingMethod", InvoiceDetail.Invoice_ID);
            ViewData["Invoice_ID"] = new SelectList(_context.Invoices, "Invoice_ID", "Invoice_ID", InvoiceDetail.Invoice_ID);
            ViewData["ID_Comidas"] = new SelectList(_context.Comidas, "ID_Comidas", "Descripción", InvoiceDetail.ID_Comidas);


            return View(InvoiceView);
        }
    }
}
