using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;

namespace NotesAPI.Data;

// Inherit from DbContext so EF knows this is our database bridge
public class NotesDbContext : DbContext
{
    // The constructor passes configuration (like the database file location) to the base engine
    public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
    {
    }

    // THIS IS THE MAGIC: 
    // This tells EF Core to create a SQL table called "Notes" based on your C# Note model
    public DbSet<Note> Notes { get; set; }
}