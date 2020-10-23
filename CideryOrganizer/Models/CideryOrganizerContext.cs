using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CideryOrganizer.Models
{
  public class CideryOrganizerContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Apple> Apples { get; set; }
    public virtual DbSet<Cider> Ciders { get; set; }
    public virtual DbSet<Maker> Makers { get; set; }
    public virtual DbSet<Style> Styles { get; set; }

    public DbSet<AppleCider> AppleCider { get; set; }
    public DbSet<AppleMaker> AppleMaker { get; set; }
    public DbSet<AppleStyle> AppleStyle { get; set; }
    public DbSet<CiderMaker> CiderMaker { get; set; }
    public DbSet<CiderStyle> CiderStyle { get; set; }
    public DbSet <MakerStyle> MakerStyle { get; set; }
    public CideryOrganizerContext(DbContextOptions options) : base (options) { }

  }
}