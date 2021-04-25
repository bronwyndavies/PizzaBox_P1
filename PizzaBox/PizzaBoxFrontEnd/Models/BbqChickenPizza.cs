using PizzaBox.Domain.Entities;

namespace PizzaBox.Domain.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class BbqChickenPizza : Pizza
	{


		public BbqChickenPizza()
		{
			Name = "Bbq Chicken Pizza";
			var chickenTopping = PizzaHelper.GetTopping("Chicken");
			PizzaToppings.Add(new PizzaTopping {ToppingId=chickenTopping.Id, Topping = chickenTopping });

			var bbqSauceTopping = PizzaHelper.GetTopping("BBQ Sauce");
			PizzaToppings.Add(new PizzaTopping { ToppingId = bbqSauceTopping.Id, Topping = bbqSauceTopping });
			CrustId = 1;
			SizeId = 1;
		}
	}
}
