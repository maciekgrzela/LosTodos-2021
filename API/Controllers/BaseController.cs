using System.Net;
using Application.Responses;
using Application.Services;
using Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        private IMapper mapper;
        private ITagService tagService;
        private ITodoService todoService;
        private ITodoSetService todoSetService;
        private IUserService userService;
        
        protected IMapper Mapper => mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ITagService TagService => tagService ??= HttpContext.RequestServices.GetService<ITagService>();
        protected ITodoService TodoService => todoService ??= HttpContext.RequestServices.GetService<ITodoService>();
        protected ITodoSetService TodoSetService => todoSetService ??= HttpContext.RequestServices.GetService<ITodoSetService>();
        protected IUserService UserService => userService ??= HttpContext.RequestServices.GetService<IUserService>();


        protected IActionResult HandleResult<T>(Response<T> response)
        {
            switch (response.Result)
            {
                case ResponseResult.BadRequestStructure:
                    return BadRequest(response.ErrorMessage);
                case ResponseResult.UserIsNotAuthorized:
                    return Unauthorized(response.ErrorMessage);
                case ResponseResult.AccessDenied:
                    return StatusCode((int) HttpStatusCode.Forbidden, response.ErrorMessage);
                case ResponseResult.ResourceDoesntExist:
                    return NotFound(response.ErrorMessage);
                case ResponseResult.InternalError:
                    return StatusCode((int) HttpStatusCode.InternalServerError, response.ErrorMessage);
                case ResponseResult.DataObtained:
                    return Ok(response.Value);
                case ResponseResult.Created: 
                case ResponseResult.Deleted: 
                case ResponseResult.Updated:
                    return NoContent();
                default:
                    return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
        
        
    }
}