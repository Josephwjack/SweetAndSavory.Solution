using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetAndSavory.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly SweetAndSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, SweetAndSavoryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View all Flavors";
      return View(_db.Flavors.ToList());
    }

    [AllowAnonymous]
    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add new Flavor";
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      if (_db.Flavors.FirstOrDefault(f => f.Name == flavor.Name) == null)
      {
        _db.Flavors.Add(flavor);
        _db.SaveChanges(); 
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      ViewBag.PageTitle = $"Flavor {flavor.Name}";

      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(flavor);
    }


     
    [HttpPost]
    public ActionResult Details(FlavorTreat ft)
    {
      if (_db.FlavorTreat.FirstOrDefault(f => f.FlavorId == ft.FlavorId && f.TreatId == ft.TreatId) == null)
      {
        _db.FlavorTreat.Add(ft);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = ft.FlavorId });
    }

    
    public ActionResult Edit(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      ViewBag.PageTitle = $"Edit {flavor.Name}";
      return View(flavor);
    }

    
    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = flavor.FlavorId });
    }

   
    public ActionResult Delete(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      ViewBag.PageTitle = $"Delete {flavor.Name}?";
      return View(flavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult Deleted(int id)
    {
      Flavor flavor = _db.Flavors.FirstOrDefault(f => f.FlavorId == id);
      _db.Flavors.Remove(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
    [HttpPost]
    public ActionResult DeleteFlavor(int flavorTreatId)
    {
      var ft = _db.FlavorTreat.FirstOrDefault(f => f.FlavorTreatId == flavorTreatId);
      if (ft != null)
      {
        _db.FlavorTreat.Remove(ft);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = ft.FlavorId });
    }

  }
}