using Microsoft.AspNetCore.Mvc;
using CideryOrganizer.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace CideryOrganizer.Controllers
{
  public class HomeController : Controller
  {
    private readonly CideryOrganizerContext _db;

    public HomeController(CideryOrganizerContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      var thisCider = _db.Ciders.FirstOrDefault();
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View();
    }
  }
}