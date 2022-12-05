using ITPBook.DataAccess.Repository.IRepository;
using ITPBook.Models;
using ITPBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITPBookWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Company> objCompanyList = _unitOfWork.Company.GetAll();
        return View(objCompanyList);
    }


    //Get Create
    public IActionResult Create()
    {
        return View();
    }

    //Post Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Company obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Company.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company created succesfully";
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
        var companyFromDb = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);

        if (companyFromDb == null)
        {
            return NotFound();
        }

        return View(companyFromDb);
    }

    //Post Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Company obj)
    {
        
        if (ModelState.IsValid)
        {
            _unitOfWork.Company.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company updated succesfully";
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
        var companyFromDb = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        
        if (companyFromDb == null)
        {
            return NotFound();
        }

        return View(companyFromDb);
    }

    //Post Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Company.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Company deleted succesfully";
        return RedirectToAction("Index");
    }
}
