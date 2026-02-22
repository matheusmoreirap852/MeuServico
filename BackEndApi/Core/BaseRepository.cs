using BackEndApi.Context;
using BackEndApi.Repositories.Base;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity>
    : IBaseRepository<TEntity>
    where TEntity : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
        => await _dbSet.ToListAsync();

    public async Task<TEntity> GetById(int id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity> Set(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteById(int id)
    {
        var entity = await GetById(id);

        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}