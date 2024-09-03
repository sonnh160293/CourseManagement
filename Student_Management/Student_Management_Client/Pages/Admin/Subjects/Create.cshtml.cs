using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Management_Client.ViewModel;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Subjects
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var token = httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [BindProperty]
        public CreateSubjectVM CreateSubjectVM { get; set; }

        public IActionResult OnGet()
        {
            HttpRequestMessage requestMajor = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Major");
            var responseMajor = _httpClient.SendAsync(requestMajor).Result;
            if (responseMajor.IsSuccessStatusCode)
            {
                var dataMajor = responseMajor.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var majors = JsonSerializer.Deserialize<List<MajorGetDTO>>(dataMajor, option);
                majors.Insert(0, new MajorGetDTO { MajorId = 0, MajorName = "All" });

                ViewData["MajorSelectList"] = new SelectList(majors, "MajorId", "MajorName");
            }



            HttpRequestMessage requestTerm = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Term");
            var responseTerm = _httpClient.SendAsync(requestTerm).Result;
            if (responseTerm.IsSuccessStatusCode)
            {
                var dataMajor = responseTerm.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var terms = JsonSerializer.Deserialize<List<int>>(dataMajor, option);
                ViewData["TermSelectList"] = new SelectList(terms);
            }



            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            CreateSubjectVM.CreatedBy = 1;
            if (CreateSubjectVM.SubjectPrequisite == 0)
            {
                CreateSubjectVM.SubjectPrequisite = null;
            }
            var json = JsonSerializer.Serialize(CreateSubjectVM);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request
            var apiUrl = "http://localhost:5172/api/Subject/CreateSubject";
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {

                var subjectId = response.Content.ReadAsStringAsync().Result;
                var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5172/api/Grade/AddGradeToSubject?subjectId={subjectId}");
                request.Headers.Add("accept", "*/*");
                var responseGrade = await _httpClient.SendAsync(request);

                return RedirectToPage("/Admin/Subjects/Index");
            }
            return OnGet();
        }
    }
}
