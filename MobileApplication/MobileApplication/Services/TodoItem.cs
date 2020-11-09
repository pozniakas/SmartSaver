using MobileApplication.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication.Services
{
	public class TodoItemManager
	{
		IRestService restService;

		public TodoItemManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<Transaction>> GetTasksAsync()
		{
			return restService.RefreshDataAsync();
		}

		public Task SaveTaskAsync(TodoItem item, bool isNewItem = false)
		{
			return restService.SaveTodoItemAsync(item, isNewItem);
		}

		public Task DeleteTaskAsync(TodoItem item)
		{
			return restService.DeleteTodoItemAsync(item.ID);
		}
	}
}
