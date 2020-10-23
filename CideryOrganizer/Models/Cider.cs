using System.Collections.Generic;

namespace CideryOrganizer.Models
{
  public class Cider
  {
    public Cider()
    {
      this.Apples = new HashSet<AppleCider>();
      this.Makers = new HashSet<CiderMaker>();
      this.Styles = new HashSet<CiderStyle>();
    }

    public int CiderId { get; set; }
    public string CiderName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AppleCider> Apples { get; set; }
    public virtual ICollection<CiderMaker> Makers { get; set; }
    public virtual ICollection<CiderStyle> Styles { get; set; }
    
  }
}