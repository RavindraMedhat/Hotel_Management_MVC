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
    public class CouponController : Controller
    {
        private string API_Coupon;
        private string API_HOTEL;

        public CouponController()
        {
            API_Coupon = @"http://localhost:17312/api/Coupons";
            API_HOTEL = @"http://localhost:17312/api/hoteltbs";
        }
        // GET: CouponController
        public async Task<ActionResult> Index(int hid)
        {
            List<Coupon> coupons;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_Coupon + "/ByHotelID/" + hid))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    coupons = JsonConvert.DeserializeObject<List<Coupon>>(apiresponse);
                }
            }
            ViewBag.Hotel_ID = hid;
            return View(coupons);
        }

        // GET: CouponController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CouponController/Create
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
            ViewBag.Hotel_ID = new SelectList(hotels, "Hotel_ID", "Hotel_Name", hid);
            return View();
        }

        // POST: CouponController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CouponViewModelForCreate collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using (var response = await httpClient.PostAsync(API_Coupon, contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();

                    }
                }
                return RedirectToAction("Index", "Coupon", new { hid = collection.Hotel_ID });

            }
            catch
            {
                return View();
            }
        }

        // GET: CouponController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CouponController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CouponController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CouponController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
