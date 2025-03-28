using APP.Models;
using System.Net.Http.Json;

namespace APP.Services
{
    public class ToDoItemService(HttpClient httpClient)
    {
        public async Task<List<ToDoItemList>> ListToDoItemsAsync(bool isDone)
        {
            return await httpClient.GetFromJsonAsync<List<ToDoItemList>>($"https://localhost:7066/todo-items?isDone={isDone}");
        }

        public async Task<HttpResponseMessage> CreateToDoItemAsync(ToDoItemCreate newItem)
        {
            return await httpClient.PostAsJsonAsync("https://localhost:7066/todo-items", newItem);
        }
    }
}
