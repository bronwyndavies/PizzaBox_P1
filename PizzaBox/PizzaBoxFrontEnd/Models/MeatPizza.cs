using PizzaBox.Domain.Entities;

namespace PizzaBox.Domain.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class MeatPizza : Pizza
	{

		public MeatPizza()
		{
			Name = "Meat Pizza";
			var baconTopping = PizzaHelper.GetTopping("Bacon");
			PizzaToppings.Add(new PizzaTopping {ToppingId = baconTopping.Id, Topping = baconTopping });

			var sausageTopping = PizzaHelper.GetTopping("Sausage");
			PizzaToppings.Add(new PizzaTopping {ToppingId = sausageTopping.Id, Topping = sausageTopping });
			CrustId = 1;
			SizeId = 1;
		}
	}
}
