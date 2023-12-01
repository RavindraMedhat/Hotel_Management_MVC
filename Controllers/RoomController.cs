using Hotel_Management_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class RoomController : Controller
    {
        // GET: RoomController
        private string API_ROOM;
        private string API_URL_ROOMCAT;

        public RoomController()
        {
            API_ROOM = @"http://localhost:17312/api/roomtbs";
            API_URL_ROOMCAT = @"http://localhost:17312/api/RoomCategoryTBs";
        }
        public async Task<ActionResult> Index(int id)
        {
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

            List<RoomTB> room = new List<RoomTB>();

            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_ROOM + "/ByBranchID/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    room = JsonConvert.DeserializeObject<List<RoomTB>>(apiresponse);
                }
            }
            ViewBag.Branch_ID = id;
            return View(room);
        }
        public async Task<ActionResult> Index_Branch()
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_ROOM))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                }
            }
            return View();
        }
        public async Task<ActionResult> Index_RoomCategory()
        {
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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
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

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
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
public class RoomCategoryTBforcheckbox
{
    public int Category_ID { get; set; }
    public string Category_Name { get; set; }
}