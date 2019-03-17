using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TodoApi.Models;

namespace TodosApi.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase {
        private TodoContext _context;
        public TodosController(TodoContext context) {
            _context = context;

            if (_context.TodoItems.Count() == 0) {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems() {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetSingleTodoItem(long id) {
            var item = await _context.TodoItems.FindAsync(id);
            
            if (item == null) {
                return NotFound();
            } else {
                return item;
            }
        }
    }
}