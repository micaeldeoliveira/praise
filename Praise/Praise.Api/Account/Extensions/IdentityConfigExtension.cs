using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Praise.Api.Account.Data;

namespace Praise.Api.Account.Extensions;

public static class IdentityConfigExtension
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("'DefaultConnection' não foi encontrada.");
        
        builder.Services.AddDbContext<AccountDbContext>(
            options => options.UseSqlServer(connectionString));

        builder.Services.AddAuthorization();

        builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<AccountDbContext>();

    }

    public static void UseConfigurationIdentity(this WebApplication app)
    {
        app.MapIdentityApi<IdentityUser>();
    }
}
