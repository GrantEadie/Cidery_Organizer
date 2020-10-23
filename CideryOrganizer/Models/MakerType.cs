namespace CideryOrganizer.Models
{
  public class MakerType
    {       
        public int MakerTypeId { get; set; }
        public int MakerId { get; set; }
        public int TypeId { get; set; }
        public Maker Maker { get; set; }
        public Type Type { get; set; }
    }
}