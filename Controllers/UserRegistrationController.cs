using Hotel_Management_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using MySqlX.XDevAPI;
using Hotel_Management_MVC.ViewModels;

namespace Hotel_Management_MVC.Controllers
{
    public class UserRegistrationController : Controller
    {
        private string API_UserRegistration;
        private string API_RegistrationforOwner;
        private string API_RegistrationForHotelManager;
        private string API_RegistartonForHotelReceptionist;
        private string API_Login;
        public UserRegistrationController()
        {
            //var un = Session["Username"];
            //if ()
            //{

            //}
            
            API_UserRegistration = @"http://localhost:17312/api/userregistrations";
            API_RegistrationforOwner = @"http://localhost:17312/api/userregistrations/ForHotelOwner";
            API_RegistrationForHotelManager = @"http://localhost:17312/api/userregistrations/ForHotelManager";
            API_RegistartonForHotelReceptionist = @"http://localhost:17312/api/userregistrations/ForHotelReceptionist";
            API_Login = @"http://localhost:17312/api/users/login";
        }
        // GET: UserRegistrationController
       
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserRegistrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistrationController/Create
        //for the owner registration
        [HttpGet("Createforhotelowner/{hid}")]
        public ActionResult Createforhotelowner(int hid)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "SuperAdmin")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            ViewBag.Hotel_ID = hid;
            return View();
        }

        [HttpPost("Createforhotelowner/{hid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Createforhotelowner(UserRegistrationCreateModel collection, int? Hotel_ID)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "SuperAdmin")
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
                        form.Add(new StringContent(collection.First_Name), "First_Name");
                        form.Add(new StringContent(collection.Last_Name), "Last_Name");
                        form.Add(new StringContent(collection.Email), "Email");
                        form.Add(new StringContent(collection.ConatactNo), "ConatactNo");
                        form.Add(new StringContent(collection.DOB.ToString()), "DOB");
                        form.Add(new StringContent(collection.Gender), "Gender");
                        form.Add(new StringContent(collection.State), "State");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

                        // Add image files
                        if (collection.Profile_Image != null)
                        {


                            var streamContent = new StreamContent(collection.Profile_Image.OpenReadStream());
                            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "Profile_Image",
                                FileName = collection.Profile_Image.FileName
                            };
                            form.Add(streamContent, "Profile_Image", collection.Profile_Image.FileName);
                        }


                        // Send the request
                        using (var response = await httpClient.PostAsync(API_RegistrationforOwner+"/"+Hotel_ID, form))
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

                return RedirectToAction("Index","HotelBranch",new { id= Hotel_ID });
            }
            catch
            {
                return View();
            }
        }



        [HttpGet("CreateforhotelManager/{bid}")]
        public ActionResult CreateforhotelManager(int bid)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            ViewBag.Branch_ID = bid;
            return View();
        }
        // POST: UserRegistrationController/Create
        [HttpPost("CreateforhotelManager/{bid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateforhotelManager(UserRegistrationCreateModel collection, int? Branch_ID)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner")
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
                        form.Add(new StringContent(collection.First_Name), "First_Name");
                        form.Add(new StringContent(collection.Last_Name), "Last_Name");
                        form.Add(new StringContent(collection.Email), "Email");
                        form.Add(new StringContent(collection.ConatactNo), "ConatactNo");
                        form.Add(new StringContent(collection.DOB.ToString()), "DOB");
                        form.Add(new StringContent(collection.Gender), "Gender");
                        form.Add(new StringContent(collection.State), "State");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

                        // Add image files
                        if (collection.Profile_Image != null)
                        {


                            var streamContent = new StreamContent(collection.Profile_Image.OpenReadStream());
                            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "Profile_Image",
                                FileName = collection.Profile_Image.FileName
                            };
                            form.Add(streamContent, "Profile_Image", collection.Profile_Image.FileName);
                        }


                        // Send the request
                        using (var response = await httpClient.PostAsync(API_RegistrationForHotelManager+"/"+Branch_ID, form))
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

                return RedirectToAction("index","room",new { id= Branch_ID });
            }
            catch
            {
                return View();
            }
        }



        [HttpGet("CreateforhotelReceptionist/{bid}")]
        public ActionResult CreateforhotelReceptionist(int bid)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner")
            {
                return RedirectToAction("Index", Redirect, new { id = RedirctID });
            }
            ViewBag.Branch_ID = bid;
            return View();
        }

        // POST: UserRegistrationController/Create
        [HttpPost("CreateforhotelReceptionist/{bid}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateforhotelReceptionist(UserRegistrationCreateModel collection, int? Branch_ID)
        {
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            var Redirect = HttpContext.Session.GetString("Redirect");
            var RedirctID = HttpContext.Session.GetInt32("RedirctID");
            if (Email == null || Role == null || Redirect == null || RedirctID == null)
            {
                return RedirectToAction("login", "UserRegistration");

            }
            else if (Role != "HotelOwner")
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
                        form.Add(new StringContent(collection.First_Name), "First_Name");
                        form.Add(new StringContent(collection.Last_Name), "Last_Name");
                        form.Add(new StringContent(collection.Email), "Email");
                        form.Add(new StringContent(collection.ConatactNo), "ConatactNo");
                        form.Add(new StringContent(collection.DOB.ToString()), "DOB");
                        form.Add(new StringContent(collection.Gender), "Gender");
                        form.Add(new StringContent(collection.State), "State");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

                        // Add image files
                        if (collection.Profile_Image != null)
                        {


                            var streamContent = new StreamContent(collection.Profile_Image.OpenReadStream());
                            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "Profile_Image",
                                FileName = collection.Profile_Image.FileName
                            };
                            form.Add(streamContent, "Profile_Image", collection.Profile_Image.FileName);
                        }


                        // Send the request
                        using (var response = await httpClient.PostAsync(API_RegistartonForHotelReceptionist+"/"+Branch_ID, form))
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

                return RedirectToAction("index", "room", new { id = Branch_ID });

            }
            catch
            {
                return View();
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create(UserRegistrationCreateModel collection)
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var form = new MultipartFormDataContent())
        //            {
        //                // Add your model properties as content fields
        //                form.Add(new StringContent(collection.First_Name), "First_Name");
        //                form.Add(new StringContent(collection.Last_Name), "Last_Name");
        //                form.Add(new StringContent(collection.Email), "Email");
        //                form.Add(new StringContent(collection.ConatactNo), "ConatactNo");
        //                form.Add(new StringContent(collection.DOB.ToString()), "DOB");
        //                form.Add(new StringContent(collection.Gender), "Gender");
        //                form.Add(new StringContent(collection.State), "State");
        //                form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
        //                form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
        //                form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

        //                // Add image files
        //                if (collection.Profile_Image != null)
        //                {


        //                    var streamContent = new StreamContent(collection.Profile_Image.OpenReadStream());
        //                    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //                    {
        //                        Name = "Profile_Image",
        //                        FileName = collection.Profile_Image.FileName
        //                    };
        //                    form.Add(streamContent, "Profile_Image", collection.Profile_Image.FileName);
        //                }


        //                // Send the request
        //                using (var response = await httpClient.PostAsync(API_UserRegistration, form))
        //                {
        //                    if (response.IsSuccessStatusCode)
        //                    {
        //                        // Request was successful, handle the response here
        //                        var responseBody = await response.Content.ReadAsStringAsync();
        //                        Console.WriteLine("Response: " + responseBody);
        //                    }
        //                    else
        //                    {
        //                        // Handle an error response
        //                        Console.WriteLine("Error: " + response.StatusCode);
        //                    }
        //                }
        //            }
        //        }

        //        return RedirectToAction("login","userregistration");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public async Task<ActionResult> Create(UserRegistrationCreateModelForCustomer collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Add your model properties as content fields
                        form.Add(new StringContent(collection.First_Name), "First_Name");
                        form.Add(new StringContent(collection.Last_Name), "Last_Name");
                        form.Add(new StringContent(collection.Email), "Email");
                        form.Add(new StringContent(collection.Password), "Password");
                        form.Add(new StringContent(collection.ConatactNo), "ConatactNo");
                        form.Add(new StringContent(collection.DOB.ToString()), "DOB");
                        form.Add(new StringContent(collection.Gender), "Gender");
                        form.Add(new StringContent(collection.State), "State");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.sortedfield.ToString()), "sortedfield");

                        // Add image files
                        if (collection.Profile_Image != null)
                        {


                            var streamContent = new StreamContent(collection.Profile_Image.OpenReadStream());
                            streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "Profile_Image",
                                FileName = collection.Profile_Image.FileName
                            };
                            form.Add(streamContent, "Profile_Image", collection.Profile_Image.FileName);
                        }


                        // Send the request
                        using (var response = await httpClient.PostAsync(API_UserRegistration, form))
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

                //return RedirectToAction("login", "userregistration");

                
                var Redirect = HttpContext.Session.GetString("Redirect");
                var RedirctID = HttpContext.Session.GetInt32("RedirctID");

                return RedirectToAction("Index", Redirect, new { id = RedirctID });
                
            }
            catch
            {
                return View();
            }
        }








        // GET: UserRegistrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistrationController/Edit/5
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

        // GET: UserRegistrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistrationController/Delete/5
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

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginRequest collection)
        {
            try
            {
                LoginResponse loginResponse;
                using(var httpClient=new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");
                    using (var response = await httpClient.PostAsync(API_Login, contentdata))
                    {
                        var apiresponse = await response.Content.ReadAsStringAsync();
                        loginResponse = JsonConvert.DeserializeObject<LoginResponse>(apiresponse);
                    }
                }
                if (loginResponse.Success)
                {
                    HttpContext.Session.SetString("Email", collection.EmailID);
                    HttpContext.Session.SetString("Role", loginResponse.Role);
                    HttpContext.Session.SetString("Redirect", loginResponse.Redirect);
                    HttpContext.Session.SetInt32("RedirctID", loginResponse.RedirctID);
                    
                    return RedirectToAction("Index", loginResponse.Redirect, new { id = loginResponse.RedirctID });
                }
                else
                {
                    return View();
                }
               

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Redirect");
            HttpContext.Session.Remove("RedirctID");
            return RedirectToAction("login", "UserRegistration");
        }
    }
}
