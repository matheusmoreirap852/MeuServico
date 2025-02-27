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


       
        public async Task<bool> DeleteById(decimal id)
        {
            try
            {
                var dados = await GetAllId(id);
                if (dados == null)
                {
                    return false; // Ou lançar uma exceção, caso prefira.
                }
                _context.tbRegistroServico.Remove(dados);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Aqui você pode registrar o erro, se necessário
                return false;
            }
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
                var existingEntity = await _context.tbRegistroServico.FindAsync(dados.Id);
                if (existingEntity == null)
                {
                    throw new Exception("Entity not found.");
                }

                // Atualiza as propriedades da entidade existente com os valores de 'dados'
                _context.Entry(existingEntity).CurrentValues.SetValues(dados);

                // Marca a entidade como modificada
                _context.Entry(existingEntity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return existingEntity;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
