using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoTecnologias.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Status { get; set; }

        public string Priority { get; set; }

        public DateTime CreationDate  { get; set; } = DateTime.Now;

        public DateTime LastModifiedDate { get; set; } = DateTime.Now;

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        // Usuario que reporta la incidencia
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Técnico asignado
        public int TechnicianId { get; set; }
        public virtual User Technician { get; set; }



    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsTechnician { get; set; } 

        // Relación con Incidencias
        public virtual List<Ticket> ReportedTickets { get; set; }
        public virtual List<Ticket> AssignedTickets { get; set; }
    }

    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}