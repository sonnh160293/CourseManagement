using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Curriculums
{
    public class DetailModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public DetailModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var token = httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [BindProperty]
        public List<SubjectCurriculumGetDTO> SubjectCurriculumGetDTO { get; set; }

        [BindProperty]
        public List<SubjectGetDTO> SubjectAvailable { get; set; }

        [BindProperty]
        public CurriculumGetDTO CurriculumGetDTO { get; set; }


        [BindProperty(SupportsGet = true)]
        public int CurriculumId { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<int> SubjectIds { get; set; }

        public IActionResult OnGet(int id)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            HttpRequestMessage requestSubjectCurriculum = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Curriculum/SubjectInCurriculum/{id}");
            var responseSubjectCurriculum = _httpClient.SendAsync(requestSubjectCurriculum).Result;
            if (responseSubjectCurriculum.IsSuccessStatusCode)
            {
                var dataSubject = responseSubjectCurriculum.Content.ReadAsStringAsync().Result;

                SubjectCurriculumGetDTO = JsonSerializer.Deserialize<List<SubjectCurriculumGetDTO>>(dataSubject, option);
            }

            HttpRequestMessage requestCurriculum = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Curriculum/{id}");
            var responseCurriculum = _httpClient.SendAsync(requestCurriculum).Result;
            if (responseCurriculum.IsSuccessStatusCode)
            {
                var dataSubject = responseCurriculum.Content.ReadAsStringAsync().Result;

                CurriculumGetDTO = JsonSerializer.Deserialize<CurriculumGetDTO>(dataSubject, option);
            }

            HttpRequestMessage requestSubject = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Curriculum/GetAvailableSubjects?curriculumId={id}");
            var responseSubject = _httpClient.SendAsync(requestSubject).Result;
            if (responseSubject.IsSuccessStatusCode)
            {
                var dataSubject = responseSubject.Content.ReadAsStringAsync().Result;

                SubjectAvailable = JsonSerializer.Deserialize<List<SubjectGetDTO>>(dataSubject, option);
            }




            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {


            var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5172/api/Curriculum/AddListSubjectToCurr?curriculumId={CurriculumId}");
            request.Headers.Add("accept", "*/*");
            var json = JsonSerializer.Serialize(SubjectIds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return OnGet(CurriculumId);
            }
            return Page();
        }

    }
}
