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
  [Authorize]
  public class ApplesController : Controller
  {
    private readonly CideryOrganizerContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplesController(UserManager<ApplicationUser> userManager, CideryOrganizerContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userApples = _db.Apples.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userApples);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Apple apple)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      apple.User = currentUser;
      _db.Apples.Add(apple);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }  
    public ActionResult Details(int id)
    {
      var thisApple = _db.Apples
        .Include(apple => apple.Makers)
        .ThenInclude(join => join.Maker)
        .Include(apple => apple.Ciders)
        .ThenInclude(join => join.Cider)
        .Include(apple => apple.Styles)
        .ThenInclude(join => join.Style)
        .FirstOrDefault(apple => apple.AppleId == id);
      return View(thisApple);
    }
    public ActionResult Edit(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisApple);
    }

    [HttpPost]
    public ActionResult Edit(Apple apple, int MakerId, int StyleId, int CiderId)
    {
      if (MakerId != 0)
      {
        _db.AppleMaker.Add(new AppleMaker(){ MakerId = MakerId, AppleId = apple.AppleId });
      }
      if (CiderId != 0)
      {
        _db.AppleCider.Add(new AppleCider(){ CiderId = CiderId, AppleId = apple.AppleId });
      }
      if (StyleId != 0)
      {
        _db.AppleStyle.Add(new AppleStyle(){ StyleId = StyleId, AppleId = apple.AppleId });
      }
      _db.Entry(apple).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = apple.AppleId });
    }
    public ActionResult Delete(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      return View(thisApple);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      _db.Apples.Remove(thisApple);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteMaker(int appleId, int joinId)
    {
      var joinEntry = _db.AppleMaker.FirstOrDefault(entry => entry.AppleMakerId == joinId);
      _db.AppleMaker.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appleId});
    }
    public ActionResult AddMaker(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      return View(thisApple);
    }
    [HttpPost]
    public ActionResult AddMaker(Apple apple, int MakerId)
    {
      if (MakerId != 0)
      {
      _db.AppleMaker.Add(new AppleMaker() { MakerId = MakerId, AppleId = apple.AppleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = apple.AppleId});
    }
    [HttpPost]
    public ActionResult DeleteCider(int appleId, int joinId)
    {
      var joinEntry = _db.AppleCider.FirstOrDefault(entry => entry.AppleCiderId == joinId);
      _db.AppleCider.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appleId});
    }
    public ActionResult AddCider(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisApple);
    }
    [HttpPost]
    public ActionResult AddCider(Apple apple, int CiderId)
    {
      if (CiderId != 0)
      {
      _db.AppleCider.Add(new AppleCider() { CiderId = CiderId, AppleId = apple.AppleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = apple.AppleId});
    }

    [HttpPost]
    public ActionResult DeleteStyle(int appleId, int joinId)
    {
      var joinEntry = _db.AppleStyle.FirstOrDefault(entry => entry.AppleStyleId == joinId);
      _db.AppleStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appleId});
    }
    public ActionResult AddStyle(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      return View(thisApple);
    }
    [HttpPost]
    public ActionResult AddStyle(Apple apple, int StyleId)
    {
      if (StyleId != 0)
      {
      _db.AppleStyle.Add(new AppleStyle() { StyleId = StyleId, AppleId = apple.AppleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = apple.AppleId});
    }
  }
}