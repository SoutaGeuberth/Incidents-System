using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using NeoTecnologias.Data;
using NeoTecnologias.Models;
using PagedList;
public class TicketsController : Controller
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    // Listar Tickets
    public ActionResult Index(string search, string status, string priority, int? page)
    {
        var incidents = _context.Tickets
            .Include(i => i.User)
            .Include(i => i.Technician)
            .AsQueryable();

        // Filtrar por estado
        if (!string.IsNullOrEmpty(status))
        {
            incidents = incidents.Where(i => i.Status == status);
        }

        // Filtrar por prioridad
        if (!string.IsNullOrEmpty(priority))
        {
            incidents = incidents.Where(i => i.Priority == priority);
        }

        // Búsqueda por título
        if (!string.IsNullOrEmpty(search))
        {
            incidents = incidents.Where(i => i.Title.Contains(search));
        }

        // Ordenar por fecha y paginar
        int pageSize = 5;
        int pageNumber = (page ?? 1);
        return View(incidents.OrderByDescending(i => i.CreationDate).ToPagedList(pageNumber, pageSize));
    }

    // Crear ticket (GET)
    public ActionResult Create()
    {
        ViewBag.Users = new SelectList(_context.Users.Where(u => !u.IsTechnician), "Id", "Name");
        ViewBag.Technicians = new SelectList(_context.Users.Where(u => u.IsTechnician), "Id", "Name");
        return View();
    }

    // Crear ticket (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Ticket ticket)
    {
        if (ModelState.IsValid)
        {
            ticket.CreationDate = DateTime.Now;
            ticket.LastModifiedDate = DateTime.Now;
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Users = new SelectList(_context.Users.Where(u => !u.IsTechnician), "Id", "Name");
        ViewBag.Technicians = new SelectList(_context.Users.Where(u => u.IsTechnician), "Id", "Name");
        return View(ticket);
    }

    // Editar ticket (GET)
    public ActionResult Edit(int id)
    {
        var ticket = _context.Tickets.Find(id);
        if (ticket == null) return HttpNotFound();

        ViewBag.Users = new SelectList(_context.Users.Where(u => !u.IsTechnician), "Id", "Name");
        ViewBag.Technicians = new SelectList(_context.Users.Where(u => u.IsTechnician), "Id", "Name");
        return View(ticket);
    }

    // Editar ticket (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Ticket ticket)
    {
        if (!_context.Users.Any(u => u.Id == ticket.TechnicianId && u.IsTechnician))
        {
            ModelState.AddModelError("TechnicianId", "El técnico asignado no es válido.");
        }
        if (ModelState.IsValid)
        {
            ticket.LastModifiedDate = DateTime.Now;
            _context.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Users = new SelectList(_context.Users.Where(u => !u.IsTechnician), "Id", "Name");
        ViewBag.Technicians = new SelectList(_context.Users.Where(u => u.IsTechnician), "Id", "Name");

        return View(ticket);
    }

    // Eliminar ticket (GET)
    public ActionResult Delete(int id)
    {
        var ticket = _context.Tickets.Find(id);
        if (ticket == null) return HttpNotFound();
        return View(ticket);
    }

    // Eliminar ticket (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var ticket = _context.Tickets.Find(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var ticket = _context.Tickets
                                 .Include("Comments") // Carga los comentarios
                                 .Include(i => i.User)
                                 .Include(i => i.Technician)
                                 .FirstOrDefault(i => i.Id == id);
        if (ticket == null) return HttpNotFound();
        return View(ticket);
    }

}
