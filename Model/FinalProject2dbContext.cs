using Microsoft.EntityFrameworkCore;
using Model.DomainModels;

namespace Model
{
    public class FinalProject2dbContext : DbContext
    {
        public FinalProject2dbContext(DbContextOptions options) : base(options)
        {
            
        }

        public FinalProject2dbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.;Initial Catalog=FinalProjectDb;Integrated Security=True;MultipleActiveResultSets=True;Encrypt=False");
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Product> Product {  get; set; }

    }
}
