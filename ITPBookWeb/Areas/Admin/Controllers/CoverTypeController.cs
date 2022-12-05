using ITPBook.DataAccess.Repository.IRepository;
using ITPBook.Models;
using ITPBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    //Get Create
    public IActionResult Create()
    {
        return View();
    }
    //Post Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create (CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type added successfully";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //Get Edit
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var coverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(c=>c.Id==id);
        return View(coverTypeFromDb);
    }
    //Post Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
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
