namespace CideryOrganizer.Models
{
  public class AppleMaker
    {       
        public int AppleMakerId { get; set; }
        public int AppleId { get; set; }
        public int MakerId { get; set; }
        public Apple Apple { get; set; }
        public Maker Maker { get; set; }
    }
}