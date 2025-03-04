using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;

public class AppDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;database=wpf_crud_app;user=root;password=",
            new MySqlServerVersion(new Version(8, 0, 33)) // Use your MySQL version
        );
    }
}
