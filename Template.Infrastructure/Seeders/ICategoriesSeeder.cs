namespace Template.Infrastructure.Seeders
{
	public interface ICategoriesSeeder
	{
		Task Seed();
		public Task SetCategoriesImages();
		public Task SetSubCategoriesImages();
	}
}
