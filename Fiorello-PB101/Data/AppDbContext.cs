
using Fiorello_PB101.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }   
        
        public DbSet<SliderInfo> SliderInfos { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Expert> Experts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);





            modelBuilder.Entity<Blog>().HasData(
            new Blog
            {
                Id = 1,
                Title = "Title 1",
                Description = "Reshadin blogu",
                Image= "blog-feature-img-1.jpg",
                CreatedDate = DateTime.Now
            },
             new Blog
             {
                 Id = 2,
                 Title = "Title 2",
                 Description = "Kamranin blogu",
                 Image = "blog-feature-img-3.jpg",
                 CreatedDate = DateTime.Now
             },
              new Blog
              {
                  Id = 3,
                  Title = "Title 3",
                  Description = "Ilqarin blogu",
                  Image = "blog-feature-img-4.jpg",
                  CreatedDate = DateTime.Now
              }
                );


            modelBuilder.Entity<Expert>().HasData(
           new Expert
           {
               Id = 1,
               Image = "h3-team-img-1.png ",
               Name = "CRYSTAL BROOKS",
               Position = "FLORIST",
               CreatedDate = DateTime.Now
           },
           new Expert
           {
               Id = 2,
               Image = "h3-team-img-2.png ",
               Name = "SHIRLEY HARRIS",
               Position = "Manager",
               CreatedDate = DateTime.Now
           },
           new Expert
           {
               Id = 3,
               Image = "h3-team-img-3.png ",
               Name = "BEVERLY CLARK",
               Position = "Florist",
               CreatedDate = DateTime.Now
           },
           new Expert
           {
               Id = 4,
               Image = "h3-team-img-4.png ",
               Name = "AMANDA WATKINS ",
               Position = "Florist",
               CreatedDate = DateTime.Now
           });

        }







    }
}
