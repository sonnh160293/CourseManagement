using DTO.GetDTO;
using DTO.UpdateDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Subjects.Grades
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
        public List<GradeGetDTO> GradesSubject { get; set; }


        [BindProperty]
        public SubjectGetDTO Subject { get; set; }

        [BindProperty]
        public List<GradeUpdateDTO> GradeUpdateDTOs { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

        public IActionResult OnGet(int subjectId)
        {
            HttpRequestMessage requestGrade = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Grade/GradesOfSubject?subjectId={subjectId}");
            var responseGrade = _httpClient.SendAsync(requestGrade).Result;
            if (responseGrade.IsSuccessStatusCode)
            {
                var dataSubject = responseGrade.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                GradesSubject = JsonSerializer.Deserialize<List<GradeGetDTO>>(dataSubject, option);
            }


            HttpRequestMessage requestSubject = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Subject/{subjectId}");
            var responseSubject = _httpClient.SendAsync(requestSubject).Result;
            if (responseSubject.IsSuccessStatusCode)
            {
                var dataSubject = responseSubject.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Subject = JsonSerializer.Deserialize<SubjectGetDTO>(dataSubject, option);
            }


            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5172/api/Grade/UpdateGrades");
            request.Headers.Add("accept", "*/*");
            var json = JsonSerializer.Serialize(GradeUpdateDTOs);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return OnGet(SubjectId);
            }
            return Page();
        }
    }
}
