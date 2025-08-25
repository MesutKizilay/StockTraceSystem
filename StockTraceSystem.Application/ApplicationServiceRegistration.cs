using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using StockTraceSystem.Application.Services.AuthServices;
using System.Reflection;
using FluentValidation;

namespace StockTraceSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService,AuthManager>();

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            services.AddMediatR(configuration =>
            {
                configuration.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxNzg3MTg0MDAwIiwiaWF0IjoiMTc1NTY3MzE3OSIsImFjY291bnRfaWQiOiIwMTk4YzY0NDJkM2E3OWQ2YTRjMmI4ZGQyNWYzM2U3NCIsImN1c3RvbWVyX2lkIjoiY3RtXzAxazMzNGRkcWpmYmFnaGdhejlndDZ6NnMwIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.YETEow80J1q5rcZW-MPIHCIg26PEYSRN6wtvgLZ3ruQInOIOEcQL2BJW-8fvPte8vg4RJAB7NmHDmwte-V8at2UBjWS90bTKBR9jPHdK6kAbc2jIwmTwwo_h01n4PAnbuVmWuKbvJ8Zmv5vA84rivTIbY6B70Ik5zVwjWzLAtD3pMmFurunTYTSHpFcc4ASZNkOjW1D-LXXiP6v7FhPMI9xGdOTmjT1Gu0DYdQjPTr1AzLGc1RgoXZRCGeBZiYX11UiK1feA_uc6K05xhQg5CA7KW4Fvl-qiyByIX_Zr3594_izjv9-ETuXo5RiMlRJSfNqDlsJQbbsvPsbUv2EveQ";

                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

        private static IServiceCollection AddSubClassesOfType(this IServiceCollection services,
            Assembly assembly,
            Type type,
            Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
        {
            var X = assembly.GetTypes();
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);

                else
                    addWithLifeCycle(services, type);
            return services;
        }
    }
}