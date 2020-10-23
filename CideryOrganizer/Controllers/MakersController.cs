using Microsoft.AspNetCore.Mvc;
using CideryOrganizer.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CideryOrganizer.Controllers
{
  public class MakersController : Controller
  {
    private readonly CideryOrganizerContext _db;

    public MakersController(CideryOrganizerContext db)
    {
      _db = db ;
    }

    public ActionResult Index()
    {
      List<Maker> model = _db.Makers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Maker maker)
    {
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
        .Include(maker => maker.Types)
        .ThenInclude(join => join.Type)
        .FirstOrDefault(maker => maker.MakerId == id);
      return View(thisMaker);
    }
    public ActionResult Edit(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisMaker);
    }

    [HttpPost]
    public ActionResult Edit(Maker maker, int AppleId, int TypeId, int CiderId)
    {
      if (AppleId != 0)
      {
        _db.AppleMaker.Add(new AppleMaker(){ AppleId = AppleId, MakerId = maker.MakerId });
      }
      if (CiderId != 0)
      {
        _db.CiderMaker.Add(new CiderMaker(){ CiderId = CiderId, MakerId = maker.MakerId });
      }
      if (TypeId != 0)
      {
        _db.MakerType.Add(new MakerType(){ TypeId = TypeId, MakerId = maker.MakerId });
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
      ViewBag.MakerId = new SelectList(_db.Apples, "AppleId", "AppleTitle");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddApple(Maker maker, int AppleId)
    {
      if (MakerId != 0)
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
    public ActionResult DeleteType(int makerId, int joinId)
    {
      var joinEntry = _db.MakerType.FirstOrDefault(entry => entry.MakerTypeId == joinId);
      _db.MakerType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = makerId});
    }
    public ActionResult AddType(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddType(Maker maker, int TypeId)
    {
      if (TypeId != 0)
      {
      _db.MakerType.Add(new MakerType() { TypeId = TypeId, MakerId = maker.MakerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId});
    }
  }
}