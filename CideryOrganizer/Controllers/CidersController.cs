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
  public class CidersController : Controller
  {
    private readonly CideryOrganizerContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CidersController(UserManager<ApplicationUser> userManager, CideryOrganizerContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userCiders = _db.Ciders.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userCiders);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Cider cider)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      cider.User = currentUser;
      _db.Ciders.Add(cider);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }    
    public ActionResult Details(int id)
    {
      var thisCider = _db.Ciders
        .Include(cider => cider.Makers)
        .ThenInclude(join => join.Maker)
        .Include(cider => cider.Apples)
        .ThenInclude(join => join.Apple)
        .Include(cider => cider.Styles)
        .ThenInclude(join => join.Style)
        .FirstOrDefault(cider => cider.CiderId == id);
      return View(thisCider);
    }
    public ActionResult Edit(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View(thisCider);
    }

    [HttpPost]
    public ActionResult Edit(Cider cider, int MakerId, int StyleId, int AppleId)
    {
      if (MakerId != 0)
      {
        _db.CiderMaker.Add(new CiderMaker(){ MakerId = MakerId, CiderId = cider.CiderId });
      }
      if (AppleId != 0)
      {
        _db.AppleCider.Add(new AppleCider(){ AppleId = AppleId, CiderId = cider.CiderId });
      }
      if (StyleId != 0)
      {
        _db.CiderStyle.Add(new CiderStyle(){ StyleId = StyleId, CiderId = cider.CiderId });
      }
      _db.Entry(cider).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId });
    }
    public ActionResult Delete(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      return View(thisCider);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      _db.Ciders.Remove(thisCider);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteMaker(int ciderId, int joinId)
    {
      var joinEntry = _db.CiderMaker.FirstOrDefault(entry => entry.CiderMakerId == joinId);
      _db.CiderMaker.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ciderId});
    }
    public ActionResult AddMaker(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      return View(thisCider);
    }
    [HttpPost]
    public ActionResult AddMaker(Cider cider, int MakerId)
    {
      if (MakerId != 0)
      {
      _db.CiderMaker.Add(new CiderMaker() { MakerId = MakerId, CiderId = cider.CiderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId});
    }
    [HttpPost]
    public ActionResult DeleteApple(int ciderId, int joinId)
    {
      var joinEntry = _db.AppleCider.FirstOrDefault(entry => entry.AppleCiderId == joinId);
      _db.AppleCider.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ciderId});
    }
    public ActionResult AddApple(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View(thisCider);
    }
    [HttpPost]
    public ActionResult AddCider(Cider cider, int AppleId)
    {
      if (AppleId != 0)
      {
      _db.AppleCider.Add(new AppleCider() { AppleId = AppleId, CiderId = cider.CiderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId});
    }

    [HttpPost]
    public ActionResult DeleteStyle(int ciderId, int joinId)
    {
      var joinEntry = _db.CiderStyle.FirstOrDefault(entry => entry.CiderStyleId == joinId);
      _db.CiderStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ciderId});
    }
    public ActionResult AddStyle(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      return View(thisCider);
    }
    [HttpPost]
    public ActionResult AddStyle(Cider cider, int StyleId)
    {
      if (StyleId != 0)
      {
      _db.CiderStyle.Add(new CiderStyle() { StyleId = StyleId, CiderId = cider.CiderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId});
    }
  }
}