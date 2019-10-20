using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcDemo.Data;
using MvcDemo.Models;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MvcDemoDbContext _context;

        public HomeController(MvcDemoDbContext context)
        {
            _context = context;

            // Force la création de la base. Utilisé ici uniquement 
            // parce que l'on travaille avec une In-Memory database
            // (avec un SGBD traditionnel, on utilise les migrations)
            _context.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            // get orders summaries with product names and customer name
            var orders = 
                _context.OrderLines
                        .Include(ol => ol.OrderHeader)
                            .ThenInclude(oh => oh.Customer)
                        .GroupBy(ol => ol.OrderHeader.Id)
                        .Select(g => new
                        {
                            OrderId = g.Key,
                            Customer = g.First().OrderHeader.Customer.Name,
                            Total = g.First().OrderHeader.TotalAmount,
                            Products = g.Select(l => l.Product.Name)
                        }).ToList();

            ViewData["orders"] = orders;

            return View();
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
