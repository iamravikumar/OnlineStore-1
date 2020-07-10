using Microsoft.EntityFrameworkCore;

namespace OnlineStore.API.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrdersDbContext(DbContextOptions options): base(options)
        {
            
        }
    }
}
