using System.Linq.Expressions;
using PB_AddressBook.Model;

namespace PB_AddressBook.Services;

public interface IAddressBookService
{
    Task<List<AddressBook>> Create(AddressBook addressBook);
    Task<List<AddressBook>> Update(AddressBook addressBook);
    Task<List<AddressBook>> GetAll();
    Task<List<AddressBook>> GetBy(Expression<Func<AddressBook,bool>> ex);
    Task<AddressBook> GetSingle(int id);
    Task<List<AddressBook>> Delete(AddressBook addressBook);
}
