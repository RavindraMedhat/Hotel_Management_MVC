using Hotel_Management_MVC.Models;
using Hotel_Management_MVC.ViewModels;
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
    public class BillingController : Controller
    {
        private string API_Billing;
        

        public BillingController()
        {
            API_Billing = @"http://localhost:17312/api/billings";
        }
        // GET: BillingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BillingController/Details/5
        public async Task<ActionResult> Details(string Gid)
        {
            BillRes data;
            using (var httpclient=new HttpClient())
            {
                var Jsondata = JsonConvert.SerializeObject(Gid);
                var contentdata = new StringContent(Jsondata, Encoding.UTF8, @"Application/json");
                using(var response=await httpclient.PostAsync(API_Billing+"/"+Gid, contentdata))
                {
                    var apiresponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<BillRes>(apiresponse);

                        return View(data);
                    }
                    else
                    {
                        var Redirect = HttpContext.Session.GetString("Redirect");
                        var RedirctID = HttpContext.Session.GetInt32("RedirctID");

                        return RedirectToAction("Index", Redirect, new { id = RedirctID });

                    }
                }
            }
            
        }
        //public async Task<IActionResult> GetHTMLPageAsPDF(string Gid) {
        //         BillRes data;
        //         using (var httpclient = new HttpClient())
        //         {
        //             var Jsondata = JsonConvert.SerializeObject(Gid);
        //             var contentdata = new StringContent(Jsondata, Encoding.UTF8, @"Application/json");
        //             using (var response = await httpclient.PostAsync(API_Billing + "/" + Gid, contentdata))
        //             {
        //                 var apiresponse = await response.Content.ReadAsStringAsync();

        //                 if (response.IsSuccessStatusCode)
        //                 {
        //                     data = JsonConvert.DeserializeObject<BillRes>(apiresponse);

        //                     var Renderer = new IronPdf.ChromePdfRenderer();

        //                     using var PDF = Renderer.RenderHtmlAsPdf(GenerateHtml(data));

        //                     var contentLength = PDF.BinaryData.Length;

        //                     Response.Headers["Content-Length"] = contentLength.ToString();


        //                     return File(PDF.BinaryData, "application/pdf;");
        //                 }
        //                 else
        //                 {
        //                     var Redirect = HttpContext.Session.GetString("Redirect");
        //                     var RedirctID = HttpContext.Session.GetInt32("RedirctID");

        //                     return RedirectToAction("Index", Redirect, new { id = RedirctID });

        //                 }
        //             }
        //         }

        // }
        public async Task<IActionResult> GetHTMLPageAsPDF(string Gid)
        {
            BillRes data;

            using (var httpClient = new HttpClient())
            {
                var jsonData = JsonConvert.SerializeObject(Gid);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(API_Billing + "/" + Gid, contentData))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        data = JsonConvert.DeserializeObject<BillRes>(apiResponse);

                        var renderer = new IronPdf.ChromePdfRenderer();

                        using var pdf = renderer.RenderHtmlAsPdf(GenerateHtml(data));

                        // Set Content-Disposition header for download
                        Response.Headers["Content-Disposition"] = "attachment; filename=YourFileName.pdf";

                        // Set Content-Length header
                        var contentLength = pdf.BinaryData.Length;
                        Response.Headers["Content-Length"] = contentLength.ToString();

                        // Return the PDF as a file download
                        return File(pdf.BinaryData, "application/pdf", $@"{data.bill.Group_ID}.pdf");
                    }
                    else
                    {
                        var redirect = HttpContext.Session.GetString("Redirect");
                        var redirectID = HttpContext.Session.GetInt32("RedirectID");

                        return RedirectToAction("Index", redirect, new { id = redirectID });
                    }
                }
            }
        }

        public string GenerateHtml(BillRes model)
        {
            // Start building the HTML string manually
            var html = $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Generate Bill</title>
            <style>
                .bill-container {{
                    max-width: 600px;
                    margin: 0 auto;
                }}

                table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-bottom: 20px;
                }}

                th, td {{
                    border: 1px solid #ddd;
                    padding: 8px;
                    text-align: left;
                }}

                th {{
                    background-color: #f2f2f2;
                }}

                h2, h3 {{
                    color: #333;
                }}

                .table-header {{
                    background-color: #4CAF50;
                    color: black;
                }}

                .footer {{
                    margin-top: 20px;
                    font-size: 12px;
                    color: #666;
                }}
            </style>
        </head>
        <body>

            <div class='bill-container'>
                <!-- Display Billing Information -->
                <table>
                    <tr>
                        <th>Bill ID</th>
                        <td>{model.bill.Bill_ID}</td>
                        <th>Group ID</th>
                        <td>{model.bill.Group_ID}</td>
                    </tr>
                    <tr>
                        <th>Bill Date</th>
                        <td>{model.bill.Bill_Date.ToString("yyyy-MM-dd")}</td>
                    </tr>
                </table>

                <!-- Display Additional Payment Information -->
                <h3>Payment Information</h3>
                <table>
                    <thead>
                        <tr class='table-header'>
                            <th>Detail</th>
                            <th>Amount</th>
                        </tr>
                    </thead>
                    <tbody>";

            // Add details to the HTML string
            foreach (var detail in model.details)
            {
                html += $@"
            <tr>
                <td>{detail.detail}</td>
                <td>{detail.amount.ToString("C")}</td>
            </tr>";
            }

            // Continue building the HTML string
            html += $@"
<tr><td>-----------------------------------------------------------------------</td><td>---------------------------------</td></tr>

                <tr>
                    <th>Total Amount</th>
                    <td>{model.bill.Total_Amount.ToString("C")}</td>
                </tr>

                <tr>
                    <th> Discount Amount </th>
                    <td>-{model.bill.Discount_Amount.ToString("C")}</td>
                </tr>
                <tr>
                    <th> Paid Amount </th>
                    <td> -{model.bill.Payed_Amount.ToString("C")} </td>
                </tr>
                <tr>
                    <th> Final Amount </th>
                    <td> {model.bill.Final_Amount.ToString("C")} </td>
                </tr>
                     </tbody>
        </table>
        
        <div class='footer'>
            This bill is a computer-generated document. No signature is required.
        </div>
    </div>
</body>
</html>";

            return html;
        }


        // GET: BillingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BillingController/Create
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

        // GET: BillingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BillingController/Edit/5
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

        // GET: BillingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BillingController/Delete/5
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
