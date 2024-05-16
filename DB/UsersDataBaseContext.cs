using Microsoft.EntityFrameworkCore;
using Usersitos.Models;

namespace Usersitos.Data
{
  public class UsersDataBaseContext : DbContext
  {
    public UsersDataBaseContext(DbContextOptions<UsersDataBaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

  }

}