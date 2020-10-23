using Microsoft.AspNetCore.Mvc;
using CideryOrganizer.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CideryOrganizer.Controllers
{
  public class TypesController : Controller
  {
    private readonly CideryOrganizerContext _db;

    public TypesController(CideryOrganizerContext db)
    {
      _db = db ;
    }

    public ActionResult Index()
    {
      List<Type> model = _db.Types.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Type type)
    {
      _db.Types.Add(type);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }  
    public ActionResult Details(int id)
    {
      var thisType = _db.Types
        .Include(type => type.Apples)
        .ThenInclude(join => join.Apple)
        .Include(type => type.Ciders)
        .ThenInclude(join => join.Cider)
        .Include(type => type.Makers)
        .ThenInclude(join => join.Maker)
        .FirstOrDefault(type => type.TypeId == id);
      return View(thisType);
    }
    public ActionResult Edit(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisType);
    }

    [HttpPost]
    public ActionResult Edit(Type type, int MakerId, int TypeId, int CiderId)
    {
      if (AppleId != 0)
      {
        _db.AppleType.Add(new AppleType(){ AppleId = AppleId, TypeId = type.TypeId });
      }
      if (CiderId != 0)
      {
        _db.CiderType.Add(new CiderType(){ CiderId = CiderId, TypeId = type.TypeId });
      }
      if (MakerId != 0)
      {
        _db.MakerType.Add(new MakerType(){ MakerId = MakerId, TypeId = type.TypeId });
      }
      _db.Entry(type).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = type.TypeId });
    }
    public ActionResult Delete(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      return View(thisType);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      _db.Types.Remove(thisType);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteApple(int typeId, int joinId)
    {
      var joinEntry = _db.AppleType.FirstOrDefault(entry => entry.AppleTypeId == joinId);
      _db.AppleType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = typeId});
    }
    public ActionResult AddApple(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      ViewBag.TypeId = new SelectList(_db.Apples, "AppleId", "AppleTitle");
      return View(thisType);
    }
    [HttpPost]
    public ActionResult AddApple(Type type, int AppleId)
    {
      if (TypeId != 0)
      {
      _db.AppleType.Add(new AppleType() { AppleId = AppleId, TypeId = type.TypeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = type.TypeId});
    }
    [HttpPost]
    public ActionResult DeleteCider(int typeId, int joinId)
    {
      var joinEntry = _db.CiderType.FirstOrDefault(entry => entry.CiderTypeId == joinId);
      _db.CiderType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = typeId});
    }
    public ActionResult AddCider(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      ViewBag.CiderId = new SelectList(_db.Ciders, "CiderId", "CiderName");
      return View(thisType);
    }
    [HttpPost]
    public ActionResult AddCider(Type type, int CiderId)
    {
      if (CiderId != 0)
      {
      _db.CiderType.Add(new CiderType() { CiderId = CiderId, TypeId = type.TypeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = type.TypeId});
    }

    [HttpPost]
    public ActionResult DeleteMaker(int typeId, int joinId)
    {
      var joinEntry = _db.MakerType.FirstOrDefault(entry => entry.MakerTypeId == joinId);
      _db.MakerType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = typeId});
    }
    public ActionResult AddMaker(int id)
    {
      var thisType = _db.Types.FirstOrDefault(types => types.TypeId == id);
      ViewBag.TypeId = new SelectList(_db.Makers, "MakerId", "MakerName");
      return View(thisType);
    }
    [HttpPost]
    public ActionResult AddMaker(Type type, int MakerId)
    {
      if (TypeId != 0)
      {
      _db.MakerType.Add(new MakerType() { MakerId = MakerId, TypeId = type.TypeId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = type.TypeId});
    }
  }
}