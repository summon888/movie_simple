using Application.Interfaces;
using Application.Services;
using Domain.Core.Bus;
using Domain.Core.Events;
using Domain.Core.Interfaces;
using Domain.Core.Notifications;
using Domain.Customers.CommandHandlers;
using Domain.Customers.Commands;
using Domain.Customers.EventHandlers;
using Domain.Customers.Events;
using Domain.Customers.Interfaces;
using Domain.Movies.Commands;
using Domain.Movies.EventHandlers;
using Domain.Movies.Events;
using Domain.Movies.Interfaces;
using Identity.Authorization;
using Identity.Models;
using Identity.Services;
using Infrastructure.EventSourcing;
using Infrastructure.Repository.Customers;
using Infrastructure.Repository.EventSourcing;
using Infrastructure.Repository.Movies;
using Infrastructure.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddHttpContextAccessor();

            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();
            services.AddScoped<IMovieAppService, MovieAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Domain - Events - Customers
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Events - Movies
            services.AddScoped<INotificationHandler<MovieAddNewEvent>, MovieEventHandler>();
            services.AddScoped<INotificationHandler<MovieUpdatedEvent>, MovieEventHandler>();
            services.AddScoped<INotificationHandler<MovieRemovedEvent>, MovieEventHandler>();

            // Domain - Commands - Customers
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            // Domain - Commands - Movies
            services.AddScoped<IRequestHandler<AddNewMovieCommand, bool>, MovieCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateMovieCommand, bool>, MovieCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveMovieCommand, bool>, MovieCommandHandler>();

            // Domain - Providers, 3rd parties
            //services.AddScoped<IHttpProvider, HttpProvider>();
            //services.AddScoped<IMailProvider, MailProvider>();
            //services.AddScoped<INotificationProvider, NotificationProvider>();
            //services.AddScoped<IWebhookProvider, WebhookProvider>();
            //services.AddScoped<ICronProvider, CronProvider>();
            //services.AddScoped<IOfficeProvider, OfficeProvider>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<IJwtFactory, JwtFactory>();
        }
    }
}
