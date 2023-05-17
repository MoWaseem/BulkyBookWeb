using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BullyBook.DataAccess.Data;

namespace BullyBook.DataAccess.Repository;

public class CoverTypeRespository : Repository<CoverType> ,ICoverTypeRepository
{
    private readonly ApplicationDbContext _db;

    //public CoverTypeRespository(ApplicationDbContext db):base(db)
    public CoverTypeRespository(ApplicationDbContext db):base(db) 
    {
        _db = db; 
    }


    public void Update(CoverType obj)
    {
        _db.CoverTypes.Update(obj);
    }
}