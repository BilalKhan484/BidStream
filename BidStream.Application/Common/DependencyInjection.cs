using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace BidStream.Application.Common
{
    public static  class DependencyInjection
    {
        
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                return services;
            }
    }
}
