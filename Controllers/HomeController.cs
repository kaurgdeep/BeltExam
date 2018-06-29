using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeltExam.Models;

namespace BeltExam.Controllers
{
    public class HomeController : Controller
    {
         private YourContext _context;
 
        public HomeController(YourContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         [HttpPost]
        [Route ("RegisterUser")]
        public IActionResult RegisterUser (User MyUser, string ConfirmPassword) {
            
            System.Console.WriteLine ("WE HIT REGISTERED USER FUNCTION IN CONTROLLER");
            if(MyUser.Password != ConfirmPassword) {
                System.Console.WriteLine("DAMN HOMIE your passwords dont match **************************");
                ViewBag.PasswordError = $"{MyUser.FirstName} I'm so terribly sorry but I'm a robot and I don't understand why you would type passwords that don't match. THANKS FOR PLAYING. TRY AGAIN!";
                return View ("Index");
            }

            if (ModelState.IsValid) {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                MyUser.Password = Hasher.HashPassword(MyUser, MyUser.Password);
                User ExistingUser = _context.users.SingleOrDefault (u => u.Email == MyUser.Email);
                if (ExistingUser != null) {
                    System.Console.WriteLine (" *************EMAIL ALREADY IN USE**********************");
                    ViewBag.AlreadyInUseEmail = true;
                    // ViewBag.AlreadyInUseEmail = $"{MyUser.Email} is already in the Data base, YOU FUCK!";
                    return View ("Index");
                    // Yo dude Have you ever watched Mike Tyson Mysteries? Its really good show.
                }
                else{
                    _context.Add (MyUser);
                    _context.SaveChanges ();
                    // HttpContext.Session.SetString("UserSessionEmail", MyUser.Email);
                    HttpContext.Session.SetString("userEmail", MyUser.Email);
                    HttpContext.Session.SetInt32("userID", MyUser.UserId);

                    return RedirectToAction("Dashboard");
                }
            } else {
                System.Console.WriteLine ("There were errors adding user returned to index********************");
                return View ("Index");
            }
        }


        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(string email, string Password){
            
            var user = _context.users.SingleOrDefault(u => u.Email == email);
            if(user != null && Password != null){
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, Password)){
                    // HttpContext.Session.SetString("UserSessionEmail", user.Email);
                    HttpContext.Session.SetString("userEmail", email);
                    HttpContext.Session.SetInt32("userID", user.UserId);
                    return RedirectToAction("Dashboard");
                }
                else{
                    System.Console.WriteLine("*************************$$$$$$$$$$$#################################$$$$$$$$$$$$$$************");
                    System.Console.WriteLine("Else for login if password doesnt match");
                    System.Console.WriteLine("*************************$$$$$$$$$$$#################################$$$$$$$$$$$$$$************");

                    ViewBag.loginError = "Wrong password!";
                    return View("Index");
                }
            }
            else{
                System.Console.WriteLine("*************************$$$$$$$$$$$#################################$$$$$$$$$$$$$$************");
                System.Console.WriteLine("Else for login email doesnt exist");
                System.Console.WriteLine("*************************$$$$$$$$$$$#################################$$$$$$$$$$$$$$************");

                ViewBag.loginError = "Email not registered";
                return View("Index");
            }
            
        }

        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if( HttpContext.Session.GetInt32("userID") == null){ //after logout will goto index page
                return RedirectToAction("Index");
            }
            if (HttpContext.Session.GetInt32("wallet") == null) {
                HttpContext.Session.SetInt32("wallet", 1000);
            }
            int? Wallet = HttpContext.Session.GetInt32("wallet");
            ViewBag.wallet = Wallet;
            
            ViewBag.LoggedUser = _context.users.SingleOrDefault(user => (user.UserId == HttpContext.Session.GetInt32("userID")));
            ViewBag.AllAuctions = _context.auctions.Include(a => a.User).Include(a => a.AuctionHaveBids).OrderBy(a => a.Enddate).ToList();
           
            return View();

        }

        [Route("NewAuction")]
        public IActionResult NewAuction() {
            if( HttpContext.Session.GetInt32("userID") == null){ //after logout will goto index page
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreateAuction(Auction NewAuction) {
            var UserEmail = HttpContext.Session.GetString("userEmail");
            User Users = _context.users.SingleOrDefault(u => u.Email == UserEmail);
            

            if(ModelState.IsValid) {
            NewAuction.UserId = Users.UserId;
            _context.Add(NewAuction);
            _context.SaveChanges();
            // System.Console.WriteLine(Users.UserId);

            HttpContext.Session.SetInt32("AuctionID", NewAuction.AuctionId);

            return RedirectToAction("Dashboard");

            }
            else {
                return View("NewAuction");
            }

        }

        [Route("delete/{id}")]
        public IActionResult delete (int id){

            Auction CurrentPost = _context.auctions.SingleOrDefault(p => p.AuctionId == id);//this if check not gonna allows other to delete
            if(CurrentPost.UserId == (int)HttpContext.Session.GetInt32("userID"))//the post which I have created by fowwing the delete route (security perpose)
            {

            Auction deleteAuction = _context.auctions.SingleOrDefault(i => i.AuctionId == id);

            _context.Remove(deleteAuction);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [Route("productdetails/{id}")]
        public IActionResult productdetails (int id) {

            if( HttpContext.Session.GetInt32("userID") == null){ //after logout will goto index page
                return RedirectToAction("Index");
            }
            int? AuctionID = HttpContext.Session.GetInt32("AuctionID");
            Auction auctions = _context.auctions.SingleOrDefault(u => u.AuctionId == id);

            User users = _context.users.SingleOrDefault(ui => ui.UserId == auctions.UserId);

            ViewBag.AllAuctions = _context.auctions.Include(u => u.User).Include(p => p.AuctionHaveBids).ThenInclude(u => u.User).SingleOrDefault(uq => uq.AuctionId == id);


            ViewBag.Auctions = auctions;
            ViewBag.Users = users;

            return View();
        }

        [HttpPost]
        [Route("Home/postbid/{id}")]
        public IActionResult postbid(int id)
        {
            if(ModelState.IsValid) 
            {
        
                Bid addMe = new Bid()
                {
                    UserId = (int)HttpContext.Session.GetInt32("userID"),
                   AuctionId = id
                };

                _context.Add(addMe);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("productdetails");
            
        }

        



        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [Route("dashboardbutton")]
        public IActionResult dashboardbutton()
        {
            return RedirectToAction("Dashboard");
        }
    }
}
