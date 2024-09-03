using DTO.Common;

using DTO.GetDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin
{

    [Authorize(Roles = ConstantRole.ROLE_EDU)]
    public class IndexModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var token = httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        [BindProperty]
        public List<SubjectGetDTO> Subjects { get; set; }
        public IActionResult OnGet()
        {
            //var token = Request.Cookies["jwt"];
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Subject");
            //httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage).Result;
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToPage("/Authentication/SignIn");
            }
            var data = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Subjects = JsonSerializer.Deserialize<List<SubjectGetDTO>>(data, options);
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("true");

            }
            if (User.IsInRole(ConstantRole.ROLE_EDU))
            {
                Console.WriteLine("true");
                List<Claim> claims = User.Claims.ToList();
                foreach (var claim in claims)
                {
                    Console.WriteLine(claim.Value.ToString());
                }
            }
            return Page();
        }
    }
}
