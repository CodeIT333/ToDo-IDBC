using Application.ToDoItems;
using Application.ToDoItems.DTOs;
using Domain.Commons.Models;
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

        [HttpPost]
        [SwaggerResponse(201)]
        [SwaggerResponse(400, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateToDoItemAsync(ToDoItemCreateDTO dto)
        {
            await _toDoItemService.CreateToDoItemAsync(dto);
            return StatusCode(201);
        }

        [HttpPut("mark-done/{id}")]
        [SwaggerResponse(204)]
        [SwaggerResponse(404, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> UpdateToDoItemAsync([FromRoute] int id)
        {
            await _toDoItemService.UpdateToDoItemAsync(id);
            return NoContent();
        }
    }
}
