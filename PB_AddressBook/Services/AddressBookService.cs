using Microsoft.EntityFrameworkCore;
using PB_AddressBook.DataBaseContext;
using PB_AddressBook.Model;
using System.Linq.Expressions;

namespace PB_AddressBook.Services;

public class AddressBookService : IAddressBookService
{
    private readonly DatabaseContext _context;

    public AddressBookService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<AddressBook>> Create(AddressBook addressBook)
    {
        await _context.Address.AddAsync(addressBook);
        await _context.SaveChangesAsync();
        return await _context.Address.ToListAsync();
    }

    public async Task<List<AddressBook>> GetAll()
    {
        return await _context.Address.ToListAsync();
    }

    public async Task<List<AddressBook>> GetBy(Expression<Func<AddressBook, bool>> ex)
    {
        return await _context.Address.Where(ex).ToListAsync();
    }

    public async Task<AddressBook> GetSingle(int id)
    {
        return await _context.Address.FirstAsync(x => x.Id == id);
    }

    public async Task<List<AddressBook>> Update(AddressBook addressBook)
    {
        _context.Entry(addressBook).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return await _context.Address.ToListAsync();
    }
    
    public async Task<List<AddressBook>> Delete(AddressBook addressBook)
    {
        _context.Address.Remove(addressBook);
        await _context.SaveChangesAsync();
        return await _context.Address.ToListAsync();
    }
}

