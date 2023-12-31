﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PublicTravelApi.Models
{
    public class RestaurantReview
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
        public int RestrauntId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        [ForeignKey("RestrauntId")]
        public Restaurant restraunt { get; set; }
    }
}
