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
    public class RoomCategoryController : Controller
    {
        // GET: RoomCategoryController

        private string API_URL_ROOMCAT;

        public RoomCategoryController()
        {
            API_URL_ROOMCAT = @"http://localhost:17312/api/RoomCategoryTBs";
        }
        public async Task<ActionResult> Index()
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            List<RoomCategoryTBForDetail> roomCategories;
            using(var httpclient=new HttpClient())
            {
                using(var response=await httpclient.GetAsync(API_URL_ROOMCAT))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategories = JsonConvert.DeserializeObject<List<RoomCategoryTBForDetail>>(apiresponse);
                }
            }
            return View(roomCategories);
        }

        // GET: RoomCategoryController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            RoomCategoryTBForDetail roomCategories;
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_URL_ROOMCAT + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategories = JsonConvert.DeserializeObject<RoomCategoryTBForDetail>(apiresponse);
                }
            }
            return View(roomCategories);
        }
            //return View();
        

        // GET: RoomCategoryController/Create
        public async Task<ActionResult> Create()
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            List<BranchNameAndIdViewModel> branches = new List<BranchNameAndIdViewModel>();
            using(var httpclient=new HttpClient())
            {
                using(var response=await httpclient.GetAsync(@"http://localhost:17312/api/HotelBranchTBs/ForDropDown"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    branches = JsonConvert.DeserializeObject<List<BranchNameAndIdViewModel>>(apiresponse);
                }
            }
            ViewBag.Branch_ID = new SelectList(branches, "Branch_ID", "Branch_Name", null);
            return View();
        }

        // POST: RoomCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoomCategoryTB collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
              using(var httpclient=new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/Json");

                    using(var response=await httpclient.PostAsync(API_URL_ROOMCAT, contentdata))
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

        // GET: RoomCategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            List<BranchNameAndIdViewModel> branches = new List<BranchNameAndIdViewModel>();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(@"http://localhost:17312/api/HotelBranchTBs/ForDropDown"))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    branches = JsonConvert.DeserializeObject<List<BranchNameAndIdViewModel>>(apiresponse);
                }
            }
            ViewBag.Branch_ID = new SelectList(branches, "Branch_ID", "Branch_Name", null);
            RoomCategoryTB roomCategories;
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_URL_ROOMCAT + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategories = JsonConvert.DeserializeObject<RoomCategoryTB>(apiresponse);
                }
            }
            return View(roomCategories);
            //return View();
        }

        // POST: RoomCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RoomCategoryTB collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
                using (var httpclient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/Json");

                    using (var response = await httpclient.PutAsync(API_URL_ROOMCAT+"/"+id, contentdata))
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

        // GET: RoomCategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            RoomCategoryTBForDetail roomCategories;
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_URL_ROOMCAT+"/"+id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategories = JsonConvert.DeserializeObject<RoomCategoryTBForDetail>(apiresponse);
                }
            }
            return View(roomCategories);
        }

        // POST: RoomCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionis" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
                using(var httpclient=new HttpClient())
                {
                    using(var response=await httpclient.DeleteAsync(API_URL_ROOMCAT + "/" + id))
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
    public class BranchNameAndIdViewModel
    {
        public int Branch_ID { get; set; }
        public string Branch_Name { get; set; }
    }
}
