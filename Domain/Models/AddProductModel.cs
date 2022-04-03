namespace ProductAPI.Domain.Models
{
    public class AddProductModel
    {
        public string Name { get; set; }
        public byte TypeId { get; set; }
        public string Category { get; set; }
    }
}