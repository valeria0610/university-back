using System.Collections.Generic;
using System.Threading.Tasks;

namespace University.BL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int id);

        //task metodos asincronos
        // quien implemente esa internetfaz debe de terner esos metodos, la interfaz es como un control
    }
}
