using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _db.Categories.ToList();
            List<Category> categoryList = _categoryRepository.GetAll().ToList();

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
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _categoryRepository.Get(u => u.Id == id);
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
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Category category = _categoryRepository.Get(u => u.Id == id);
            //Category category = _db.Categories.Find(id); //on primary key
            //Category category1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category category2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category obj = _categoryRepository.Get(u => u.Id == id);
            if (obj == null) return NotFound();
            _categoryRepository.Remove(obj);
            TempData["success"] = "Category deleted successfully";
            _categoryRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
