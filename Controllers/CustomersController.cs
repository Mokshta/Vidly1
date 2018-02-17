using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose (bool disposing)
        {
            _context.Dispose();
        }



        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            //Include- method is used when referencing Customers , (c => c.MembershipType) - expression that deteremines target property

            //This customers property is a DBset but defined in DBContext, with this we can get all customers in the
            //database
            //when this line is executed, entity framework is not going into database, this is called deffered execution
            //The query is executed when we iterate over the customers object

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            //above query will be immediately executed because of - SingleorDefault extension

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

       
    }
}