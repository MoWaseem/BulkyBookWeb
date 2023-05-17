using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BullyBook.DataAccess.Repository.IRepository;

// these are methods use Implemenet in ****IRepository****

//T GetFirstorDefault(Expression<Func<T, bool>> filter);
//IEnumerable<T> GetAll();
//void Add(T entity);
//void Remove(T entity);
//void RemoveRange(IEnumerable<T> entity);
public interface IProductRepository:IRepository<Product>
{
    void Update(Product obj);
 
}