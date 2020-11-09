using MobileApplication.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication.Services
{
	public interface IRestService
	{
		Task<List<Transaction>> RefreshDataAsync();

		Task SaveTodoItemAsync(TodoItem item, bool isNewItem);

		Task DeleteTodoItemAsync(string id);
	}
}
