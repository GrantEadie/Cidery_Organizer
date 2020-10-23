using System.Collections.Generic;

namespace CideryOrganizer.Models
{
  public class Apple
  {
    public Apple()
    {
      this.Ciders = new HashSet<AppleCider>();
      this.Makers = new HashSet<AppleMaker>();
      this.Types = new HashSet<AppleType>();
    }

    public int AppleId { get; set; }
    public string AppleName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AppleCider> Ciders { get; set; }
    public virtual ICollection<AppleMaker> Makers { get; set; }
    public virtual ICollection<AppleType> Types { get; set; }
    
  }
}