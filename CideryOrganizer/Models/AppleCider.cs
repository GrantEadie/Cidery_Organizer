namespace CideryOrganizer.Models
{
  public class AppleCider
    {       
        public int AppleCiderId { get; set; }
        public int AppleId { get; set; }
        public int CiderId { get; set; }
        public Apple Apple { get; set; }
        public Cider Cider { get; set; }
    }
}