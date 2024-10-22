using System;
using Newtonsoft.Json;
using System.Net.Http;
using BookOwner.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Web.Http;

namespace BookOwner.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://digitalcodingtest.bupa.com.au/api/v1/bookowners";



        public async Task<ActionResult> Book()
        {
            List<book> BookInfo = new List<book>();
            try
            {
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("/api/v1/bookowners");

                    //Checking the response is successful or not which is sent using HttpClient  
                                        if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var OwnerResponse = await Res.Content.ReadAsStringAsync();
                        //var owners = JsonConvert.DeserializeObject<List<Owner>>(OwnerResponse);
                        dynamic data = JsonConvert.DeserializeObject<dynamic>(OwnerResponse);
                        if (data != null)
                        {
                            
                            var adults = new List<dynamic>();
                            var child = new List<dynamic>();
                            foreach (var items in data)
                            {
                                if (items.age > 18)
                                {
                                    adults.Add(items);

                                }
                                else
                                {
                                    child.Add(items);
                                }
                            }
                            ViewBag.Adults = adults;
                            ViewBag.child = child;
                            return View();
                        }
                        else
                        {
                            ViewBag.Error = "Content not loading from the api";
                            return View("Error","Api Value not Loaded");

                        }
                        
                    }

                    else
                    {
                        return View("Error");
                    }
                }
            }
            //returning the employee list to view  


            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "There was a problem connection to the API");
                return View("Error");
            }
        }

        public async Task<ActionResult> Error()
        {
            ViewBag.Message = "Error";
            return View();
        }
        public async Task<ActionResult> AllBooks()
        {
            try
            {

                List<book> BookInfo = new List<book>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("/api/v1/bookowners");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var bookOwner = await Res.Content.ReadAsStringAsync();
                        var owners = JsonConvert.DeserializeObject<List<Owner>>(bookOwner);
                        if (owners == null)
                        {
                            ModelState.AddModelError("", "There was a problem connection to the API");
                            return View("Error");
                        }
                        else
                        {

                            var Allbooks = owners
                                .Where(owner => owner.Books != null)
                                .SelectMany(listbooks => listbooks.Books)
                                .OrderBy(bookssort => bookssort.Name).ToList();
                            ViewBag.Message = "AllBooks";
                            return View(Allbooks);
                        }
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "There was a problem connection to the API");
                return View("Error");
            }
        }

        public async Task<ActionResult> HardCoverBooks()
        {
            try
            {
                List<book> BookInfo = new List<book>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("/api/v1/bookowners");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var bookOwner = await Res.Content.ReadAsStringAsync();
                        var owners = JsonConvert.DeserializeObject<List<Owner>>(bookOwner);
                        if (owners != null)
                        {
                            var HardCover = owners.Where(owner => owner != null)
                           .SelectMany(ownercover => ownercover.Books).Where(type => type.Type == "Hardcover").OrderBy(cover => cover.Name).ToList();
                            ViewBag.Message = "HardCoverBooksOnly";
                            return View(HardCover);
                        }
                        else
                        {
                            ModelState.AddModelError("", "There was a problem connection to the API");
                            return View("Error");
                        }

                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "There was a problem connection to the API");
                return View("Error");
            }
        }



    }
}
