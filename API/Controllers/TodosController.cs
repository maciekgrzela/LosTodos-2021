using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Todo;
using Application.Responses;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodosController : BaseController
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() 
        {
            var todos = await TodoService.GetAllAsync();
            var resource = Mapper.Map<Response<List<Todo>>, Response<List<TodoResource>>>(todos);
            return HandleResult(resource);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var todo = await TodoService.GetAsync(id);
            var resource = Mapper.Map<Response<Todo>, Response<TodoResource>>(todo); 
            return HandleResult(resource);
        }
        
        [HttpGet("stats/{days}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetProductivityStats(int days)
        {
            var stats = await TodoService.GetProductivityStats(days);
            var resource = Mapper.Map<Response<ProductivityStat>, Response<ProductivityStatResource>>(stats); 
            return HandleResult(resource);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTodoResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var todoToSave = Mapper.Map<SaveTodoResource, Todo>(resource);
            var todoSaved = await TodoService.SaveAsync(todoToSave);
            
            return HandleResult(todoSaved);
        }
        
        [HttpPost("list")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> SaveListAsync([FromBody] List<SaveTodoResource> resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var todosToSave = Mapper.Map<List<SaveTodoResource>, List<Todo>>(resource);
            var todosSaved = await TodoService.SaveListAsync(todosToSave);

            return HandleResult(todosSaved);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTodoResource resource, Guid id){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var todoToUpdate = Mapper.Map<SaveTodoResource, Todo>(resource);
            var todoUpdated = await TodoService.UpdateAsync(todoToUpdate, id);

            return HandleResult(todoUpdated);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            
            var todoDeleted = await TodoService.DeleteAsync(id);
            return HandleResult(todoDeleted);
        }

    }
}