using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;

        private readonly IDatabase db;

        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Ehlullah Karakurt");
            db.StringSet("ziyaretçi", 100);


            return View();
        }

        public IActionResult Show()
        {
            //var value = db.StringGet("name");

            //db.StringIncrement("ziyaretçi", 10);

            //var count = db.StringDecrementAsync("ziyaretçi", 1).Result;

            //var value = db.StringGetRange("name", 0, 3);

            var value = db.StringLength("name");

            db.StringDecrementAsync("ziyaretçi", 10).Wait();

            //if (value.HasValue)
            //{
            //    ViewBag.value = value.ToString();
            //}

            ViewBag.value = value.ToString();

            return View();
        }
    }
}