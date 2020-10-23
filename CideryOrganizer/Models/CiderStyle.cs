namespace CideryOrganizer.Models
{
  public class CiderStyle
    {       
        public int CiderStyleId { get; set; }
        public int CiderId { get; set; }
        public int StyleId { get; set; }
        public Cider Cider { get; set; }
        public Style Style { get; set; }
    }
}