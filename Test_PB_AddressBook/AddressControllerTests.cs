using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PB_AddressBook.Controllers;
using PB_AddressBook.DataBaseContext;
using PB_AddressBook.Services;
using PB_AddressBook.Tests;

namespace Test_PB_AddressBook;

public class AddressControllerTests
{
    [Fact]
    public async Task Test_GetLastAddress_ShouldReturnLastAddress()
    {
        //Arange
        var service = new Mock<IAddressBookService>();
        service.Setup(x => x.GetAll()).ReturnsAsync(TestData.GetAddressBookTestData());
        var addressController = new AddressController(service.Object);

        var expectedData = TestData.GetAddressBookTestData().Last();

        //Act
        var result = await addressController.GetLastAddress() as OkObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeOfType<AddressBook>();

        var resultData = result.Value as AddressBook;
        resultData.Should().NotBeNull();
        resultData.Id.Should().Be(expectedData.Id);
        resultData.Street.Should().Be(expectedData.Street);
        resultData.HouseNumber.Should().Be(expectedData.HouseNumber);
        resultData.City.Should().Be(expectedData.City);
        resultData.Country.Should().Be(expectedData.Country);
        resultData.Zip.Should().Be(expectedData.Zip);
        resultData.Phone.Should().Be(expectedData.Phone);
        resultData.Email.Should().Be(expectedData.Email);
    }

    [Fact]
    public async Task Test_GetLastAddress_ShouldReturnEmptyAddressBooks()
    {
        //Arange
        var service = new Mock<IAddressBookService>();
        service.Setup(x => x.GetAll()).ReturnsAsync(TestData.GetEmptyAddressBookTestData());
        var addressController = new AddressController(service.Object);

        //Act
        var result = await addressController.GetLastAddress() as BadRequestObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<BadRequestObjectResult>();
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Test_GetAddressBook_ShouldReturnAddressBookByCity()
    {
        //Arange
        var service = new Mock<IAddressBookService>();

        var testCity = "TestCity";
        var expectedData = TestData.GetAddressBookTestData().Where(x => x.City == testCity).ToList();

        service.Setup(x => x.GetBy(y => y.City == testCity))
            .ReturnsAsync(TestData.GetAddressBookTestData().Where(x => x.City == testCity).ToList());
        var addressController = new AddressController(service.Object);

        //Act
        var result = await addressController.GetAddressBook(testCity) as OkObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeOfType <List<AddressBook>>();

        var resultData = result.Value as List<AddressBook>;
        resultData.Should().NotBeNull();
        resultData.Count.Should().Be(expectedData.Count);
        resultData.Should().BeEquivalentTo(expectedData);
    }
    [Fact]
    public async Task Test_GetAddressBook_ShouldReturnEmptyAddressBookByCity()
    {
        //Arange
        var service = new Mock<IAddressBookService>();

        var testCity = "no city";
        var expectedData = TestData.GetEmptyAddressBookTestData();

        service.Setup(x => x.GetBy(y => y.City == testCity))
            .ReturnsAsync(TestData.GetEmptyAddressBookTestData());
        var addressController = new AddressController(service.Object);


        //Act
        var result = await addressController.GetAddressBook(testCity) as OkObjectResult;

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
        result.StatusCode.Should().Be(200);
        result.Value.Should().BeOfType <List<AddressBook>>();

        var resultData = result.Value as List<AddressBook>;
        resultData.Should().NotBeNull();
        resultData.Count.Should().Be(expectedData.Count);
        resultData.Should().BeEquivalentTo(expectedData);
    }
}