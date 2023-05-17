using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BullyBook.DataAccess.Data;

namespace BullyBook.DataAccess.Repository;

public class ProductRespository : Repository<Product> ,IProductRepository
{
    private readonly ApplicationDbContext _db;

    //public CoverTypeRespository(ApplicationDbContext db):base(db)
    public ProductRespository(ApplicationDbContext db):base(db)  // we are passing base/Repository construct 
    {
        _db = db; 
    }


    public void Update(Product obj)
    {
        var objFromDB= _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
        if (objFromDB!=null)
        {
            objFromDB.Title = obj.Title;
            objFromDB.ISBN = obj.ISBN;
            objFromDB.Price = obj.Price;
            objFromDB.Price50 = obj.Price50;
            objFromDB.ListPrice = obj.ListPrice;
            objFromDB.Price100 = obj.Price100;
            objFromDB.Description = obj.Description;
            objFromDB.CategoryId = obj.CategoryId;
            objFromDB.Author = obj.Author;
            objFromDB.CoverTypeId = obj.CoverTypeId;
            if(obj.ImageUrl != null)
            {
                objFromDB.ImageUrl = obj.ImageUrl;
            }
                
        }
    }
}