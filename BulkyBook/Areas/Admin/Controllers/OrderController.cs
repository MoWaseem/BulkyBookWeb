using BullyBook.DataAccess.Repository;
using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BulkyBook.Utilities;
using System.Security.Claims;
using BullyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area(("Admin"))]
    [Authorize]
    public class OrderController : Controller
    {
        
        public readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int test)
        {
            OrderVM = new OrderVM()
            {
                OrderHeader =
                    _unitOfWork.OrderHeader.GetFirstorDefault(u => u.Id == test,
                        includeProperties: "ApplicationUser"),
                OrderDetail =
                    _unitOfWork.OrderDetail.GetAll(u => u.Id == test,
                        includeProperties: "Product"),
            };
                
            return View(OrderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {

            IEnumerable<OrderHeader> orderHeaders;

            if(User.IsInRole(Sd.Role_Admin) || User.IsInRole((Sd.Role_Employee)))
            {

                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitOfWork.OrderHeader.GetAll(u=>u.ApplicationUserId==claim.Value,
                    includeProperties: "ApplicationUser");
            }


            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == Sd.PaymentStatusDelayedPayment);
                    break;
                case "inprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Sd.StatusInProcess);
                    break;
                case "completed":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Sd.StatusShipped);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == Sd.StatusApproved);
                    break;
                default:
                    
                    break;
            }
      

            return Json(new { data = orderHeaders });
        }
        #endregion
    }
}
