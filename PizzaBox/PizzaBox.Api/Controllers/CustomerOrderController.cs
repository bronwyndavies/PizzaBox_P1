using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaBox.Domain.Entities;

namespace PizzaBox.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerOrderController : ControllerBase
    {
   
        private readonly ILogger<CustomerOrderController> _logger;
        private readonly PizzaBoxDbContext _context;
        public CustomerOrderController(ILogger<CustomerOrderController> logger, PizzaBoxDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Order> Get(int id)
        {
            var customerOrders = _context.Orders.Include(o => o.Customer).
                Include(o => o.Store)
                .Include(o => o.Pizzas).
                ThenInclude(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
                .Include(o => o.Pizzas).ThenInclude(p => p.Crust)
                .Include(o => o.Pizzas).ThenInclude(p => p.Size)
                .Where(o => o.CustomerId == id).ToList(); //Calls SQL Server immediately to execute the query and return result

            IEnumerable<Order> IEnumerableCustomerOrders = _context.Orders.Include(o => o.Customer).
                Include(o => o.Store)
                .Include(o => o.Pizzas).
                ThenInclude(p => p.PizzaToppings).ThenInclude(pt => pt.Topping)
                .Include(o => o.Pizzas).ThenInclude(p => p.Crust)
                .Include(o => o.Pizzas).ThenInclude(p => p.Size)
                .Where(o => o.CustomerId == id); //does not call SQL server

            foreach (var order in IEnumerableCustomerOrders) // Oh, someone is trying to use this list now, I better call SQL and execute the query and return result
            {

            }
            return customerOrders;
        }
    }
}
