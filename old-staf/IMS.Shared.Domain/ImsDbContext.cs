using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMS.Shared.Domain;

/// <summary>
/// Ims App DB Domain
/// </summary>
public class ImsDbContext: DomainDbContext
{
    private readonly IConfiguration _configuration;
    
    public ImsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("WebApiDatabase"));
    }
}