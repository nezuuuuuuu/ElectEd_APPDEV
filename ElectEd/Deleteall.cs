using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public class DatabaseService
{
    private readonly ApplicationDbContext _context;

    public DatabaseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void ClearDatabase()
    {
        var tables = _context.Model.GetEntityTypes().Select(t => t.GetTableName()).ToList();
        foreach (var table in tables)
        {
            _context.Database.ExecuteSqlRaw($"DELETE FROM {table}");
        }
        _context.SaveChanges();
    }
}
