using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_AddressBook.Tests;

internal class TestData
{
    public static List<AddressBook> GetAddressBookTestData()
    {
        return new List<AddressBook>
        {
            new AddressBook
            {
                Id = 1,
                Street = "TestStreet",
                HouseNumber = "1",
                City = "TestCity",
                Country = "TestCountry",
                Zip = "12345",
                Phone = "123456789",
                Email = "address1@test.test"
            },
            new AddressBook
            {
                Id = 2,
                Street = "TestStreet2",
                HouseNumber = "2",
                City = "TestCity2",
                Country = "TestCountry2",
                Zip = "12345",
                Phone = "123456789",
                Email = "address2@test.test"
            },
            new AddressBook
            {
                Id = 3,
                Street = "TestStreet3",
                HouseNumber = "3",
                City = "TestCity3",
                Country = "TestCountry3",
                Zip = "12345",
                Phone = "123456789",
                Email = "address3@test.test"
            }
        };
    }

    public static List<AddressBook> GetEmptyAddressBookTestData()
    {
        return new List<AddressBook>();
    }
}
