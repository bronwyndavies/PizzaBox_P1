using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaBox.Domain.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
		public int StoreId { get; set; }
		public DateTime OrderDate { get; set; }
		public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Store Store { get; set; }
		public virtual List<Pizza> Pizzas { get; set; }

		public Order()
		{
            Pizzas = new List<Pizza>();
            OrderDate = DateTime.Now;
        }
		public decimal GetTotal()
		{
			decimal total = 0m;
			foreach (var pizza in Pizzas)
			{
				total += pizza.GetCost();
			}
			return total;
		}
	}
}
