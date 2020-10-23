namespace CideryOrganizer.Models
{
  public class CiderMaker
    {       
        public int CiderMakerId { get; set; }
        public int CiderId { get; set; }
        public int MakerId { get; set; }
        public Cider Cider { get; set; }
        public Maker Maker { get; set; }
    }
}