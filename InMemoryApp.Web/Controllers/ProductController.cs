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

            //if(!_memoryCache.TryGetValue("time", out string timecache))
            //{
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

                options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);

                //options.SlidingExpiration = TimeSpan.FromSeconds(10);

                options.Priority = CacheItemPriority.High;

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _memoryCache.Set("callback", $"{key}->{value} => reason:{reason}");
                });

                _memoryCache.Set<string>("time", DateTime.Now.ToString(), options);
            //}

            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.GetOrCreate<string>("time", entry =>
            //{
            //    return DateTime.Now.ToString();
            //});

            _memoryCache.TryGetValue("time", out string timecache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.time = timecache;
            ViewBag.callback = callback;

            return View();
        }
    }
}