using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace PrdocuctApplication
{
    public static class OrderApplicationConfigration
    {
        public static IServiceCollection AddOrderApplication(this IServiceCollection services)
        {
            return services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
