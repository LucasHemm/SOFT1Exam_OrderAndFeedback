using Microsoft.EntityFrameworkCore;
using OrderAndFeedbackService.Models;

namespace OrderAndFeedbackService;

public class ApplicationDbContext : DbContext
{

    public DbSet<Order> Orders { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    // Constructor that accepts DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure SQL Server if no options are provided (to avoid overriding options in tests)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=OrderServiceDB;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True;");
        }
    }
}