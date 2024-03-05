using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _db.Categories.ToList();
            List<Category> categoryList = _unitOfWork.Category.GetAll().ToList();

            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "test is an invalid value");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _unitOfWork.Category.Get(u => u.Id == id);
            //Category category = _db.Categories.Find(id); on primary key
            //Category category1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _unitOfWork.Category.Get(u => u.Id == id);
            //Category category = _db.Categories.Find(id); //on primary key
            //Category category1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null) return NotFound();
            _unitOfWork.Category.Remove(obj);
            TempData["success"] = "Category deleted successfully";
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }
    }
}
