using FinalProject.DataAccessLayer;
using FinalProject.DataAccessLayer.Infrastructure.IRepository;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            return View(categories);
        }
        [HttpGet]
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create(Category category)

        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category success done";

                return RedirectToAction("Index", "Category");


            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)

        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated done";


                return RedirectToAction("Index", "Category");

            }
            return RedirectToAction("create");
        }




        //[HttpGet]
        //public IActionResult CreateUpdate(int? id)
        //{
        //    Category category = new Category();
        //    if (id == null || id == 0)
        //    {
        //        return View(category);
        //    }
        //    else 
        //    {
        //        var EditCategory = _unitOfWork.Category.GetT(x => x.Id == id);
        //        if (id == null || id == 0)
        //        {
        //            return View(EditCategory);
        //        }

        //    }
        //    //var category = _unitOfWork.Category.GetT(x => x.Id == id);
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateUpdate(Category category)

        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Update(category);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Category Updated done";


        //        return RedirectToAction("Index", "Category");

        //    }
        //    return RedirectToAction("create");
        //}









        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            return View();
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)

        {


            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = " Category deleted done ";


            return RedirectToAction("Index", "Category");



        }

    }
}
