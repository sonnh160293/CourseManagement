using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Student_Management_Client.ViewModel;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Courses
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
        public int SelectedStatus { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedMajorId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedSemester { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedTeacher { get; set; }

        public List<MajorGetDTO> Majors { get; set; }


        public List<int> Terms { get; set; }

        [BindProperty]
        public List<CourseGetDTO> Courses { get; set; }

        public IActionResult OnGet()
        {
            HttpRequestMessage requestSubject = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Course/GetCourses?status={SelectedStatus}&semesterId={SelectedSemester}&majorId={SelectedMajorId}");
            var responseSubject = _httpClient.SendAsync(requestSubject).Result;
            if (responseSubject.IsSuccessStatusCode)
            {
                var dataSubject = responseSubject.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                Courses = JsonSerializer.Deserialize<List<CourseGetDTO>>(dataSubject, option);
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


            HttpRequestMessage requestSemester = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Semester");
            var responseSemester = _httpClient.SendAsync(requestSemester).Result;
            if (responseSemester.IsSuccessStatusCode)
            {
                var dataMajor = responseSemester.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var semesters = JsonSerializer.Deserialize<List<SemesterGerDTO>>(dataMajor, option);
                var semesterDisplayList = semesters.Select(s => new SemesterVM
                {
                    SemesterId = s.SemesterId,
                    SemesterDisplayName = $"{s.SemesterName} {s.Year}"
                }).ToList();
                semesterDisplayList.Insert(0, new SemesterVM { SemesterId = 0, SemesterDisplayName = "All" });

                ViewData["SemesterSelectList"] = new SelectList(semesterDisplayList, "SemesterId", "SemesterDisplayName");
            }

            HttpRequestMessage requestStatus = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Status/GetStatus/3");
            var responseStatus = _httpClient.SendAsync(requestStatus).Result;
            if (responseStatus.IsSuccessStatusCode)
            {
                var dataMajor = responseStatus.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var Statuses = JsonSerializer.Deserialize<List<StatusGetDTO>>(dataMajor, option);
                Statuses.Insert(0, new StatusGetDTO { StatusId = 0, StatusName = "All" });
                ViewData["StatusSelectList"] = new SelectList(Statuses, "StatusId", "StatusName");
            }
            return Page();

        }
    }
}
