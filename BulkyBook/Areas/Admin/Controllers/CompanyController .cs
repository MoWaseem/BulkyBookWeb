using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using BullyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; 

       
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         
        }
        public IActionResult Index()
        {

            return View();

        }

        public IActionResult Upsert(int? id)
        {
            Company company = new();
       
            if (id == null || id == 0)
            {
           
                return View(company);
            }
            else
            {

                company=_unitOfWork.Company.GetFirstorDefault(u=>u.Id == id);
                return View(company);
                //Update the product
            }
       

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
       
                if (obj.Id == 0 )
                {
                    _unitOfWork.Company.Add(obj);
                    _unitOfWork.Save();
                    TempData["success"] = "Created  Succesfully";


                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Updated  Succesfully";
                }
                

                _unitOfWork.Save();
                TempData["success"] = "Cover Type Edit Succesfully";


                return RedirectToAction("Index");
            }
            return View(obj);
        }

     



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }


        [HttpDelete]
       
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstorDefault(u => u.Id == id);
            if (obj == null) { 
                return Json(new {success=false,message="Erroe while Deleting"});
            }

            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Sucess" });
        }

        #endregion
    }
}
