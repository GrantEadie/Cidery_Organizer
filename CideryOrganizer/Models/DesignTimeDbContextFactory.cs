using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CideryOrganizer.Models
{
  public class CideryOrganizerContextFactory : IDesignTimeDbContextFactory<CideryOrganizerContext>
  {
    CideryOrganizerContext IDesignTimeDbContextFactory<CideryOrganizerContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

      var builder = new DbContextOptionsBuilder<CideryOrganizerContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new CideryOrganizerContext(builder.Options);
    }
  }
}