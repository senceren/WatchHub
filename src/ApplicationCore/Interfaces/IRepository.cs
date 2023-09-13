using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    // Burda verilerin nasıl gösterileceğine dair bilgiler yer alır.
    // Generic Repository Pattern 
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();

        // belirli sınırlarda listemek ve getirmek için
        Task<List<T>> GetAllAsync(ISpecification<T> specification);  
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity); // geri döndürmeye gerek yok
        Task DeleteAsync(T entity);

        // 300 dolardan fazla olan ürünlerin sayısını getir.
        Task<int> CountAsync(ISpecification<T> specification);
        Task<T> FirstAsync(ISpecification<T> specification);
        Task<T?> FirstOrDefaultAsync(ISpecification<T> specification);
    }
}
