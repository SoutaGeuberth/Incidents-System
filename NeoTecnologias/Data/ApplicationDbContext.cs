using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NeoTecnologias.Models;


namespace NeoTecnologias.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación: Una incidencia tiene un usuario que la reporta (UsuarioId)
            modelBuilder.Entity<Ticket>()
                .HasRequired(i => i.User)
                .WithMany(u => u.ReportedTickets)
                .HasForeignKey(i => i.UserId)
                .WillCascadeOnDelete(false);

            // Relación: Una incidencia tiene un técnico asignado (TecnicoId)
            modelBuilder.Entity<Ticket>()
                .HasRequired(t => t.Technician)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(i => i.TechnicianId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId)
                .WillCascadeOnDelete(true);
        }
    }


}
