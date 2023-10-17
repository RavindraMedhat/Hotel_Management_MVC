using Hotel_Management_MVC.Models;
using Hotel_Management_MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class ImageMasterController : Controller
    {
        private string APIURL_IMAGE;

        public ImageMasterController()
        {
            APIURL_IMAGE = @"http://localhost:17312/api/imagemastertbs";
        }
        // GET: ImageMasterController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ImageMasterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ImageMasterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ImageMasterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ImageViewModel collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        // Add your model properties as content fields
                        form.Add(new StringContent(collection.SortedFiled.ToString()), "SortedFiled");
                        form.Add(new StringContent(collection.Active_Flag.ToString()), "Active_Flag");
                        form.Add(new StringContent(collection.Delete_Flag.ToString()), "Delete_Flag");
                        form.Add(new StringContent(collection.ReferenceTB_Name), "ReferenceTB_Name");
                        form.Add(new StringContent(collection.Reference_ID.ToString()), "Reference_ID");

                        // Add image files
                        if (collection.Image_URl != null)
                        {
                          
                                var streamContent = new StreamContent(collection.Image_URl.OpenReadStream());
                                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "Image_URl",
                                    FileName = collection.Image_URl.FileName
                                };
                                form.Add(streamContent, "Image_URl", collection.Image_URl.FileName);
                        }
                        
                        // Send the request
                        using (var response = await httpClient.PostAsync(APIURL_IMAGE, form))
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
                return RedirectToAction(actionName:"Details" , controllerName:"Hotel", new { id=collection.Reference_ID});
            }
            catch
            {
                return View();
            }
        }

        // GET: ImageMasterController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ImageMasterController/Edit/5
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

        // GET: ImageMasterController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ImageMasterController/Delete/5
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
