using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndApi.Repositories.Base
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetById(int id);

        Task<TEntity> Set(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<bool> DeleteById(int id);
    }
}