using MobileApplication.Models;
using DbEntities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileApplication.Services.Rest;

namespace MobileApplication.Services
{
	public class TodoGoalManager
	{
		IRestService<Goal> restService;

		public TodoGoalManager(IRestService<Goal> service)
		{
			restService = service;
		}

		public Task<List<Goal>> GetTasksAsync()
		{
			return restService.RefreshDataAsync();
		}

		public Task SaveTaskAsync(Goal item)
		{
			return restService.SaveItemAsync(item);
		}

		public Task DeleteTaskAsync(Goal item)
		{
			return restService.DeleteItemAsync(item.Id);
		}
	}
}
