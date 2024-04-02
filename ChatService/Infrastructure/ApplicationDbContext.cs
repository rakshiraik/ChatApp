using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ChatService.Repository
{
    public partial class ApplicationDbContext
    {

        public readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : this(options)
        {

            this._configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = this._configuration.GetSection("ConnectionStrings:ChatMessageContextConnection").Value;
            optionsBuilder.UseSqlServer(connection);
        }
        public async Task<int> SaveAsync()
        {
            return await this.SaveChangesAsync();
        }
    }
}
