using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BullyBook.DataAccess.Data;

namespace BullyBook.DataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderHeaderRepository(ApplicationDbContext db):base(db)
    {
        _db = db; 
    }


    public void Update(OrderHeader obj)
    {
        _db.OrderHeaders.Update(obj);
    }

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
        if (orderFromDb != null)
        {
            orderFromDb.OrderStatus=orderStatus;
            if (paymentStatus != null)
            {
                orderFromDb.PaymentStatus = paymentStatus;
            }
            
        }
    }

    public void UpdateStripPaymentId(int id, string sessionId)
    {
        var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);


        orderFromDb.SessionId = sessionId;
        orderFromDb.PaymnetDate = DateTime.Now;


    }
    public void UpdateStripPaymentIntentId(int id,string paymentIntentId)
    {
        var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
       
        orderFromDb.PaymentIntentId = paymentIntentId;
        orderFromDb.PaymnetDate= DateTime.Now;

    }
}