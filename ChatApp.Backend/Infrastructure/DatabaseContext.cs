using ChatApp.Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Backend.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    public DbSet<Message> Messages => Set<Message>();

}