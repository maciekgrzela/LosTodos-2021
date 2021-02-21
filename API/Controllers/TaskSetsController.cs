using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.TaskSet;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/todolist")]
    public class TaskSetsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITaskSetService taskSetService;

        public TaskSetsController(IMapper mapper, ITaskSetService taskSetService)
        {
            this.taskSetService = taskSetService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() 
        {
            var taskSets = await taskSetService.GetAllAsync();
            var taskSetsResource = mapper.Map<List<TaskSet>, List<TaskSetResource>>(taskSets);
            return Ok(taskSetsResource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpGet("my")]
        public async Task<IActionResult> GetAllForUserAsync(){
            var taskSets = await taskSetService.GetAllForUserAsync();

            if(!taskSets.Success){
                return BadRequest(taskSets.Message);
            }

            var resource = mapper.Map<List<TaskSet>, List<MyTaskSetResource>>(taskSets.Value);

            return Ok(resource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await taskSetService.GetAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            var taskSetResource = mapper.Map<TaskSet, TaskSetResource>(response.Value); 
            return Ok(taskSetResource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTaskSetResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var taskSet = mapper.Map<SaveTaskSetResource, TaskSet>(resource);
            var response = await taskSetService.SaveAsync(taskSet);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return Ok(response.Value.Id);
        }


        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTaskSetResource resource, Guid id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var taskSet = mapper.Map<SaveTaskSetResource, TaskSet>(resource);
            var response = await taskSetService.UpdateAsync(taskSet, id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return Ok(response.Value.Id);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            var response = await taskSetService.DeleteAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }
}