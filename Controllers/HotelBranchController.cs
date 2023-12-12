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
    public class HotelBranchController : Controller
    {
        private string API_HOTEL_BRANCH;
        private string API_HOTEL;

        public HotelBranchController()
        {
         API_HOTEL_BRANCH = @"http://localhost:17312/api/HotelBranchTBs";
         API_HOTEL = @"http://localhost:17312/api/hoteltbs";
        }
        // GET: HotelBranchController
       

        public async Task<ActionResult> Index(int id,string HotelName)

        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

        
        List<HotelBranchViewModelForIndex> HotelBranchs = new List<HotelBranchViewModelForIndex>();

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH+"/ByHotelId/"+id))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchs = JsonConvert.DeserializeObject<List<HotelBranchViewModelForIndex>>(apiresponse);
                }
            }
            ViewBag.HotelName = HotelName;
            if(HotelName == null)
            {
                ViewBag.HotelName = HotelBranchs[0].Hotel_Name;

            }
            ViewBag.Hotel_ID = id;

            return View(HotelBranchs);
        }

        // GET: HotelBranchController/Details/5
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
            else if (Role != "HotelOwner" && Role != "SuperAdmin" && id == RedirctID)
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            HotelBranchViewModelForDetails HotelBranchsdetail;

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH+"/"+id))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchsdetail = JsonConvert.DeserializeObject<HotelBranchViewModelForDetails>(apiresponse);
                }
            }
            return View(HotelBranchsdetail);
        }

        // GET: HotelBranchController/Create
        public async Task<ActionResult> Create(int id)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            List<HotelNameAndIdViewModel> Hotels = new List<HotelNameAndIdViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL+"/ForDropDown"))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    Hotels = JsonConvert.DeserializeObject <List<HotelNameAndIdViewModel>>(apiresponse);
                }
            }
            Hotels = (from h in Hotels
                      where h.Hotel_ID == id
                      select h).ToList();
            ViewBag.Hotel_ID = new SelectList(Hotels, "Hotel_ID", "Hotel_Name", id);


            return View();
        }

        // POST: HotelBranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HotelBranchTBCreateModel collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
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
                        form.Add(new StringContent(collection.Hotel_ID.ToString()), "Hotel_ID");
                        form.Add(new StringContent(collection.Branch_Name), "Branch_Name");
                        form.Add(new StringContent(collection.Branch_Description), "Branch_Description");
                        form.Add(new StringContent(collection.Branch_map_coordinate), "Branch_map_coordinate");
                        form.Add(new StringContent(collection.Branch_Address), "Branch_Address");
                        form.Add(new StringContent(collection.Branch_Contact_No), "Branch_Contact_No");
                        form.Add(new StringContent(collection.Branch_Email_Adderss), "Branch_Email_Adderss");
                        form.Add(new StringContent(collection.Branch_Contect_Person), "Branch_Contect_Person");
                        //form.Add(new StringContent(collection.Standard_check_In_Time), "Standard_check_In_Time");
                        //form.Add(new StringContent(collection.Standard_check_out_Time), "Standard_check_out_Time");
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
                        using (var response = await httpClient.PostAsync(API_HOTEL_BRANCH, form))
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

                return RedirectToAction(nameof(Index),new {id=collection.Hotel_ID });
            }
            catch
            {
                return View();
            }
        }

        // GET: HotelBranchController/Edit/5
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
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            List<HotelNameAndIdViewModel> Hotels = new List<HotelNameAndIdViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(@"http://localhost:17312/api/HotelTBs/ForDropDown"))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    Hotels = JsonConvert.DeserializeObject<List<HotelNameAndIdViewModel>>(apiresponse);
                }
            }

            HotelBranchViewModelForDetails HotelBranchsedit;

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH + "/" + id))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchsedit = JsonConvert.DeserializeObject<HotelBranchViewModelForDetails>(apiresponse);
                }
            }
            ViewBag.Hotel_ID = new SelectList(Hotels, "Hotel_ID", "Hotel_Name", HotelBranchsedit.Hotel_ID);
            return View(HotelBranchsedit);
        }

        // POST: HotelBranchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, HotelBranchTB collection)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            try
            {
               

                using (var httpclient=new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);

                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");

                    using(var response=await httpclient.PutAsync(API_HOTEL_BRANCH + "/" + id, contentdata))
                    {
                        var apiresponse=await response.Content.ReadAsStringAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HotelBranchController/Delete/5
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
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            HotelBranchViewModelForDetails HotelBranchsdel;

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH + "/" + id))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchsdel = JsonConvert.DeserializeObject<HotelBranchViewModelForDetails>(apiresponse);
                }
            }
            return View(HotelBranchsdel);
        }

        // POST: HotelBranchController/Delete/5
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
            else if (Role != "HotelOwner" && Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }

            try
            {
                using(var httpclient=new HttpClient())
                {
                    using(var response= await httpclient.DeleteAsync(API_HOTEL_BRANCH+"/"+id))
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
public class HotelNameAndIdViewModel
{
    public int Hotel_ID { get; set; }
    public string Hotel_Name { get; set; }
}
