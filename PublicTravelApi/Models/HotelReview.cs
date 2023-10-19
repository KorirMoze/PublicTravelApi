using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAPI.Models
{
    public class HotelReview
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public int HotelId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey("HotelId")]
        public Hotel hotel { get; set; }
    }
}
