using Hotel_Management_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_MVC.Controllers
{
    public class UserRolesController : Controller
    {
        private string API_UserRole;
        public UserRolesController()
        {
            API_UserRole = @"http://localhost:17312/api/userroles";
        }
        // GET: UserRolesController
        public async Task<ActionResult> Index()
        {
            List<UserRole> userrole;

            using(var httpclient=new HttpClient())
            {
                using(var response=await httpclient.GetAsync(API_UserRole))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    userrole = JsonConvert.DeserializeObject<List<UserRole>>(apiresponse);
                }
            }
            return View(userrole);
        }

        // GET: UserRolesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            UserRole userRole;
            using(var httpclient=new HttpClient()) 
            { 
                using(var response=await httpclient.GetAsync(API_UserRole + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    userRole = JsonConvert.DeserializeObject<UserRole>(apiresponse);
                }
            }
            return View(userRole);
        }

        // GET: UserRolesController/Create
        public async Task<ActionResult> Create()
        {

            return View();
        }

        // POST: UserRolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserRole collection)
        {
            try
            {
                using(var httpClient=new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");

                    using (var response = await httpClient.PostAsync(API_UserRole, contentdata))
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

        // GET: UserRolesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            UserRole userRole;
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_UserRole + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    userRole = JsonConvert.DeserializeObject<UserRole>(apiresponse);
                }
            }
            return View(userRole);
        }

        // POST: UserRolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, UserRole collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var jsondata = JsonConvert.SerializeObject(collection);
                    var contentdata = new StringContent(jsondata, Encoding.UTF8, @"Application/json");

                    using (var response = await httpClient.PutAsync(API_UserRole+"/"+id, contentdata))
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

        // GET: UserRolesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            UserRole userRole;
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync(API_UserRole + "/" + id))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    userRole = JsonConvert.DeserializeObject<UserRole>(apiresponse);
                }
            }
            return View(userRole);
        }

        // POST: UserRolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                   
                    using (var response = await httpClient.DeleteAsync(API_UserRole + "/" + id))
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
