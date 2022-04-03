namespace ProductAPI.Domain.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte TypeId { get; set; }
        public string? TypeName { get; set; }
        public string Category { get; set; }
    }
}