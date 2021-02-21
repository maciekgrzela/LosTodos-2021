using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Task;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var tasks = await taskService.GetAllAsync();
            var tasksResource = mapper.Map<List<Domain.Models.Task>, List<Application.Resources.Task.TaskResource>>(tasks);
            return Ok(tasksResource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
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

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpGet("stats/{days}")]
        public async Task<IActionResult> GetProductivityStats(int days)
        {
            var response = await taskService.GetProductivityStats(days);
            if(!response.Success){
                return BadRequest(response.Message);
            }

            var taskResource = mapper.Map<ProductivityStat, ProductivityStatResource>(response.Value); 
            return Ok(taskResource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
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

            return Ok(response.Value.Id);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPost("list")]
        public async Task<IActionResult> SaveListAsync([FromBody] List<SaveTaskResource> resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var task = mapper.Map<List<SaveTaskResource>, List<Domain.Models.Task>>(resource);
            var response = await taskService.SaveListAsync(task);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTaskResource resource, Guid id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var task = mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);
            var response = await taskService.UpdateAsync(task, id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            var response = await taskService.DeleteAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }
}