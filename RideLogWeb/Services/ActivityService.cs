using Blazored.LocalStorage;
using RideLogWeb.Models;

namespace RideLogWeb.Services;

public class ActivityService
{
    private readonly ILocalStorageService _localStorage;
    private const string Key = "ridelog_activities";

    public ActivityService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    private async Task<List<Activity>> GetAllAsync()
    {
        return await _localStorage
            .GetItemAsync<List<Activity>>(Key) ?? new List<Activity>();
    }

    private async Task SaveAllAsync(List<Activity> activities)
    {
        await _localStorage.SetItemAsync(Key, activities);
    }

    public async Task<List<Activity>> GetActivitiesAsync()
    {
        var all = await GetAllAsync();
        return all.Where(a => !a.IsTemplate)
                  .OrderByDescending(a => a.Date)
                  .ToList();
    }

    public async Task<List<Activity>> GetTemplatesAsync()
    {
        var all = await GetAllAsync();
        return all.Where(a => a.IsTemplate).ToList();
    }

    public async Task SaveActivityAsync(Activity activity)
    {
        var all = await GetAllAsync();

        if (activity.Id == 0)
        {
            activity.Id = all.Count > 0 ? all.Max(a => a.Id) + 1 : 1;
            all.Add(activity);
        }
        else
        {
            var index = all.FindIndex(a => a.Id == activity.Id);
            if (index >= 0) all[index] = activity;
        }

        await SaveAllAsync(all);
    }

    public async Task DeleteActivityAsync(Activity activity)
    {
        var all = await GetAllAsync();
        all.RemoveAll(a => a.Id == activity.Id);
        await SaveAllAsync(all);
    }
}