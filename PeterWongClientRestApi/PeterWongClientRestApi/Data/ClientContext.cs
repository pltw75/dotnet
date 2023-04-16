using Microsoft.EntityFrameworkCore;
using PeterWongClientRestApi.Models;

namespace PeterWongClientRestApi.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }

        public DbSet<ClientModel> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientModel>().ToTable("Client");
        }
    }
}
