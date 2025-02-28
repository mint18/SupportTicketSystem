using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence;

public class TicketsDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Status> Statuses { get; set; }

    public TicketsDbContext(DbContextOptions<TicketsDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>()
            .HasOne(e => e.Status)
            .WithMany()
            .HasForeignKey("StatusId");

        modelBuilder.Entity<Ticket>()
            .HasMany(e => e.Comments)
            .WithOne(c => c.Ticket)
            .HasForeignKey("TicketId");

        modelBuilder.Entity<Comment>()
            .HasOne(e => e.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey("TicketId");
    }
}