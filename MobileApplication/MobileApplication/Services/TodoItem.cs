﻿using MobileApplication.Models;
using DbEntities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication.Services
{
	public class TodoItemManager
	{
		IRestService<Transaction> restService;

		public TodoItemManager(IRestService<Transaction> service)
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
