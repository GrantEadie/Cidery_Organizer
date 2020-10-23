using System.Collections.Generic;

namespace CideryOrganizer.Models
{
  public class Style
  {
    public Style()
    {
      this.Apples = new HashSet<AppleStyle>();
      this.Ciders = new HashSet<CiderStyle>();
      this.Makers = new HashSet<MakerStyle>();
    }

    public int StyleId { get; set; }
    public string StyleName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AppleStyle> Apples { get; set; }
    public virtual ICollection<CiderStyle> Ciders { get; set; }
    public virtual ICollection<MakerStyle> Makers { get; set; }
    
  }
}