using System.ComponentModel.DataAnnotations;
using TravelAPI.Models;

namespace PublicTravelApi.Models
{
    public class Restaurant
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string PhoneNumber { get; set; }
        public string RestrauntEmail { get; set; }
        public string Opens { get; set; }
        public string Closes { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public decimal Price { get; set; }
        public string ReserveUrl { get; set; }
        public string ImageUrl { get; set; }
        public string ReservePhoneNumberUrl { get; set; }
        public List<RestaurantReview> RestaurantReview { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
