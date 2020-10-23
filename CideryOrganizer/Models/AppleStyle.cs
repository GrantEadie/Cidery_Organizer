namespace CideryOrganizer.Models
{
  public class AppleStyle
    {       
        public int AppleStyleId { get; set; }
        public int AppleId { get; set; }
        public int StyleId { get; set; }
        public Apple Apple { get; set; }
        public Style Style { get; set; }
    }
}