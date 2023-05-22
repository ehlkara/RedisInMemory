using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;

        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            //_distributedCache.SetString("name", "ehlullah", cacheEntryOptions);
            //await _distributedCache.SetStringAsync("surname", "karakurt");

            Product product = new Product { Id = 1, Name = "Pen", Price = 100 };
            Product product2 = new Product { Id = 2, Name = "Pen2", Price = 100 };
            string jsonproduct = JsonConvert.SerializeObject(product);
            string jsonproduct2 = JsonConvert.SerializeObject(product2);

            Byte[] byteproduct = Encoding.UTF8.GetBytes(jsonproduct);

            _distributedCache.Set("product:1", byteproduct);

            //await _distributedCache.SetStringAsync("product:1", jsonproduct, cacheEntryOptions);
            //await _distributedCache.SetStringAsync("product:2", jsonproduct2, cacheEntryOptions);

            return View();
        }

        public IActionResult Show()
        {
            //string name = _distributedCache.GetString("name");
            //ViewBag.name = name;

            //string jsonProduct = _distributedCache.GetString("product:1");

            //Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);

            Byte[] byteProduct = _distributedCache.Get("product:1");

            string jsonProduct = Encoding.UTF8.GetString(byteProduct);

            Product p = JsonConvert.DeserializeObject<Product>(jsonProduct);

            ViewBag.product = p;
            
            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache.Remove("name");

            return View();
        }
    }
}