using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Praise.Api.Account.Data;

public class AccountDbContext : IdentityDbContext<IdentityUser>
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) :
       base(options)
    { }
}
