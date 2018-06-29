using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeltExam.Models;

namespace BeltExam
{
    public class User 
    {
        [Key]
        public int UserId {get; set;}


        [Required(ErrorMessage   = "Must have name")]
        [MinLength(2)]
        public string FirstName {get; set;}
        
        
        [Required(ErrorMessage = "Must have LastName")]
        [MinLength(2)]
        public string LastName {get; set;}


        [MinLength(3), MaxLength(20)]
        [Required(ErrorMessage = "Username must be greater than 3 but less than 20 chracters")]
        // [RegularExpression("^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+.[a-zA-Z]+$")]
        public string Email {get; set;}


        [Required(ErrorMessage = "Must have 8 characters password")]
        [MinLength(8)]
        public string Password {get; set;}

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<Auction> UserHaveAuctionList { get; set; }

        public List<Bid> UserMakeBidList { get; set; }


        public User() {
            UserHaveAuctionList = new List<Auction>();
            UserMakeBidList = new List<Bid>();
             
        }
    }
}