using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;
using webapsmvc.Models;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        MobileContext db;
        public HomeController(MobileContext context)
        {
            db = context;
        }


        public IActionResult Index()
        {
            return View(db.Phones.ToList());
        }
        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<string> BuyAsync(Order order)
        {
            db.Orders.Add(order);
            using (FileStream fs = new FileStream("order.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<Order>(fs, order);
            }
            // зберігаємо зміни в базі даних 
            db.SaveChanges();
            return "Дякуємо, " + order.User + ", за покупку!";
        }
    }
}