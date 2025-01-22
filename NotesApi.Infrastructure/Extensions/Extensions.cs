using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApi.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection service, IConfiguration config)
        {
            service.AddDbContext<NotesDbContext>(o =>
            {
                o.UseNpgsql(config.GetConnectionString("PostgreDb"));
            });
            return service;
        }
    }
}
