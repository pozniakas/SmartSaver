using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplication.Services.Rest
{
    public interface IRestService<T>
	{
		Task<List<T>> RefreshDataAsync();

		Task SaveItemAsync(T item);

		Task UpdateItemAsync(T item, long id);

		Task DeleteItemAsync(long id);
	}
}
