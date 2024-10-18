namespace Template.Domain.Entities.Products
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public List<SubCategory> SubCategories { get; set; } = new();
    }
}
