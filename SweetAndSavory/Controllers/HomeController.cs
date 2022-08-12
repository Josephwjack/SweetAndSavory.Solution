using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetAndSavory.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace SweetAndSavory.Controllers
{
  public class HomeController : Controller
  {
    private readonly SweetAndSavoryContext _db;

    public HomeController(SweetAndSavoryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      ViewBag.PageTitle = "Home";
      return View();
    }

    // public async Task<IActionResult> Index(string searchString)
    // {
    //   var books = from b in _db.Books select b;
    //   if (!String.IsNullOrEmpty(searchString))
    //   {
    //     books = books.Where(s => s.Title!.Contains(searchString));
    //   }
    //   return View(await books.ToListAsync());
    // }
  }
}