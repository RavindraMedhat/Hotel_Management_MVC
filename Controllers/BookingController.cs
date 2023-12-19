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
    public class BookingController : Controller
    {
        private string API_GET_Customer;
        private string API_Booking;

        public BookingController()
        {
            API_GET_Customer = @"http://localhost:17312/api/userregistrations/getCustomerForDropdown";
            API_Booking = @"http://localhost:17312/api/bookings";
        }
        // GET: BookingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BookingController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Booking booking;
            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync(API_Booking + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Booking>(apiresponse);
                }
            }
            return View(booking);
        }

        // GET: BookingController/Create
        public async Task<ActionResult> Create(int Rid,int Bid,string date)
        {
            List<CustomerForDropdown> CustomerForDropdown;
            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync(API_GET_Customer))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    CustomerForDropdown = JsonConvert.DeserializeObject<List<CustomerForDropdown>>(apiresponse);
                }
            }
            ViewBag.User_ID = new SelectList(CustomerForDropdown, "User_ID", "Name", null);

            Booking booking = new Booking() {Room_ID=Rid,Branch_ID=Bid, Booking_Date=DateTime.Now, Active_Flag=true,Delete_Flag=false,Sortedfield=99,Booking_Status="Done",Discount=0,Customer_status="aavigayo",Group_ID="",Payment_Mode="cash",Payment_Status="pending",Check_In_Date=  DateTime.Now,Check_Out_Date= DateTime.Now.AddDays(1)};

            if (date != null)
            {
                if(DateTime.Parse(date) >= DateTime.Now)
                {
                    booking.Check_In_Date = DateTime.Parse(date);
                    booking.Check_Out_Date = DateTime.Parse(date).AddDays(1);
                }
            }


            

            return View(booking);
        }

        // POST: BookingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Booking collection)
        {
            try
            {
                if(collection.Check_In_Date.Date > collection.Check_Out_Date.Date || collection.Check_Out_Date.Date <DateTime.Now.Date || collection.Check_In_Date.Date < DateTime.Now.Date)
                {
                    ViewBag.Errormessage = "Invalid Check in / check out date";

                    collection.Check_In_Date = DateTime.Now;
                    collection.Check_Out_Date = DateTime.Now.AddDays(1);

                    List<CustomerForDropdown> CustomerForDropdown;
                    using (var httpClient2 = new HttpClient())
                    {
                        using (var response2 = await httpClient2.GetAsync(API_GET_Customer))
                        {
                            var apiresponse2 = await response2.Content.ReadAsStringAsync();
                            CustomerForDropdown = JsonConvert.DeserializeObject<List<CustomerForDropdown>>(apiresponse2);
                        }
                    }
                    ViewBag.User_ID = new SelectList(CustomerForDropdown, "User_ID", "Name", null);

                    return View(collection);

                }

                using (var httpClient=new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using(var response=await httpClient.PostAsync(API_Booking, contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        if (!response.IsSuccessStatusCode)
                        {
                            ViewBag.Errormessage = (JsonConvert.DeserializeObject<MyError>(apiresponse)).Errormessage;
                            List<CustomerForDropdown> CustomerForDropdown;
                            using (var httpClient2 = new HttpClient())
                            {
                                using (var response2 = await httpClient2.GetAsync(API_GET_Customer))
                                {
                                    var apiresponse2 = await response2.Content.ReadAsStringAsync();
                                    CustomerForDropdown = JsonConvert.DeserializeObject<List<CustomerForDropdown>>(apiresponse2);
                                }
                            }
                            ViewBag.User_ID = new SelectList(CustomerForDropdown, "User_ID", "Name", null);
                            return View(collection);

                        }
                    }
                }
                return RedirectToAction("details","Room",new {id=collection.Room_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingController/Edit/5
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

        // GET: BookingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingController/Delete/5
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
