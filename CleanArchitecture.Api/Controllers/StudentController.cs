using System;
using System.Threading.Tasks;
using CleanArchitecture.Api.Models;
using CleanArchitecture.Api.Swagger;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.SortProviders;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Students;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
//[Authorize]
[Route("/api/v1/[controller]")]
public sealed class StudentController : ApiController
{
    private readonly IStudentService _StudentService;

    public StudentController(
        INotificationHandler<DomainNotification> notifications,
        IStudentService StudentService) : base(notifications)
    {
        _StudentService = StudentService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Students")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<StudentViewModel>>))]
    public async Task<IActionResult> GetAllStudentsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<StudentViewModelSortProvider, StudentViewModel, Student>]
        SortQuery? sortQuery = null)
    {
        var Students = await _StudentService.GetAllStudentsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Students);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Student by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<StudentViewModel>))]
    public async Task<IActionResult> GetStudentByIdAsync([FromRoute] Guid id)
    {
        var Student = await _StudentService.GetStudentByIdAsync(id);
        return Response(Student);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Student")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentViewModel Student)
    {
        var StudentId = await _StudentService.CreateStudentAsync(Student);
        return Response(StudentId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Student")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateStudentViewModel>))]
    public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentViewModel Student)
    {
        await _StudentService.UpdateStudentAsync(Student);
        return Response(Student);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Student")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteStudentAsync([FromRoute] Guid id)
    {
        await _StudentService.DeleteStudentAsync(id);
        return Response(id);
    }
}