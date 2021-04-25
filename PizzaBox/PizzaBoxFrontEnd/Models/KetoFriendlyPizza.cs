using PizzaBox.Domain.Entities;

namespace PizzaBox.Domain.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class KetoFriendlyPizza : Pizza
  {

    public KetoFriendlyPizza() //added
    {
      Name = "Keto Friendly Pizza";
            var extraCheeseTopping = PizzaHelper.GetTopping("Extra Cheese");
            PizzaToppings.Add(new PizzaTopping { ToppingId = extraCheeseTopping.Id, Topping = extraCheeseTopping });

            var baconTopping = PizzaHelper.GetTopping("Bacon");
            PizzaToppings.Add(new PizzaTopping { ToppingId = baconTopping.Id, Topping = baconTopping });
            CrustId = 1;
            SizeId = 1;
        }
  }
}
