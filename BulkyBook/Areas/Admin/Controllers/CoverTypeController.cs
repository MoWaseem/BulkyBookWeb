using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork; //get Schema via unit  *ICoverTypeRepository CoverType { get; }*
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> obj=_unitOfWork.CoverType.GetAll();
            return View(obj);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Created Succesfully";
                return RedirectToAction("Index");
                
            }
            
            return View(obj);
        }
        public IActionResult Edit(int? Id)
        {
            if(Id==null && Id == 0)
            {
                return NotFound();
            }
            var coverTypeFirstFromDb=_unitOfWork.CoverType.GetFirstorDefault(u=>u.Id==Id);
            if (coverTypeFirstFromDb == null)
            {
                return NotFound() ;
            }

            return View(coverTypeFirstFromDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Edit Succesfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id) { 
            if(Id==null && Id == 0) { return NotFound(); }
            var coverTypeFirstFromDb=_unitOfWork.CoverType.GetFirstorDefault(u=> u.Id==Id);
            if (coverTypeFirstFromDb == null) { return NotFound() ; }


            return View(coverTypeFirstFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType obj)
        {
            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type Delete Succesfully";
            return RedirectToAction("Index");
        }
       
    }
}
