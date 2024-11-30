using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class ApplicationDbContext : DbContext
{
    // Add constructor to accept DbContextOptions
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Election> Elections { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<VoteSlip> VoteSlips { get; set; }

    // OnConfiguring is no longer needed when using AddDbContext
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlite("Data Source=ElectEdDb.db");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add custom configurations if necessary
    }
}
