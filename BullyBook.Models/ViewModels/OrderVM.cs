using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullyBook.Models.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
