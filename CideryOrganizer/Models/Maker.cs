using System.Collections.Generic;

namespace CideryOrganizer.Models
{
  public class Maker
  {
    public Maker()
    {
      this.Apples = new HashSet<AppleMaker>();
      this.Ciders = new HashSet<CiderMaker>();
      this.Styles = new HashSet<MakerStyle>();
    }

    public int MakerId { get; set; }
    public string MakerName { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AppleMaker> Apples { get; set; }
    public virtual ICollection<CiderMaker> Ciders { get; set; }
    public virtual ICollection<MakerStyle> Styles { get; set; }
    
  }
}