using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileApplication.Services.Rest
{
    public interface IRestService<T>
	{
		Task<List<T>> RefreshDataAsync();

		Task SaveItemAsync(T item, bool isNewItem);

		Task DeleteItemAsync(long id);
	}
}
