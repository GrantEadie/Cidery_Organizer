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
        .Include(maker => maker.Makers)
        .ThenInclude(join => join.Maker)
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
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.MakerId = new SelectList(_db.Makers, "TypeId", "TypeName");
      ViewBag.MakerId = new SelectList(_db.Makers, "CiderId", "CiderName");
      return View(thisMaker);
    }

    [HttpPost]
    public ActionResult Edit(Maker maker, int MakerId, int TypeId, int CiderId)
    {
      if (MakerId != 0)
      {
        _db.MakerMaker.Add(new MakerMaker(){ MakerId = MakerId, MakerId = maker.MakerId });
      }
      if (CiderId != 0)
      {
        _db.MakerCider.Add(new MakerCider(){ CiderId = CiderId, MakerId = maker.MakerId });
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
    public ActionResult DeleteMaker(int makerId, int joinId)
    {
      var joinEntry = _db.MakerMaker.FirstOrDefault(entry => entry.MakerMakerId == joinId);
      _db.MakerMaker.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = makerId});
    }
    public ActionResult AddMaker(int id)
    {
      var thisMaker = _db.Makers.FirstOrDefault(makers => makers.MakerId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerTitle");
      return View(thisMaker);
    }
    [HttpPost]
    public ActionResult AddMaker(Maker maker, int MakerId)
    {
      if (MakerId != 0)
      {
      _db.MakerMaker.Add(new MakerMaker() { MakerId = MakerId, MakerId = maker.MakerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = maker.MakerId});
    }
    [HttpPost]
    public ActionResult DeleteCider(int makerId, int joinId)
    {
      var joinEntry = _db.MakerCider.FirstOrDefault(entry => entry.MakerCiderId == joinId);
      _db.MakerCider.Remove(joinEntry);
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
      _db.MakerCider.Add(new MakerCider() { CiderId = CiderId, MakerId = maker.MakerId });
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