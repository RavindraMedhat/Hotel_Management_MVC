using Hotel_Management_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private string API_HOTEL;

        public DiscountController()
        {
            API_Discount = @"http://localhost:17312/api/Discounts";
            API_HOTEL = @"http://localhost:17312/api/hoteltbs";

        }
        // GET: DicountController
        public async Task<ActionResult> Index(int hid)
        {
            List<Discount> discounts;
            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync(API_Discount+ "/ByHotelID/" + hid))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    discounts = JsonConvert.DeserializeObject<List<Discount>>(apiresponse);
                }
            }
            ViewBag.Hotel_ID = hid;
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
        public async Task<ActionResult> Create(int hid)
        {
            List<HotelTB> hotels;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_HOTEL))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    hotels = JsonConvert.DeserializeObject<List<HotelTB>>(apiresponse);
                }
            }
            hotels = (from h in hotels
                      where h.Hotel_ID == hid
                      select h).ToList();
            ViewBag.Hotel_ID = new SelectList(hotels, "Hotel_ID", "Hotel_Name",  hid );
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
                return RedirectToAction("Index", "Discount", new { hid = collection.Hotel_ID });
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

            List<HotelTB> hotels;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_HOTEL))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    hotels = JsonConvert.DeserializeObject<List<HotelTB>>(apiresponse);
                }
            }
            hotels = (from h in hotels
                      where h.Hotel_ID == discounts.Hotel_ID
                      select h).ToList();
            ViewBag.Hotel_ID = new SelectList(hotels, "Hotel_ID", "Hotel_Name", discounts.Hotel_ID);
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
                return RedirectToAction("Index", "Discount", new { hid = collection.Hotel_ID });
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
        public async Task<ActionResult> Delete(int id, Discount collection)
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
                return RedirectToAction("Index", "Discount", new { hid = collection.Hotel_ID });
            }
            catch
            {
                return View();
            }
        }
    }
}
