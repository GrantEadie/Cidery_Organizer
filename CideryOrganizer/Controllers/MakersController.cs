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
  public class MakersController : Controller
  {
    private readonly CideryOrganizerContext _db;

    private readonly UserManager<ApplicationUser> _userManager;

    public MakersController(UserManager<ApplicationUser> userManager, CideryOrganizerContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userMakers = _db.Makers.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userMakers);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Maker maker)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      maker.User = currentUser;
      _db.Makers.Add(maker);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }   
    public ActionResult Details(int id)
    {
      var thisMaker = _db.Makers
        .Include(maker => maker.Apples)
        .ThenInclude(join => join.Apple)
        .Include(maker => maker.Ciders)
        .ThenInclude(join => join.Cider)
        .Include(maker => maker.Styles)
        .ThenInclude(join => join.Style)
        .FirstOrDefault(maker => maker.MakerId == id);
      return View(thisMaker);
    }
    public ActionResult Edit(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisMaker);
    }

    [HttpPost]
    public ActionResult Edit(Maker maker, int AppleId, int StyleId, int CiderId)
    {
      if (AppleId != 0)
      {
        _db.AppleMaker.Add(new AppleMaker(){ AppleId = AppleId, MakerId = maker.MakerId });
      }
      if (CiderId != 0)
      {
        _db.CiderMaker.Add(new CiderMaker(){ CiderId = CiderId, MakerId = maker.MakerId });
      }
      if (StyleId != 0)
      {
        _db.MakerStyle.Add(new MakerStyle(){ StyleId = StyleId, MakerId = maker.MakerId });
      }
      _db.Entry(maker).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId });
    }
    public ActionResult Delete(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      return View(thisMaker);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      _db.Makers.Remove(thisMaker);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteApple(int makerId, int joinId)
    {
      var joinEntry = _db.AppleMaker.FirstOrDefault(entry => entry.AppleMakerId == joinId);
      _db.AppleMaker.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = makerId});
    }
    public ActionResult AddApple(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddApple(Maker maker, int AppleId)
    {
      if (AppleId != 0)
      {
      _db.AppleMaker.Add(new AppleMaker() { AppleId = AppleId, MakerId = maker.MakerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId});
    }
    [HttpPost]
    public ActionResult DeleteCider(int makerId, int joinId)
    {
      var joinEntry = _db.CiderMaker.FirstOrDefault(entry => entry.CiderMakerId == joinId);
      _db.CiderMaker.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = makerId});
    }
    public ActionResult AddCider(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddCider(Maker maker, int CiderId)
    {
      if (CiderId != 0)
      {
      _db.CiderMaker.Add(new CiderMaker() { CiderId = CiderId, MakerId = maker.MakerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId});
    }

    [HttpPost]
    public ActionResult DeleteStyle(int makerId, int joinId)
    {
      var joinEntry = _db.MakerStyle.FirstOrDefault(entry => entry.MakerStyleId == joinId);
      _db.MakerStyle.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = makerId});
    }
    public ActionResult AddStyle(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.StyleId = new SelectList(_db.Styles, "StyleId", "StyleName");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddStyle(Maker maker, int StyleId)
    {
      if (StyleId != 0)
      {
      _db.MakerStyle.Add(new MakerStyle() { StyleId = StyleId, MakerId = maker.MakerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId});
    }
  }
}