using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FormularioDinamico.Domain.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void Save();
        Task SaveAsync();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
    }
}
