using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Subjects
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


        [BindProperty(SupportsGet = true)]
        public int SelectedTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedMajorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedName { get; set; }

        public List<MajorGetDTO> Majors { get; set; }


        public List<int> Terms { get; set; }

        [BindProperty]
        public List<SubjectGetDTO> Subjects { get; set; }

        public IActionResult OnGet()
        {
            HttpRequestMessage requestSubject = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Subject?majord=" + SelectedMajorId + "&term=" + SelectedTerm + "&adminId=" + 0 + "&subjectName=" + SelectedName + "&status=");
            var responseSubject = _httpClient.SendAsync(requestSubject).Result;
            if (responseSubject.IsSuccessStatusCode)
            {
                var dataSubject = responseSubject.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Subjects = JsonSerializer.Deserialize<List<SubjectGetDTO>>(dataSubject, option);
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
                Majors = JsonSerializer.Deserialize<List<MajorGetDTO>>(dataMajor, option);
            }

            Majors.Insert(0, new MajorGetDTO { MajorId = 0, MajorName = "All" });

            ViewData["MajorSelectList"] = new SelectList(Majors, "MajorId", "MajorName");


            HttpRequestMessage requestTerm = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Term");
            var responseTerm = _httpClient.SendAsync(requestTerm).Result;
            if (responseTerm.IsSuccessStatusCode)
            {
                var dataMajor = responseTerm.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Terms = JsonSerializer.Deserialize<List<int>>(dataMajor, option);
            }

            ViewData["TermSelectList"] = new SelectList(Terms);
            return Page();

        }
    }
}
