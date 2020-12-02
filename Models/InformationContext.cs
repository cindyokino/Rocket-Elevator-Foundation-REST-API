using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;




namespace Rocket_Elevator_RESTApi.Models
{
    public class InformationContext : DbContext
    {
        public InformationContext(DbContextOptions<InformationContext> options)
            : base(options)
        {
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
    /////////This is where relations between tables in DB are specified
               
                modelBuilder.Entity<Building>()
                .HasKey(b => b.id);

                modelBuilder.Entity<Battery>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Column>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Elevator>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Lead>()
                .HasKey(x => x.id);

                modelBuilder.Entity<Intervention>()  // New - for intervention
                .HasKey(x => x.id);

                modelBuilder.Entity<Customer>()  // New - for customers
                .HasKey(x => x.id);

                
                modelBuilder.Entity<Customer>()
                .HasMany(x => x.Buildings) //customer has_many :buildings
                .WithOne( y => y.Customer)
                .HasForeignKey(z => z.customer_id);
                
                modelBuilder.Entity<Building>()
                .HasMany(x => x.Batteries) //building has_many    :batteries
                .WithOne( y => y.Building)
                .HasForeignKey(z => z.building_id);                
                
                modelBuilder.Entity<Battery>()
                .HasMany(x => x.Columns) //battery has_many :columns
                .WithOne(y => y.Battery)
                .HasForeignKey(z => z.battery_id);                

                modelBuilder.Entity<Column>() 
                .HasOne(x => x.Battery) //column belongs_to  :battery
                .WithMany(y => y.Columns)
                .HasForeignKey(z => z.battery_id);

                modelBuilder.Entity<Column>() 
                .HasMany(x => x.Elevators) //column has_many    :elevators
                .WithOne(y => y.Column)
                .HasForeignKey(z => z.column_id);

                modelBuilder.Entity<Elevator>() 
                .HasOne(x => x.Column) //elevator belongs_to :column
                .WithMany(y => y.Elevators)
                .HasForeignKey(z => z.column_id);                
            }

        public DbSet<Elevator> elevators { get; set; }
        public DbSet<Column> columns { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Building> buildings  { get; set; }
        public DbSet<Lead> leads { get; set; }
        public DbSet<Quote> quotes { get; set; }
        public DbSet<Intervention> interventions { get; set; }  // New - for intervention
        public DbSet<Customer> customers { get; set; }  // New - for customer
    }
}