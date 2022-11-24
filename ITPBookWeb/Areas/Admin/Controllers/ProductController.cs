using ITPBook.DataAccess.Repository.IRepository;
using ITPBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITPBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    

    //Get Edit
    public IActionResult Upsert(int? id)
    {
        Product product = new();
        if (id == null || id == 0)
        {
            //Create Product
            return View(product);
        }
        else
        {
            //update product
        }
        
        return View(product);
    }
    //Post Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type updated successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //Get Delete
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        return View(coverTypeFromDb);
    }
    //Post Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
       _unitOfWork.CoverType.Remove(obj);
       _unitOfWork.Save();
       TempData["success"] = "Cover Type deleted successfully";
       return RedirectToAction("Index");
    }
}
