using Microsoft.EntityFrameworkCore;
using PublicTravelApi.Models;
using System.Collections.Generic;
using TravelAPI.Models;

namespace TravelAPI.DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {

        }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Restaurant> restraunts { get; set; }
        public DbSet<HotelReview> hotelReviews { get; set; }
        public DbSet<RestaurantReview> restrauntReviews { get; set; }
    }

}
