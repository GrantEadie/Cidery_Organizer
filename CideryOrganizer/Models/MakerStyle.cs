namespace CideryOrganizer.Models
{
  public class MakerStyle
    {       
        public int MakerStyleId { get; set; }
        public int MakerId { get; set; }
        public int StyleId { get; set; }
        public Maker Maker { get; set; }
        public Style Style { get; set; }
    }
}