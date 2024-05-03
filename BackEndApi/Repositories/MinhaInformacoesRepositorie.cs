using BackEndApi.Context;
using BackEndApi.Models;
using BackEndApi.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Repositories
{
    public class MinhaInformacoesRepositorie : IMinhaInformacoesRepositorie
    {
        private const bool V = false;
        private readonly AppDbContext _context;

        public MinhaInformacoesRepositorie(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteById(int id)
        {
            try
            {
                var registro = await _context.tbRegistroServico.FindAsync(id);
                if (registro != null)
                {
                    _context.tbRegistroServico.Remove(registro);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteById(decimal id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RegistroServico>> GetAll()
        {
            try
            {
                return await _context.tbRegistroServico.ToListAsync();
            }catch (Exception ex)
            {
                return Enumerable.Empty<RegistroServico>();
            }
        }

        public async Task<RegistroServico> GetAllId(decimal id)
        {
            try
            {
                return await _context.tbRegistroServico.Where(c => c.Id == id).FirstOrDefaultAsync();
            }catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RegistroServico> Set(RegistroServico dados)
        {
            try
            {
                var entityEntry = _context.tbRegistroServico.Add(dados);
                await _context.SaveChangesAsync();
                return entityEntry.Entity;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<RegistroServico> Update(RegistroServico dados)
        {
            try
            {
                var existingEntity = _context.tbRegistroServico.Attach(dados);
                _context.Entry(existingEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return existingEntity.Entity;

            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
