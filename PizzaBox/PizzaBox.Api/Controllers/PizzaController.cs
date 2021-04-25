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
    public class PizzaController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly PizzaBoxDbContext _context;
        public PizzaController(ILogger<CustomerController> logger, PizzaBoxDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public Pizza Get(int id)
        {
            var pizza = _context.Pizzas.Where(p=>p.Id== id).FirstOrDefault();
            return pizza;
        }

        [HttpPost]

        public Pizza Post(Pizza pizza)
        {
            pizza.PizzaToppings = null;
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();

            return pizza;
        }


        [HttpPut]

        public Pizza Put(Pizza pizza)
        {
            pizza.PizzaToppings = null;
            _context.Pizzas.Update(pizza);
            _context.SaveChanges();
            return pizza;
        }
    }
}
