using APP.Models;
using System.Net.Http.Json;

namespace APP.Services
{
    public class ToDoItemService(HttpClient httpClient)
    {
        public async Task<List<ToDoItemList>> ListToDoItemsAsync(bool isDone)
        {
            return await httpClient.GetFromJsonAsync<List<ToDoItemList>>($"/todo-items?isDone={isDone}");
        }

        public async Task<HttpResponseMessage> CreateToDoItemAsync(ToDoItemCreate newItem)
        {
            return await httpClient.PostAsJsonAsync("/todo-items", newItem);
        }

        public async Task<HttpResponseMessage> UpdateToDoItemAsync(int id)
        {
            return await httpClient.PutAsJsonAsync($"/todo-items/mark-done/{id}", new { });
        }
    }
}
