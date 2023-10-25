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
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class HotelBranchController : Controller
    {
        private string API_HOTEL_BRANCH;

        public HotelBranchController()
        {
                     API_HOTEL_BRANCH = @"http://localhost:17312/api/HotelBranchTBs";
        }
        // GET: HotelBranchController
        public async Task<ActionResult> Index()
        
        {
            List<HotelBranchViewModelForIndex> HotelBranchs = new List<HotelBranchViewModelForIndex>();

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(API_HOTEL_BRANCH))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    HotelBranchs = JsonConvert.DeserializeObject<List<HotelBranchViewModelForIndex>>(apiresponse);
                }
            }

            return View(HotelBranchs);
        }

        // GET: HotelBranchController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HotelBranchController/Create
        public async Task<ActionResult> Create()
        {
            List<HotelNameAndIdViewModel> Hotels = new List<HotelNameAndIdViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var resonse = await httpClient.GetAsync(@"http://localhost:17312/api/HotelTBs/ForDropDown"))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    Hotels = JsonConvert.DeserializeObject <List<HotelNameAndIdViewModel>>(apiresponse);
                }
            }

            ViewBag.Hotel_ID = new SelectList(Hotels, "Hotel_ID", "Hotel_Name", null);


            return View();
        }

        // POST: HotelBranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HotelBranchTBCreateModel collection)
        {
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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HotelBranchController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HotelBranchController/Edit/5
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

        // GET: HotelBranchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HotelBranchController/Delete/5
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
public class HotelNameAndIdViewModel
{
    public int Hotel_ID { get; set; }
    public string Hotel_Name { get; set; }
}
