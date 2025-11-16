using Microsoft.EntityFrameworkCore;
using DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context) => _context = context;

    public async Task<List<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}


