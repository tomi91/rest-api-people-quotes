using Microsoft.EntityFrameworkCore;
using RestApi.Core.Entities;

#nullable disable

namespace RestApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Quote> Quote { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().HasData(new Person { Id = 1, FirstName = "Andrew", LastName = "Hendrixson" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 2, FirstName = "Coco", LastName = "Chanel" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 3, FirstName = "Albert", LastName = "Einstein" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 4, FirstName = "Brian", LastName = "Tracy" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 5, FirstName = "Grace", LastName = "Coddington" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 6, FirstName = "Henry David", LastName = "Thoreau" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 7, FirstName = "Author", LastName = "Unknown" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 8, FirstName = "Abraham", LastName = "Lincoln" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 9, FirstName = "Robin", LastName = "Sharma" });
            modelBuilder.Entity<Person>().HasData(new Person { Id = 10, FirstName = "Anais", LastName = "Nin" });

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 1, Description = "Anyone who has ever made anything of importance was disciplined.", PersonId = 1 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 2, Description = "Don’t spend time beating on a wall, hoping to transform it into a door.", PersonId = 2 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 3, Description = "Creativity is intelligence having fun.", PersonId = 3 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 4, Description = "Optimism is the one quality more associated with success and happiness than any other.", PersonId = 4 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 5, Description = "Always keep your eyes open. Keep watching. Because whatever you see can inspire you.", PersonId = 5 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 6, Description = "What you get by achieving your goals is not as important as what you become by achieving your goals.", PersonId = 6 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 7, Description = "If the plan doesn’t work, change the plan, but never the goal.", PersonId = 7 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 8, Description = "I destroy my enemies when I make them my friends.", PersonId = 8 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 9, Description = "Don’t live the same year 75 times and call it a life.", PersonId = 9 });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 10, Description = "You cannot save people, you can just love them.", PersonId = 10 });

        }

    }
}