using BullyBook.DataAccess.Repository.IRepository;
using BullyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        // Create GET
        public IActionResult Create()
        {
            return View(); // this view working( View->Category->Create)
        }
        //Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)

        {
            if (obj.Title == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("title", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Succesfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            //var categoryFromDB = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst); //CategoryController:View->Category->Edit
        }
        //Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)

        {
            if (obj.Title == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("title", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edit Succesfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            //var categoryFromDB = _db.Categories.Find(id);
            var categoryFromDbFirst = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst); //CategoryController:View->Category->Edit
        }
        //Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)

        {
            var obj = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Delete Succesfully";

            return RedirectToAction("Index");


        }

    }
}
