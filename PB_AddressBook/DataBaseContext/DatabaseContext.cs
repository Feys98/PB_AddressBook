using Microsoft.EntityFrameworkCore;
using PB_AddressBook.Model;

namespace PB_AddressBook.DataBaseContext;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("AddressBookDb");
    }

    public DbSet<AddressBook> Address { get; set; } = null!;
}