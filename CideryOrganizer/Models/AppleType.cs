namespace CideryOrganizer.Models
{
  public class AppleType
    {       
        public int AppleTypeId { get; set; }
        public int AppleId { get; set; }
        public int TypeId { get; set; }
        public Apple Apple { get; set; }
        public Type Type { get; set; }
    }
}