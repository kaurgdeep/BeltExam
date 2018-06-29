using System;
using System.ComponentModel.DataAnnotations;
using BeltExam.Models;

namespace BeltExam
{
    public class Bid
    {
        [Key]
        public int BidId {get;set;}

        public int Yourbid {get; set;}

        public int UserId {get;set;}
        public User User {get;set;}

        public int AuctionId {get;set;}
        public Auction Auction {get;set;}

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        
       
    }
}