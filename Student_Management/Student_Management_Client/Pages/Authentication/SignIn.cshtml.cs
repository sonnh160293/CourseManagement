using DTO.Common;
using DTO.PostDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Student_Management_Client.Pages.Authentication
{
    public class SignInModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        public SignInModel()
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public LoginModel Login { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string LoginAPIUrl = "http://localhost:5172/api/Authen/Login";
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync(LoginAPIUrl, Login);
            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<string>(result, option);
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.Now.AddHours(1)
                };
                Response.Cookies.Append("jwt", token, cookieOptions);

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
                var claims = jsonToken.Claims;

                if (jsonToken != null)
                {
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var properties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                }

                var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                if (roleClaim == "Education Management")
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    return RedirectToPage("/Admin/Index");
                }


            }

            ResponseMessage errorMessage = JsonSerializer.Deserialize<ResponseMessage>(result, option);
            string error = errorMessage.Message;
            TempData["ErrorMessage"] = error;
            return Page();

        }
    }
}
