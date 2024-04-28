using Application.Common;
using Application.Interfaces.Repository;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AttendanceDbContext _context;

    public BaseRepository(AttendanceDbContext context)
    {
        this._context = context;
    }
    
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public virtual void UpdateEntity(TEntity entity)
    {
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EntityExists(int id)
    {
        return await _context.Set<TEntity>().Where(e => e.Id == id).FirstOrDefaultAsync() != null;
    }
}