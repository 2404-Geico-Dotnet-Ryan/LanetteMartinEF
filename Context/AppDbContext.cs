using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
class AppDbContext : DbContext
{
    // We provide the models through the DbSet<'modle name'> fields
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

    // We have to override the default behavior to ensure the foreign key constraint is established.
    // If we do not do this then the columns will still be create but the constraints will not be enforced

    /*********************/
    /* need to redo this now that I have good example of setting up forgien keys! :) 
    /*********************/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Settings for Person table */
        /* As a one to many "1 Person to Many Pets" relationship with Pets table */
        modelBuilder.Entity<Person>()
        .HasMany(pr => pr.Pets)
        .WithOne(pt => pt.Person)
        .HasForeignKey(pt => pt.PersonId);

        /* Settings for Pet table */
        /* As a one to many "1 Pet to Many Visits" relationship with Visits table */
        modelBuilder.Entity<Pet>()
        .HasMany(pt => pt.Visits)
        .WithOne(vt => vt.Pet)
        .HasForeignKey(vt => vt.PetId);
    } 
}