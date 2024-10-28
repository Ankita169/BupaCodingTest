using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using BookOwner.Models;
using Newtonsoft.Json;

namespace BookOwner
{
    public class BookOwnerService
    {
        private readonly HttpClient _httpClient;

       
        public BookOwnerService()
        {
            _httpClient = new HttpClient();

        }
        public async Task<List<Owner>> GetBooksByCategory()
        {
            {
                try
                {
                    var response = await _httpClient.GetAsync("https://digitalcodingtest.bupa.com.au/api/v1/bookowners");
                    response.EnsureSuccessStatusCode();

                    if (!response.IsSuccessStatusCode)
                    {
                        // Handle non-success status codes
                        if (response.StatusCode == HttpStatusCode.BadRequest)
                        {
                            return new List<Owner>(); // Return an empty list for Bad Request
                        }

                        response.EnsureSuccessStatusCode(); // Throw exception for other status codes
                    }

                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(responseContent))
                    {
                        return new List<Owner>();
                    }

                    var owners = JsonConvert.DeserializeObject<List<Owner>>(responseContent);
                    return owners ?? new List<Owner>();

                }

                catch (Exception ex)
                {

                    return new List<Owner>();
                }

            }
        }
            
    }
}
