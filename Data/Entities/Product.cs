namespace ProductAPI.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte TypeId { get; set; }
        public string Category { get; set; }
    }
}