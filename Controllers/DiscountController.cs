using Hotel_Management_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class DiscountController : Controller
    {
        private string API_Discount;

        public DiscountController()
        {
            API_Discount = @"http://localhost:17312/api/Discounts";
        }
        // GET: DicountController
        public async Task<ActionResult> Index()
        {
            List<Discount> discounts;
            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync(API_Discount))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    discounts = JsonConvert.DeserializeObject<List<Discount>>(apiresponse);
                }
            }
            return View(discounts);
        }

        // GET: DicountController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Discount discounts;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_Discount+"/"+id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    discounts = JsonConvert.DeserializeObject<Discount>(apiresponse);
                }
            }
            return View(discounts);
        }

        // GET: DicountController/Create
        public  ActionResult Create()
        {
            return View();
        }

        // POST: DicountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Discount collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using (var response = await httpClient.PostAsync(API_Discount,contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                       
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DicountController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Discount discounts;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_Discount + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    discounts = JsonConvert.DeserializeObject<Discount>(apiresponse);
                }
            }
            return View(discounts);
        }

        // POST: DicountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Discount collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using (var response = await httpClient.PutAsync(API_Discount+"/"+id, contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DicountController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Discount discounts;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_Discount + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    discounts = JsonConvert.DeserializeObject<Discount>(apiresponse);
                }
            }
            return View(discounts);
        }

        // POST: DicountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                   
                    using (var response = await httpClient.DeleteAsync(API_Discount + "/" + id))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
