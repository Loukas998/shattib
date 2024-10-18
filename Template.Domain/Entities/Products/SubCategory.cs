namespace Template.Domain.Entities.Products
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int CategoryId { get; set; } = default!;

        public List<Product> Products { get; set; } = new();
    }
}
