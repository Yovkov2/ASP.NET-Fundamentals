﻿using HttpBasic.Models;
using Microsoft.AspNetCore.Mvc;

namespace HttpBasic.Controllers
{
    public class StoreListController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddItem(Item model)
        {
            return View();
        }
        [HttpGet]
        public IActionResult EditItem(int id)
        {
            return View();
        }
        [HttpPut]
        public IActionResult EditItem(Item model)
        { 
            return View();
        }
        [HttpPatch]
        public IActionResult UpdateItemQuantity(int id, int quantity)
        {
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteItemQuantity(int id)
        {
            return View();
        }
    }
}
