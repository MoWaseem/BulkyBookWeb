using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BullyBook.DataAccess.Repository.IRepository;

//this generic Repositpory where T is class
//this generic Repositpory where T is class
public interface IRepository<T> where T : class

{
    //Hypothically Assume that This T is Categorgy
    //T-Category
    T GetFirstorDefault(Expression<Func<T,bool>> filter,string? includeProperties=null);
    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);

}