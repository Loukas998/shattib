namespace Template.Domain.Repositories
{
	public interface IStatisticsRepository
	{
		public Task<int> GetNumberOfProductsAsync();
		public Task<int> GetNumberOfOrdersAsync();
		public Task<int> GetNumberOfClientsAsync();
		public Task<int> GetNumberOfBusinessesAsync();
	}
}
