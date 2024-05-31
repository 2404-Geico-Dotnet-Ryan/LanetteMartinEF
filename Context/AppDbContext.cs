using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Visit> Visits { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        /* Read in connection string */ 
        string connectionString = File.ReadAllText(@"C:\Users\U59A24\Desktop\KittyCityVet-DB.txt");
        /* Will connect to a Microsoft SQL Server */
        optionsBuilder.UseSqlServer(connectionString); 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Setting for Person table */
        modelBuilder.Entity<Person>()
        .HasKey(pr => pr.PersonId);
    
        /* Setting for Pet table */
        modelBuilder.Entity<Pet>()
        .HasKey(p=> p.PetId); 

        /* Setting for Visit table */
        modelBuilder.Entity<Visit>()
        .HasKey(v=> v.VisitId); 
    }
}