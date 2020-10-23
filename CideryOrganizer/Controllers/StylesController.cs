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
  public class StylesController : Controller
  {
    private readonly CideryOrganizerContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public StylesController(UserManager<ApplicationUser> userManager, CideryOrganizerContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userStyles = _db.Styles.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userStyles);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Style style)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      style.User = currentUser;
      _db.Styles.Add(style);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }  
    public ActionResult Details(int id)
    {
      var thisStyle = _db.Styles
        .Include(style => style.Apples)
        .ThenInclude(join => join.Apple)
        .Include(style => style.Ciders)
        .ThenInclude(join => join.Cider)
        .Include(style => style.Makers)
        .ThenInclude(join => join.Maker)
        .FirstOrDefault(style => style.StyleId == id);
      return View(thisStyle);
    }
    public ActionResult Edit(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisStyle);
    }

    [HttpPost]
    public ActionResult Edit(Style style, int MakerId, int AppleId, int CiderId)
    {
      if (AppleId != 0)
      {
        _db.AppleStyle.Add(new AppleStyle(){ AppleId = AppleId, StyleId = style.StyleId });
      }
      if (CiderId != 0)
      {
        _db.CiderStyle.Add(new CiderStyle(){ CiderId = CiderId, StyleId = style.StyleId });
      }
      if (MakerId != 0)
      {
        _db.MakerStyle.Add(new MakerStyle(){ MakerId = MakerId, StyleId = style.StyleId });
      }
      _db.Entry(style).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = style.StyleId });
    }
    public ActionResult Delete(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      return View(thisStyle);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      _db.Styles.Remove(thisStyle);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteApple(int styleId, int joinId)
    {
      var joinEntry = _db.AppleStyle.FirstOrDefault(entry => entry.AppleStyleId == joinId);
      _db.AppleStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = styleId});
    }
    public ActionResult AddApple(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      ViewBag.StyleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View(thisStyle);
    }
    [HttpPost]
    public ActionResult AddApple(Style style, int AppleId)
    {
      if (AppleId != 0)
      {
      _db.AppleStyle.Add(new AppleStyle() { AppleId = AppleId, StyleId = style.StyleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = style.StyleId});
    }
    [HttpPost]
    public ActionResult DeleteCider(int styleId, int joinId)
    {
      var joinEntry = _db.CiderStyle.FirstOrDefault(entry => entry.CiderStyleId == joinId);
      _db.CiderStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = styleId});
    }
    public ActionResult AddCider(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisStyle);
    }
    [HttpPost]
    public ActionResult AddCider(Style style, int CiderId)
    {
      if (CiderId != 0)
      {
      _db.CiderStyle.Add(new CiderStyle() { CiderId = CiderId, StyleId = style.StyleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = style.StyleId});
    }

    [HttpPost]
    public ActionResult DeleteMaker(int styleId, int joinId)
    {
      var joinEntry = _db.MakerStyle.FirstOrDefault(entry => entry.MakerStyleId == joinId);
      _db.MakerStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = styleId});
    }
    public ActionResult AddMaker(int id)
    {
      var thisStyle = _db.Styles.FirstOrDefault(styles => styles.StyleId == id);
      ViewBag.StyleId = new SelectList(_db.Makers, "MakerId", "MakerName");
      return View(thisStyle);
    }
    [HttpPost]
    public ActionResult AddMaker(Style style, int MakerId)
    {
      if (MakerId != 0)
      {
      _db.MakerStyle.Add(new MakerStyle() { MakerId = MakerId, StyleId = style.StyleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = style.StyleId});
    }
  }
}