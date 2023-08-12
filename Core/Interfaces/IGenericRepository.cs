using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpecifications(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> ListAsync(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
