using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minsait.Examples.Application.Commands;
using Minsait.Examples.Application.Commands.Base;
using Minsait.Examples.Application.Handlers;
using Minsait.Examples.Application.Queries;
using Minsait.Examples.Application.Responses;
using Minsait.Examples.Application.Services;
using Minsait.Examples.Application.Validators;
using Minsait.Examples.Domain.Interfaces.Application.Services;
using Minsait.Examples.Domain.Interfaces.Infra.Data.Repositories;
using Minsait.Examples.Infra.Data.Context;
using Minsait.Examples.Infra.Data.Repositories;

namespace Minsait.Examples.CrossCutting.IoC
{
    public static class IoCModule
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IMinsaitTestRepository, MinsaitTestRepository>();

            //services
            services.AddScoped<IMinsaitTestService, MinsaitTestService>();

            //validators
            services.AddScoped<IValidator<CreateMinsaitTestCommand>, CreateMinsaitTestCommandValidator>();

            //commandHandlers
            services.AddScoped<ICommandHandler<CreateMinsaitTestCommand, CreateMinsaitTestResult>, CreateMinsaitTestCommandHandler>();

            //queries
            services.AddScoped<IMinsaitTestQueries, MinsaitTestQueries>();
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyContext>(options =>
            {
                options
                    .UseInMemoryDatabase("MinsaitTestInMemory")
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
    }
}
