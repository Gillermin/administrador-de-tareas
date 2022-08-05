using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRequest _taskRequest;

        public TaskController(ITaskRequest taskRequest)
        {
            _taskRequest = taskRequest;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            return Ok(await _taskRequest.GetAllTasks());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            return Ok(await _taskRequest.GetTask(id));
        }
        [HttpPost]
        public async Task<IActionResult> InsertTask([FromBody] Tasks tasks)
        {
            if (tasks == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _taskRequest.InsertTask(tasks);

            return Created("Insertado con éxito!", result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] Tasks tasks)
        {
            if (tasks == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _taskRequest.UpdateTask(tasks);

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskRequest.DeleteTask(id);

            return NoContent();
        }
        [Route("GetCollaborators")]
        [HttpGet]
        public async Task<IActionResult> GetCollaborators()
        {
            return Ok(await _taskRequest.GetCollaborators());
        }
        [Route("Filter")]
        [HttpPost]
        public async Task<IActionResult> GetTasksFiltred([FromBody] Filter filter)
        {
            return Ok(await _taskRequest.GetTasksFiltred(filter));
        }
    }
}
