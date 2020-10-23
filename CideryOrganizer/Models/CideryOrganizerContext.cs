using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CideryOrganizer.Models
{
  public class CideryOrganizerContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Apple> Apples { get; set; }
    public virtual DbSet<Cider> Ciders { get; set; }
    public virtual DbSet<Maker> Makers { get; set; }
    public virtual DbSet<Type> Types { get; set; }

    public DbSet<AppleCider> AppleCider { get; set; }
    public DbSet<AppleMaker> AppleMaker { get; set; }
    public DbSet<AppleType> AppleType { get; set; }
    public DbSet<CiderMaker> CiderMaker { get; set; }
    public DbSet<CiderType> CiderType { get; set; }
    public DbSet <MakerType> MakerType { get; set; }
    public CideryOrganizerContext(DbContextOptions options) : base (options) { }

  }
}