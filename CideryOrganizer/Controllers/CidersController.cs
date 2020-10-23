using Microsoft.AspNetCore.Mvc;
using CideryOrganizer.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace CideryOrganizer.Controllers
{
  public class CidersController : Controller
  {
    private readonly CideryOrganizerContext _db;

    public CidersController(CideryOrganizerContext db)
    {
      _db = db ;
    }

    public ActionResult Index()
    {
      List<Cider> model = _db.Ciders.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Cider cider)
    {
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
        .Include(cider => cider.Types)
        .ThenInclude(join => join.Type)
        .FirstOrDefault(cider => cider.CiderId == id);
      return View(thisCider);
    }
    public ActionResult Edit(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerName");
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      ViewBag.AppleId = new SelectList(_db.Apples, "AppleId", "AppleName");
      return View(thisCider);
    }

    [HttpPost]
    public ActionResult Edit(Cider cider, int MakerId, int TypeId, int AppleId)
    {
      if (MakerId != 0)
      {
        _db.CiderMaker.Add(new CiderMaker(){ MakerId = MakerId, CiderId = cider.CiderId });
      }
      if (CiderId != 0)
      {
        _db.AppleCider.Add(new AppleCider(){ CiderId = CiderId, CiderId = cider.CiderId });
      }
      if (TypeId != 0)
      {
        _db.CiderType.Add(new CiderType(){ TypeId = TypeId, CiderId = cider.CiderId });
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
      ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "MakerTitle");
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
    public ActionResult DeleteApple(int appleId, int joinId)
    {
      var joinEntry = _db.CiderCider.FirstOrDefault(entry => entry.AppleCiderId == joinId);
      _db.CiderCider.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ciderId});
    }
    public ActionResult AddApple(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.CiderId = new SelectList(_db.Ciders, "AppleId", "AppleName");
      return View(thisCider);
    }
    [HttpPost]
    public ActionResult AddCider(Cider cider, int AppleId)
    {
      if (CiderId != 0)
      {
      _db.AppleCider.Add(new AppleCider() { AppleId = AppleId, CiderId = cider.CiderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId});
    }

    [HttpPost]
    public ActionResult DeleteType(int ciderId, int joinId)
    {
      var joinEntry = _db.CiderType.FirstOrDefault(entry => entry.CiderTypeId == joinId);
      _db.CiderType.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = ciderId});
    }
    public ActionResult AddType(int id)
    {
      var thisCider = _db.Ciders.FirstOrDefault(ciders => ciders.CiderId == id);
      ViewBag.TypeId = new SelectList(_db.Types, "TypeId", "TypeName");
      return View(thisCider);
    }
    [HttpPost]
    public ActionResult AddType(Cider cider, int TypeId)
    {
      if (TypeId != 0)
      {
      _db.CiderType.Add(new CiderType() { TypeId = TypeId, CiderId = cider.CiderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = cider.CiderId});
    }
  }
}