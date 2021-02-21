using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Extensions;
using Application.Resources.Tag;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITagService tagService;
        public TagsController(IMapper mapper, ITagService tagService)
        {
            this.tagService = tagService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(){
            var tags = await tagService.GetAllAsync();
            var resource = mapper.Map<List<Tag>, List<TagResource>>(tags);

            return Ok(resource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpGet("my")]
        public async Task<IActionResult> GetAllForUserAsync(){
            var tags = await tagService.GetAllForUserAsync();

            if(!tags.Success){
                return BadRequest(tags.Message);
            }

            var resource = mapper.Map<List<Tag>, List<MyTagResource>>(tags.Value);

            return Ok(resource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await tagService.GetAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            var taskResource = mapper.Map<Tag, TagResource>(response.Value); 
            return Ok(taskResource);
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPost]
        public async Task<IActionResult> SaveAsync([FromBody] SaveTagResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var tag = mapper.Map<SaveTagResource, Tag>(resource);
            var response = await tagService.SaveAsync(tag);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin,RegularUser")]
        [HttpPost("add/to/taskset")]
        public async Task<IActionResult> AddToTaskSetAsync([FromBody] AddTagsToTaskSetResource resource) 
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var tag = mapper.Map<AddTagsToTaskSetResource, AddTagsToTaskSet>(resource);
            var response = await tagService.AddTagsToTaskSetAsync(tag);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SaveTagResource resource, Guid id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrors());
            }

            var tag = mapper.Map<SaveTagResource, Tag>(resource);
            var response = await tagService.UpdateAsync(tag, id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id){
            var response = await tagService.DeleteAsync(id);

            if(!response.Success){
                return BadRequest(response.Message);
            }

            return NoContent();
        }

    }
}