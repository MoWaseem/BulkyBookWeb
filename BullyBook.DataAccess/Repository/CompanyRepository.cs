using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BullyBook.DataAccess.Data;

namespace BullyBook.DataAccess.Repository;

public class CompanyRepository : Repository<Company> ,ICompanyRepository
{
    private readonly ApplicationDbContext _db;

    //public CoverTypeRepository(ApplicationDbContext db):base(db)
    public CompanyRepository(ApplicationDbContext db):base(db) 
    {
        _db = db; 
    }


    public void Update(Company obj)
    {
        _db.Companies.Update(obj);
    }
}