using Microsoft.AspNetCore.Mvc;
using CideryOrganizer.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CideryOrganizer.Controllers
{
  public class ApplesController : Controller
  {
    private readonly CideryOrganizerContext _db;

    public ApplesController(CideryOrganizerContext db)
    {
      _db = db ;
    }

    public ActionResult Index()
    {
      List<Apple> model = _db.Apples.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Apple apple)
    {
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
        .Include(apple => apple.Types)
        .ThenInclude(join => join.Type)
        .FirstOrDefault(apple => apple.AppleId == id);
      return View(thisApple);
    }
    public ActionResult Edit(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.MakerId = new SelectList(_db.Makers, "TypeId", "TypeName");
      ViewBag.MakerId = new SelectList(_db.Makers, "CiderId", "CiderName");
      return View(thisApple);
    }

    [HttpPost]
    public ActionResult Edit(Apple apple, int MakerId, int TypeId, int CiderId)
    {
      if (MakerId != 0)
      {
        _db.AppleMaker.Add(new AppleMaker(){ MakerId = MakerId, AppleId = apple.AppleId });
      }
      if (CiderId != 0)
      {
        _db.AppleCider.Add(new AppleCider(){ CiderId = CiderId, AppleId = apple.AppleId });
      }
      if (TypeId != 0)
      {
        _db.AppleType.Add(new AppleType(){ TypeId = TypeId, AppleId = apple.AppleId });
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
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerTitle");
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
    public ActionResult DeleteType(int appleId, int joinId)
    {
      var joinEntry = _db.AppleType.FirstOrDefault(entry => entry.AppleTypeId == joinId);
      _db.AppleType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appleId});
    }
    public ActionResult AddType(int id)
    {
      var thisApple = _db.Apples.FirstOrDefault(apples => apples.AppleId == id);
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      return View(thisApple);
    }
    [HttpPost]
    public ActionResult AddType(Apple apple, int TypeId)
    {
      if (TypeId != 0)
      {
      _db.AppleType.Add(new AppleType() { TypeId = TypeId, AppleId = apple.AppleId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = apple.AppleId});
    }
  }
}