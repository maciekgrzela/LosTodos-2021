using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Tag;
using Application.Responses;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TagsController : BaseController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync(){
            
            var tags = await TagService.GetAllAsync();
            var resource = Mapper.Map<Response<List<Tag>>, Response<List<TagResource>>>(tags);

            return HandleResult(resource);
        }

        [HttpGet("my")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetAllForUserAsync(){
            
            var tags = await TagService.GetAllForUserAsync();
            var resource = Mapper.Map<Response<List<Tag>>, Response<List<MyTagResource>>>(tags);
        
            return HandleResult(resource);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var tag = await TagService.GetAsync(id);
            var resource = Mapper.Map<Response<Tag>, Response<TagResource>>(tag); 
            return HandleResult(resource);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTagResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var saveTag = Mapper.Map<SaveTagResource, Tag>(resource);
            var tagSaved = await TagService.SaveAsync(saveTag);
            
            return HandleResult(tagSaved);
        }
        
        [HttpPost("add/to/todoset")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<IActionResult> AddToTodoSetAsync([FromBody] AddTagsToTodoSetResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var tagsToAdd = Mapper.Map<AddTagsToTodoSetResource, AddTagsToTodoSet>(resource);
            var tagsAdded = await TagService.AddTagsToTodoSetAsync(tagsToAdd);

            return HandleResult(tagsAdded);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTagResource resource, Guid id){
            
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }
        
            var tagToUpdate = Mapper.Map<SaveTagResource, Tag>(resource);
            var tagUpdated = await TagService.UpdateAsync(tagToUpdate, id);

            return HandleResult(tagUpdated);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            var tagDeleted = await TagService.DeleteAsync(id);
            return HandleResult(tagDeleted);
        }

    }
}