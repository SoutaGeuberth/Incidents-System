using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoTecnologias.Data;
using System.Web.Mvc;
using NeoTecnologias.Models;


namespace NeoTecnologias.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public JsonResult AddComment(int ticketId, string text)
        {
            var comment = new Comment
            {
                TicketId = ticketId,
                Text = text,
                CreationDate = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Json(new { success = true, text, creationDate = comment.CreationDate.ToString("dd/MM/yyyy HH:mm") });
        }
    }
}