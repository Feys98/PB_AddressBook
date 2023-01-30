using Microsoft.AspNetCore.Mvc;
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
    public async Task Test_GetAll_ShouldGetAllNewAddresses()
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
    [Fact]
    public async Task Test_Create_ShouldCreateNewAddresses()
    {
        // Arrange
        var newData = TestData.GetAddressBookTestData()[0];
        var addressBookService = new AddressBookService(_context);
        var expectedData = TestData.GetAddressBookTestData()[0];

        // Act
        var result = await addressBookService.Create(newData);
        await _context.SaveChangesAsync();
        
        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
