using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.TodoSet;
using Application.Responses;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoSetsController : BaseController
    {

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync() 
        {
            var todoSets = await TodoSetService.GetAllAsync();
            var resource = Mapper.Map<Response<List<TodoSet>>, Response<List<TodoSetResource>>>(todoSets);
            
            return HandleResult(resource);
        }
        
        [HttpGet("my")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetAllForUserAsync(){
            var todoSets = await TodoSetService.GetAllForUserAsync();
            var resource = Mapper.Map<Response<List<TodoSet>>, Response<List<MyTodoSetResource>>>(todoSets);
        
            return HandleResult(resource);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var todoSet = await TodoSetService.GetAsync(id);
            var resource = Mapper.Map<Response<TodoSet>, Response<TodoSetResource>>(todoSet);
            
            return HandleResult(resource);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTodoSetResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var todoSetToSave = Mapper.Map<SaveTodoSetResource, TodoSet>(resource);
            var todoSetSaved = await TodoSetService.SaveAsync(todoSetToSave);

            return HandleResult(todoSetSaved);
        }
        
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTodoSetResource resource, Guid id)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var todoSetToUpdate = Mapper.Map<SaveTodoSetResource, TodoSet>(resource);
            var todoSetUpdated = await TodoSetService.UpdateAsync(todoSetToUpdate, id);

            return HandleResult(todoSetUpdated);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            var todoSetDeleted = await TodoSetService.DeleteAsync(id);

            return HandleResult(todoSetDeleted);
        }

    }
}