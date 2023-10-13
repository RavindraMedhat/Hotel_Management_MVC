using Hotel_Management_MVC.Models;
using Hotel_Management_MVC.ViewModels;
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

namespace Hotel_Management_MVC.Controllers
{
    public class HotelController : Controller
    {
        private string API_HOTEL;
 
        public HotelController()
        {
            API_HOTEL = @"http://localhost:17312/api/hoteltbs";
        }
        // GET: HotelController
        public async Task<ActionResult> Index()
        {
            List<HotelViewModelForIndex> Hotels = new List<HotelViewModelForIndex>();

            using(var httpClient = new HttpClient())
            {
                using(var resonse=await httpClient.GetAsync(API_HOTEL))
                {
                    var apiresponse = await resonse.Content.ReadAsStringAsync();
                    Hotels = JsonConvert.DeserializeObject<List<HotelViewModelForIndex>>(apiresponse);
                }
            }
        
            return View(Hotels);
        }

        // GET: HotelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HotelController/Create
        public async Task<ActionResult> Create()
        {

            return View();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HotelTBCreatModel collection)
        {
            try
            {

                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Add your model properties as content fields
                        form.Add(new StringContent(collection.Hotel_Name), "Hotel_Name");
                        form.Add(new StringContent(collection.Hotel_Description), "Hotel_Description");
                        form.Add(new StringContent(collection.Hotel_map_coordinate), "Hotel_map_coordinate");
                        form.Add(new StringContent(collection.Address), "Address");
                        form.Add(new StringContent(collection.Contact_No), "Contact_No");
                        form.Add(new StringContent(collection.Email_Adderss), "Email_Adderss");
                        form.Add(new StringContent(collection.Contect_Person), "Contect_Person");
                        form.Add(new StringContent(collection.Standard_check_In_Time), "Standard_check_In_Time");
                        form.Add(new StringContent(collection.Standard_check_out_Time), "Standard_check_out_Time");
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
                        using (var response = await httpClient.PostAsync(API_HOTEL, form))
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

        // GET: HotelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HotelController/Edit/5
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

        // GET: HotelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HotelController/Delete/5
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
