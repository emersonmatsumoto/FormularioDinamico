using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FormularioDinamico.Domain.Repositories
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
    }
}
