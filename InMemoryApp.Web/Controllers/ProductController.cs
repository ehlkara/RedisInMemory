using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            // first way
            //if(String.IsNullOrEmpty(_memoryCache.Get<string>("time")))
            //{
            //    _memoryCache.Set<string>("time", DateTime.Now.ToString());
            //}

            // second way

            if(!_memoryCache.TryGetValue("time", out string timecache))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

                options.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

                options.SlidingExpiration = TimeSpan.FromSeconds(10);

                _memoryCache.Set<string>("time", DateTime.Now.ToString(), options);
            }

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.GetOrCreate<string>("time", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("time", out string timecache);
            ViewBag.time = timecache;

            return View();
        }
    }
}