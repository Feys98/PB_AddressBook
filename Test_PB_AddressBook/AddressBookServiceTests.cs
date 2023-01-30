using Microsoft.EntityFrameworkCore;
using PB_AddressBook.DataBaseContext;
using PB_AddressBook.Services;

namespace PB_AddressBook.Tests;
public class AddressBookServiceTests: IDisposable
{
    private readonly DatabaseContext _context;
    public AddressBookServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "TestAddressBook")
            .Options;

        _context = new DatabaseContext(options);
        _context.Database.EnsureCreated();

    }

    [Fact]
    public async Task Test_Create_CreateAddressBook()
    {
        // Arrange
        await _context.Address.AddRangeAsync(TestData.GetAddressBookTestData());
        await _context.SaveChangesAsync();

        var addressBookService = new AddressBookService(_context);
        var expectedData = TestData.GetAddressBookTestData();

        // Act
        var result = await addressBookService.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Should().HaveCount(expectedData.Count);
        result.Should().BeEquivalentTo(expectedData);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
