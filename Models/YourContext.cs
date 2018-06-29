using Microsoft.EntityFrameworkCore;
namespace BeltExam.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Auction> auctions { get; set; }
        public DbSet<Bid> bids { get; set; }
    }
}
