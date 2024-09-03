using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Curriculums
{
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
        public List<CurriculumGetDTO> CurriculumGetDTOs { get; set; }

        [BindProperty(SupportsGet = true)]
        public int MajorId { get; set; }



        public IActionResult OnGet()
        {
            HttpRequestMessage requestCurriculum = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Curriculum?majorId={MajorId}");
            var responseCurriculum = _httpClient.SendAsync(requestCurriculum).Result;
            if (responseCurriculum.IsSuccessStatusCode)
            {
                var dataSubject = responseCurriculum.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                CurriculumGetDTOs = JsonSerializer.Deserialize<List<CurriculumGetDTO>>(dataSubject, option);
            }


            HttpRequestMessage requestMajor = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Major");
            var responseMajor = _httpClient.SendAsync(requestMajor).Result;
            if (responseMajor.IsSuccessStatusCode)
            {
                var dataMajor = responseMajor.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var Majors = JsonSerializer.Deserialize<List<MajorGetDTO>>(dataMajor, option);
                Majors.Insert(0, new MajorGetDTO { MajorId = 0, MajorName = "All" });

                ViewData["MajorSelectList"] = new SelectList(Majors, "MajorId", "MajorName");
            }

            return Page();

        }
    }
}
