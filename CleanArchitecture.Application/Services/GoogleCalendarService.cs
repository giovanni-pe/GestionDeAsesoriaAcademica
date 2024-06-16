//using System;
//using System.Threading.Tasks;
//using CleanArchitecture.Application.Interfaces;
//using CleanArchitecture.Application.Queries.Professors.GetAll;
//using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAll;
//using CleanArchitecture.Application.Queries.AdvisoryContracts.GetAdvisoryContractById;
//using CleanArchitecture.Application.ViewModels;
//using CleanArchitecture.Application.ViewModels.Professors;
//using CleanArchitecture.Application.ViewModels.Sorting;
//using CleanArchitecture.Application.ViewModels.AdvisoryContracts;
//using CleanArchitecture.Domain;
//using CleanArchitecture.Domain.Commands.AdvisoryContracts.CreateAdvisoryContract;
//using CleanArchitecture.Domain.Commands.AdvisoryContracts.DeleteAdvisoryContract;
//using CleanArchitecture.Domain.Commands.AdvisoryContracts.UpdateAdvisoryContract;
//using CleanArchitecture.Domain.Entities;
//using CleanArchitecture.Domain.Extensions;
//using CleanArchitecture.Domain.Interfaces;
//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Logging;
//using System.Threading;

//namespace CleanArchitecture.Application.Services;
///// <summary>
///// By Perez
///// </summary>
//public class GoogleCalendarService : IGoogleCalendarService
//{
//    private readonly IGoogleCalendarSettings _settings;

//    public GoogleCalendarService(IGoogleCalendarSettings settings)
//    {
//        _settings = settings;
//    }

//    public async Task<Event> CreateEvent(Event request, CancellationToken cancellationToken)
//    {
//        UserCredential credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//            new ClientSecrets()
//            {
//                ClientId = _settings.ClientId,
//                ClientSecret = _settings.ClientSecret
//            },
//            _settings.Scope,
//            _settings.User,
//            cancellationToken);

//        var services = new CalendarService(new BaseClientService.Initializer()
//        {
//            HttpClientInitializer = credential,
//            ApplicationName = _settings.ApplicationName,
//        });

//        var eventRequest = services.Events.Insert(request, _settings.CalendarId);
//        var requestCreate = await eventRequest.ExecuteAsync(cancellationToken);
//        return requestCreate;
//    }
//}

