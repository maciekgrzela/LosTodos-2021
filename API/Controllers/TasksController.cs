using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Task;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TasksController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITaskService taskService;

        public TasksController(IMapper mapper, ITaskService taskService)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var tasks = await taskService.GetAllAsync();
            var tasksResource = mapper.Map<List<Domain.Models.Task>, List<Application.Resources.Task.TaskResource>>(tasks);
            return Ok(tasksResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await taskService.GetAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            var taskResource = mapper.Map<Domain.Models.Task, Application.Resources.Task.TaskResource>(response.Value); 
            return Ok(taskResource);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTaskResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var task = mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);
            var response = await taskService.SaveAsync(task);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }
}