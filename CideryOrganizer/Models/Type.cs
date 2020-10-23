using System.Collections.Generic;

namespace CideryOrganizer.Models
{
  public class Type
  {
    public Type()
    {
      this.Apples = new HashSet<AppleType>();
      this.Ciders = new HashSet<CiderType>();
      this.Makers = new HashSet<MakerType>();
    }

    public int TypeId { get; set; }
    public string TypeName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AppleType> Apples { get; set; }
    public virtual ICollection<CiderType> Ciders { get; set; }
    public virtual ICollection<MakerType> Makers { get; set; }
    
  }
}