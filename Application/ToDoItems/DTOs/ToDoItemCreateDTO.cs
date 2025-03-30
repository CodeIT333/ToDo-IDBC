using Domain.ToDoItems;
using Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Application.ToDoItems.DTOs
{
    public class ToDoItemCreateDTO
    {
        [Required(ErrorMessage = ErrorMessages.REQUIRED_TO_DO_ITEM_NAME)]
        [MaxLength(64, ErrorMessage = ErrorMessages.TOO_LONG_TO_DO_ITEM_NAME)]
        public string name { get; set; }
        [MaxLength(256, ErrorMessage = ErrorMessages.TOO_LONG_TO_DO_ITEM_DESCRIPTION)]
        public string? description { get; set; }
        [Required(ErrorMessage = ErrorMessages.REQUIRED_TO_DO_ITEM_PRIORITY)]
        [Range(1, 3, ErrorMessage = ErrorMessages.INVALID_TO_DO_ITEM_PRIORITY)]
        public ToDoItemPriority priority  { get; set; }
    }
}
