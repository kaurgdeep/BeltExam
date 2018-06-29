using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BeltExam;
using BeltExam.Models;

namespace BeltExam
{
    public class Auction 
    {
        [Key]            
        public int AuctionId {get;set;}
        [Required(ErrorMessage = "Product Name must be greater than 3")]
        [MinLength(3)]
        public string Product {get; set;}

        [Required]
        [Range(1,10000000000000, ErrorMessage = "start Bid must be grater than zero")]
        public int Startingbid  {get; set;}

        [Required]
        [MyDate(ErrorMessage = "Must have a date in the future")]
        
        public DateTime Enddate {get; set;}

        [Required(ErrorMessage = "Description must be at least 10 characters")]
        [MinLength(10)]
        public string Description {get; set;}

        public int UserId {get; set;}

        public User User {get; set;} 
        
        public List<Bid> AuctionHaveBids {get;set;}

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public Auction()
        {
            
            AuctionHaveBids = new List<Bid>();
        }

        public class MyDateAttribute : ValidationAttribute
            {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
            }    
    
    
    }
}