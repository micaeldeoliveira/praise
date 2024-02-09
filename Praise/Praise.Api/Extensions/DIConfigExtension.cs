using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Praise.Api.Account.Data;
using Praise.Application.Interfaces.Notifications;
using Praise.Application.Interfaces.Repositories;
using Praise.Application.Interfaces.Services;
using Praise.Application.Notifications;
using Praise.Application.Services;
using Praise.Infra.Contexts;
using Praise.Infra.Repositories;

namespace Praise.Api.Extensions;

public static class DIConfigExtension
{
    public static void ConfigureDependencyInjection(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("'DefaultConnection' não foi encontrada.");

        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<INotification, Notification>();

        builder.Services.AddScoped<IMusicService, MusicService>();

        builder.Services.AddTransient<IMusicRepository, MusicRepository>();

    }
}
