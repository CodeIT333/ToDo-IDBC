using Application.ToDoItems;
using Application.ToDoItems.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("todo-items")]
    public class ToDoItemController : ControllerBase
    {
        private readonly ToDoItemService _toDoItemService;
        public ToDoItemController(
            ToDoItemService toDoItemService
            )
        {
            _toDoItemService = toDoItemService;
        }

        [HttpGet]
        [SwaggerResponse(200, Type = typeof(List<ToDoItemListDTO>))]
        public async Task<ActionResult<List<ToDoItemListDTO>>> ListToDoItemsAsync([FromQuery] bool isDone = false)
        {
            var data = await _toDoItemService.ListToDoItemsAsync(isDone);
            return Ok(data);
        }
    }
}
