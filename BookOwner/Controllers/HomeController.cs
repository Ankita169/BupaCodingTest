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
                        var owners = JsonConvert.DeserializeObject<List<Owner>>(OwnerResponse);
                        if (owners != null)
                        {

                            var ownersadult = owners.Where(adult => adult.Age >= 18);
                            var ownerschild = owners.Where(child => child.Age < 18);
                            var Adultbook = ownersadult.SelectMany(a => a.Books).OrderBy(ba => ba.Name).ToList();
                            var Childbook = ownerschild.SelectMany(c => c.Books).OrderBy(bc => bc.Name).ToList();
                            var bookowner = new Owner
                            {
                                OwnerAdult = Adultbook,
                                OwnerChild = Childbook
                            };
                            //Deserializing the response recieved from web api and storing into the Employee list  
                            return View(bookowner);
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
            //returning the employee list to view  


            catch (HttpRequestException)
            {
                ModelState.AddModelError("", "There was a problem connection to the API");
                return View("Error");
            }
        }

        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Error()
        {
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

                            //Deserializing the response recieved from web api and storing into the Employee list  
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

                            //Deserializing the response recieved from web api and storing into the Employee list  
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
                    //returning the employee list to view  
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
