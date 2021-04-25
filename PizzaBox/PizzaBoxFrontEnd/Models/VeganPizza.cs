using PizzaBox.Domain.Entities;

namespace PizzaBox.Domain.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class VeganPizza : Pizza
	{

		public VeganPizza()
		{
			Name = "Vegan Pizza";
			var spinachTopping = PizzaHelper.GetTopping("Spinach");
			PizzaToppings.Add(new PizzaTopping {ToppingId = spinachTopping.Id, Topping = spinachTopping });

			var olivesTopping = PizzaHelper.GetTopping("Olives");
			PizzaToppings.Add(new PizzaTopping {ToppingId = olivesTopping.Id, Topping = olivesTopping });
			CrustId = 1;
			SizeId = 1;

		}
	}
}
