using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}
// in command pallet, run '>SQLite: Open Database' to open SQLITE EXPLORER and see the tables