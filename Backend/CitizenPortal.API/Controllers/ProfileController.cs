using CitizenPortal.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CitizenPortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("pincode/{pincode}")]
        public async Task<IActionResult> GetLocationByPincode(string pincode)
        {
            var url = $"https://api.postalpincode.in/pincode/{pincode}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Unable to fetch pincode details.");
            }

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var root = document.RootElement[0];

            if (root.GetProperty("Status").GetString() != "Success")
            {
                return NotFound("Invalid Pincode.");
            }

            var postOffice = root
                .GetProperty("PostOffice")[0];

            var result = new LocationResponseDto
            {
                State = postOffice.GetProperty("State").GetString() ?? "",
                District = postOffice.GetProperty("District").GetString() ?? "",
                City = postOffice.GetProperty("Name").GetString() ?? ""
            };

            return Ok(result);
        }
    }
}
