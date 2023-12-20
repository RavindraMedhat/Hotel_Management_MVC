using Hotel_Management_MVC.Models;
using Hotel_Management_MVC.ViewModels;
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
        private string API_User;

        public CouponController()
        {
            API_Coupon = @"http://localhost:17312/api/Coupons";
            API_HOTEL = @"http://localhost:17312/api/hoteltbs";
            API_User = @"http://localhost:17312/api/UserRegistrations";
        }
        // GET: CouponController
        public async Task<ActionResult> Index(int hid)
        {
            List<couponViewModelForIndex> coupons;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_Coupon + "/ByHotelID/" + hid))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    coupons = JsonConvert.DeserializeObject<List<couponViewModelForIndex>>(apiresponse);
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

                        if (!response.IsSuccessStatusCode)
                        {
                            List<HotelTB> hotels;
                            using (var httpClient2 = new HttpClient())
                            {
                                using (var response2 = await httpClient.GetAsync(API_HOTEL))
                                {
                                    var apiresponse2 = await response2.Content.ReadAsStringAsync();
                                    hotels = JsonConvert.DeserializeObject<List<HotelTB>>(apiresponse2);
                                }
                            }
                            hotels = (from h in hotels
                                      where h.Hotel_ID == collection.Hotel_ID
                                      select h).ToList();
                            ViewBag.Hotel_ID = new SelectList(hotels, "Hotel_ID", "Hotel_Name", collection.Hotel_ID);
                            ViewBag.Errormessage = (JsonConvert.DeserializeObject<MyError>(apiresponse)).Errormessage;
                            return View();

                        }
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
        public async Task<ActionResult> SendEmail(int hid,string Cname, int NoOFCoupen)
        {
            List<UserAndEmail> data;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_User + "/getCustomerForEmail"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<UserAndEmail>>(apiresponse);
                }
            }

            ViewBag.Cname = Cname;
            ViewBag.NoOFCoupen = NoOFCoupen;
            ViewBag.hid = hid;

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(List<int> selectedCustomers, string Cname, int NoOFCoupen,int hid)
        {
            try
            {

                if (selectedCustomers.Count > NoOFCoupen)
                {
                    ViewBag.Errormessage = $"There are only {NoOFCoupen} Coupen available ";
                    List<UserAndEmail> data;
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync(API_User + "/getCustomerForEmail"))
                        {
                            var apiresponse = await response.Content.ReadAsStringAsync();
                            data = JsonConvert.DeserializeObject<List<UserAndEmail>>(apiresponse);
                        }
                    }

                    ViewBag.Cname = Cname;
                    ViewBag.NoOFCoupen = NoOFCoupen;

                    return View(data);
                }

                ReqSendCoupen reqSendCoupen = new ReqSendCoupen()
                {
                    Cname = Cname,
                    hid = hid,
                    userIds = selectedCustomers
                };
                using (var httpClient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(reqSendCoupen);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using (var response = await httpClient.PostAsync(API_Coupon + "/SendEmail", contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();

                    }
                }


                return RedirectToAction(nameof(Index),new {hid =hid});
            }
            catch
            {
                return View();
            }
        }
    }
}
