using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PizzaBox.Client.Singletons;
using PizzaBox.Domain.Entities;
using PizzaBoxFrontEnd.Models;

namespace PizzaBoxFrontEnd.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult ViewStoreOrders()
        {
            List<Store> stores = GetStores();
            ViewBag.Stores = stores.Select(s => new SelectListItem()
            {
                Value=s.Id.ToString(),
                Text=s.Name
            }).ToList();
            ViewStoreOrdersViewModel viewStoreOrdersViewModel = new ViewStoreOrdersViewModel();
            if (ViewBag.StoreId != null)
            {
                viewStoreOrdersViewModel.StoreId = ViewBag.StoreId.ToString();
            }
            return View(viewStoreOrdersViewModel);
        }
        [HttpPost] 
        public IActionResult ViewStoreOrders(ViewStoreOrdersViewModel viewStoreOrdersViewModel)
        {
            int storeId = int.Parse(viewStoreOrdersViewModel.StoreId);
            var orders = GetStoreOrders(storeId);
            ViewBag.StoreId = storeId;
            ViewBag.StoreOrders = orders;
            return ViewStoreOrders();
        } 
        private List<Store> GetStores()
        {
            List<Store> stores = new List<Store>();
            var client = new HttpClient();
            string url = "https://localhost:5002/Store";
            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                stores = JsonConvert.DeserializeObject<List<Store>>(responsebody);
            }
            return stores;
        
    }
        private List<Order> GetStoreOrders(int storeId)
        {
            List<Order> orders = new List<Order>(); //Call API to get the list
            using (var client = new HttpClient())
            {
                string url = $"https://localhost:5002/StoreOrder?id={storeId}";
                var response = client.GetAsync(url);

                if (response.Result.IsSuccessStatusCode)
                {
                    var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                    orders = JsonConvert.DeserializeObject<List<Order>>(responsebody);
                }
            }
            return orders;
        }

        [HttpGet]
        public IActionResult ViewCustomerOrders()
        {
            List<Customer> customers = GetCustomers();
            ViewBag.Customers = customers.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.CustomerName
            }).ToList();
            ViewCustomerOrdersViewModel viewCustomerOrdersViewModel = new ViewCustomerOrdersViewModel();
            if (ViewBag.CustomerId != null)
            {
                viewCustomerOrdersViewModel.CustomerId = ViewBag.CustomerId.ToString();
            }
            return View(viewCustomerOrdersViewModel);
        }
        [HttpGet]

        public IActionResult UpdatePizza(int id)
        {
            var pizza = GetPizza(id);
            return View(pizza);
        }

        private Pizza GetPizza(int id)
        {
            var client = new HttpClient();
            string url = $"https://localhost:5002/Pizza?id={id}";
            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Pizza>(responsebody);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]

        public IActionResult SelectPizza(int id)
        {          
            var pizzas = PizzaSingleton.Instance.Pizzas;
            var pizzasList = new List<SelectListItem>();
            for (int i = 0; i < pizzas.Count; i++)
            {
                pizzasList.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = pizzas[i].Name
                });
            }
            ViewBag.Pizzas = pizzasList;
            var viewModel = new AddPizzaViewModel();
            viewModel.OrderId = id;
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SelectPizza(AddPizzaViewModel addPizzaViewModel)
        {
            var pizzas = PizzaSingleton.Instance.Pizzas;
            var selectedPizza = pizzas[addPizzaViewModel.PizzaIndex];
            selectedPizza.OrderId = addPizzaViewModel.OrderId;
            selectedPizza = AddPizza(selectedPizza);
            return RedirectToAction("UpdatePizza", new { id = selectedPizza.Id });
        }

        private Pizza AddPizza(Pizza pizza)
        {
            var client = new HttpClient();
            string url = "https://localhost:5002/Pizza";
            string pizzaJson = JsonConvert.SerializeObject(pizza);
            HttpContent httpContent = new StringContent(pizzaJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, httpContent);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Pizza>(responsebody);
            }
            else
            {
                return null;
            }
        }

        private void UpdateOrder(Order order)
        {
            var client = new HttpClient();
            string url = "https://localhost:5002/Order";
            string orderJson = JsonConvert.SerializeObject(order);
            HttpContent httpContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var response = client.PutAsync(url, httpContent);
            if (response.Result.IsSuccessStatusCode)
            {
                ViewBag.Message = "Order was updated successfully";
            }
            else
            {
                ViewBag.Message = "ERROR: Please try again!";
            }
        }

        private Order CreateOrder(Order order)
        {
            var client = new HttpClient();
            string url = "https://localhost:5002/Order";
            string orderJson = JsonConvert.SerializeObject(order);
            HttpContent httpContent = new StringContent(orderJson, Encoding.UTF8, "application/json");
            var response = client.PostAsync(url, httpContent);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                return  JsonConvert.DeserializeObject<Order>(responsebody);
            }
            else
            {
                return null;
            }
        }
        [HttpPost]

        public IActionResult InitOrder(PlaceOrderViewModel placeOrderViewModel)
        {
            var order = new Order()
            {
                Id = placeOrderViewModel.OrderId,
                CustomerId = int.Parse(placeOrderViewModel.CustomerId),
                StoreId = int.Parse(placeOrderViewModel.StoreId)
            };
            order = CreateOrder(order);
            return RedirectToAction("SelectPizza", new { id = order.Id });
        }
        [HttpGet]
        public IActionResult InitOrder()
        {
            List<Customer> customers = GetCustomers();
            ViewBag.Customers = customers.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.CustomerName
            }).ToList();

            List<Store> stores = GetStores();
            ViewBag.Stores = stores.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            PlaceOrderViewModel orderViewModel = new PlaceOrderViewModel();

            return View(orderViewModel);
        }

        private List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            var client = new HttpClient();
            string url = "https://localhost:5002/Customer";
            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customer>>(responsebody);
            }
            return customers;
        }



        [HttpPost]
        public IActionResult ViewCustomerOrders(ViewCustomerOrdersViewModel viewCustomerOrdersViewModel)
        {
            int customerId = int.Parse(viewCustomerOrdersViewModel.CustomerId);
            var orders = GetCustomerOrders(customerId);
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerOrders = orders;
            return ViewCustomerOrders();
        }

        private List<Order> GetCustomerOrders(int customerId)
        {
            List<Order> orders = new List<Order>();
            var client = new HttpClient();
            string url = $"https://localhost:5002/CustomerOrder?id={customerId}";
            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                var responsebody = response.Result.Content.ReadAsStringAsync().Result;
                orders = JsonConvert.DeserializeObject<List<Order>>(responsebody);
            }
            return orders;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
