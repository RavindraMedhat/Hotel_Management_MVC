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
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class RoomController : Controller
    {
        // GET: RoomController
        private string API_ROOM;
        private string API_URL_ROOMCAT;
        private string API_HOTEL_BRANCH;
        private string API_Details_ROOM_ID;

        public RoomController()
        {
            API_ROOM = @"http://localhost:17312/api/roomtbs";
            API_URL_ROOMCAT = @"http://localhost:17312/api/RoomCategoryTBs";
            API_HOTEL_BRANCH = @"http://localhost:17312/api/HotelBranchTBs";
            API_Details_ROOM_ID = @"http://localhost:17312/api/bookings/ByRoomIDAndDate";
        }
        public async Task<ActionResult> Index(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            else if ((Role == "HotelManager" || Role == "HotelReceptionist") && RedirctID != id)
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            
            List<RoomCategoryTBforcheckbox> roomCategoryTBforcheckbox = new List<RoomCategoryTBforcheckbox>();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_URL_ROOMCAT+ "/ByBranchID/"+id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategoryTBforcheckbox = JsonConvert.DeserializeObject<List<RoomCategoryTBforcheckbox>>(apiresponse);
                }
            }
            ViewBag.roomcategory = roomCategoryTBforcheckbox;

            List<RoomTBForIndex> room = new List<RoomTBForIndex>();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_ROOM + "/ByBranchID/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    room = JsonConvert.DeserializeObject<List<RoomTBForIndex>>(apiresponse);
                }
            }
            ViewBag.Branch_ID = id;
            HotelBranchViewModelForDetails HotelBranchsdetail;

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH + "/" + id))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchsdetail = JsonConvert.DeserializeObject<HotelBranchViewModelForDetails>(apiresponse);
                }
            }
            ViewBag.Branch_Name = HotelBranchsdetail.Branch_Name;

            return View(room);
        }
        //public async Task<ActionResult> Index_Branch()
        //{
        //    var Email = HttpContext.Session.GetString("Email");
        //    var Role = HttpContext.Session.GetString("Role");
        //    var Redirect = HttpContext.Session.GetString("Redirect");
        //    var RedirctID = HttpContext.Session.GetInt32("RedirctID");
        //    if (Email == null || Role == null || Redirect == null || RedirctID == null)
        //    {
        //        return RedirectToAction("login", "UserRegistration");

        //    }
        //    else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
        //    {
        //        return RedirectToAction("Index", Redirect, new { id = RedirctID });
        //    }
            


        //    using (var httpclient = new HttpClient())
        //    {
        //        using (var response = await httpclient.GetAsync(API_ROOM))
        //        {
        //            var apiresponse = await response.Content.ReadAsStringAsync();
        //        }
        //    }
        //    return View();
        //}
        public async Task<ActionResult> Index_RoomCategory()
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_ROOM))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }

        // GET: RoomController/Details/5
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
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            RoomViewModelForDetails roomViewModelForDetails;
            using(var httpClient=new HttpClient())
            {
                using(var response=await httpClient.GetAsync(API_ROOM + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomViewModelForDetails = JsonConvert.DeserializeObject<RoomViewModelForDetails>(apiresponse);
                }
            }
            //ViewBag.Room_ID = id;
            List<ViewModelForAvailability> ViewModelForAvailability;
            using(var httpClient=new HttpClient())
            {
                reqDataForAvailability reqData = new reqDataForAvailability() { date = DateTime.Now, Room_id = id };
                
                var jsondata = JsonConvert.SerializeObject( reqData);
                var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                using(var response=await httpClient.PostAsync(API_Details_ROOM_ID , contentdata))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    ViewModelForAvailability = JsonConvert.DeserializeObject<List<ViewModelForAvailability>>(apiresponse);
                }
            }
            ViewBag.Availability = ViewModelForAvailability;
            //roomViewModelForDetails.Room_ID = id;
            return View(roomViewModelForDetails);
        }

        // GET: RoomController/Create
        public async Task<ActionResult> Create(int id,string? redirectTo)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            List<RoomCategoryTBforcheckbox> roomCategoryTBforcheckbox = new List<RoomCategoryTBforcheckbox>();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_URL_ROOMCAT + "/ByBranchID/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    roomCategoryTBforcheckbox = JsonConvert.DeserializeObject<List<RoomCategoryTBforcheckbox>>(apiresponse);
                }
            }


            HotelBranchTB hb = new HotelBranchTB();
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_HOTEL_BRANCH + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    hb = JsonConvert.DeserializeObject<HotelBranchTB>(apiresponse);
                }
            }
          

            ViewBag.Category_ID = new SelectList(roomCategoryTBforcheckbox, "Category_ID", "Category_Name", id);

            ViewBag.Branch_ID = id;
            ViewBag.Hotel_ID = hb.Hotel_ID;
            ViewBag.redirectTo = redirectTo;

            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoomCreateModel collection, string? redirectTo)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Add your model properties as content fields
                        form.Add(new StringContent(collection.Category_ID.ToString()), "Category_ID");
                        form.Add(new StringContent(collection.Branch_ID.ToString()), "Branch_ID");
                        form.Add(new StringContent(collection.Hotel_ID.ToString()), "Hotel_ID");
                        form.Add(new StringContent(collection.Room_No), "Room_No");
                        form.Add(new StringContent(collection.Room_Description), "Room_Description");
                        form.Add(new StringContent(collection.Room_Price.ToString()), "Room_Price");
                        form.Add(new StringContent(collection.Iminity_Pool.ToString()), "Iminity_Pool");
                        form.Add(new StringContent(collection.Iminity_Bath.ToString()), "Iminity_Bath");
                        form.Add(new StringContent(collection.Iminity_NoOfBed.ToString()), "Iminity_NoOfBed");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

                        // Add image files
                        if (collection.Photos != null && collection.Photos.Count > 0)
                        {
                            foreach (var photo in collection.Photos)
                            {
                                var streamContent = new StreamContent(photo.OpenReadStream());
                                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "Photos",
                                    FileName = photo.FileName
                                };
                                form.Add(streamContent, "Photos", photo.FileName);
                            }
                        }

                        // Send the request
                        using (var response = await httpClient.PostAsync(API_ROOM, form))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // Request was successful, handle the response here
                                var responseBody = await response.Content.ReadAsStringAsync();
                                Console.WriteLine("Response: " + responseBody);
                            }
                            else
                            {
                                // Handle an error response
                                Console.WriteLine("Error: " + response.StatusCode);
                            }
                        }
                    }
                }
                //if (redirectTo != null)
                //{
                //    return RedirectToAction("crite", "ControllerName", new { id = collection.Branch_ID });
                //}
                //else
                //{
                //    return RedirectToAction(nameof(Index), new { id = collection.Branch_ID });
                //}
                return RedirectToAction("Index", "Room", new { id = collection.Branch_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            return View();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            return View();
        }

        // POST: RoomController/Delete/5
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
            else if (Role != "HotelManager" && Role != "HotelReceptionist" && Role != "HotelOwner" )
            {
                
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            else if ((Role == "HotelManager" || Role == "HotelReceptionist") && RedirctID != id)
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            else if (Role == "HotelOwner")
            {
                List<HotelBranchViewModelForIndex> HotelBranchs;
                using (var httpClient = new HttpClient())
                {
                    using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH + "/ByHotelId/" + id))
                    {
                        var apiresponse = await resonse.Content.ReadAsStringAsync();
                        HotelBranchs = JsonConvert.DeserializeObject<List<HotelBranchViewModelForIndex>>(apiresponse);
                    }
                }

                HotelBranchs = (from h in HotelBranchs
                                where h.Branch_ID == id
                                select h).ToList();
                if (HotelBranchs.Count == 0)
                {
                    return RedirectToAction("Index", Redirect, new { id = RedirctID });
                }
            }

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
public class RoomCategoryTBforcheckbox
{
    public int Category_ID { get; set; }
    public string Category_Name { get; set; }
}