using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels.UserCalendars;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Cors;
using CleanArchitecture.Application.Services;
namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    //[Authorize] // Uncomment if authorization is needed
    [Route("api/v1/[controller]")]
    public sealed class UserCalendarController : ApiController
    {
        private readonly IUserCalendarService _userCalendarService;

        public UserCalendarController(
            INotificationHandler<DomainNotification> notifications,
            IUserCalendarService userCalendarService) : base(notifications)
        {
            _userCalendarService = userCalendarService;
        }

      

   

        [HttpPost]
        [SwaggerOperation("Create a new UserCalendar")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        public async Task<IActionResult> CreateUserCalendarAsync([FromBody] CreateUserCalendarViewModel userCalendar)
        {
            var userCalendarId = await _userCalendarService.CreateUserCalendarAsync(userCalendar);
            return Response(userCalendarId);
        }
         [HttpGet("ByUserId/{userId}")]
        [SwaggerOperation("Get UserCalendars by userId")]
        [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<UserCalendarViewModel>>))]
        public async Task<IActionResult> GetUserCalendarsByUserIdAsync(
            [FromRoute] Guid userId,
            [FromQuery] int status = 1,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] [SortableFieldsAttribute<UserCalendarViewModelSortProvider, UserCalendarViewModel, UserCalendar>]
            SortQuery? sortQuery = null,
            [FromQuery] bool includeDeleted = false,
            [FromQuery] string? searchTerm = "")
        {
            var userCalendars = await _userCalendarService.GetUserCalendarsByUserIdAsync(
                userId, status,pageNumber,pageSize,sortQuery,includeDeleted,searchTerm);

            return Ok(new ResponseMessage<PagedResult<UserCalendarViewModel>>
            {
                Success = true,
                Data = userCalendars
            });
        }


        
        //[HttpPut]
        //[SwaggerOperation("Update an existing UserCalendar")]
        //[SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateUserCalendarViewModel>))]
        //public async Task<IActionResult> UpdateUserCalendarAsync([FromBody] UpdateUserCalendarViewModel userCalendar)
        //{
        //    await _userCalendarService.UpdateUserCalendarAsync(userCalendar);
        //    return Response(userCalendar);
        //}

        //[HttpDelete("{id}")]
        //[SwaggerOperation("Delete an existing UserCalendar")]
        //[SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
        //public async Task<IActionResult> DeleteUserCalendarAsync([FromRoute] Guid id)
        //{
        //    await _userCalendarService.DeleteUserCalendarAsync(id);
        //    return Response(id);
        //}
    }
}
