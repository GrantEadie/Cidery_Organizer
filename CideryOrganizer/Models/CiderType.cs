namespace CideryOrganizer.Models
{
  public class CiderType
    {       
        public int CiderTypeId { get; set; }
        public int CiderId { get; set; }
        public int TypeId { get; set; }
        public Cider Cider { get; set; }
        public Type Type { get; set; }
    }
}