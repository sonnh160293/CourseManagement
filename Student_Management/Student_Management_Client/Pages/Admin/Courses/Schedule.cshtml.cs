using DTO.GetDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Courses
{
    public class ScheduleModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ScheduleModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var token = httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        [BindProperty]
        public CourseGetDTO CourseGetDTO { get; set; }

        [BindProperty]
        public List<CourseScheduleGetDTO> CourseScheduleGetDTOs { get; set; }



        public IActionResult OnGet(int id)
        {
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            HttpRequestMessage requestCourse = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Course/id?id={id}");
            var responseCourse = _httpClient.SendAsync(requestCourse).Result;
            if (responseCourse.IsSuccessStatusCode)
            {
                var dataSubject = responseCourse.Content.ReadAsStringAsync().Result;

                CourseGetDTO = JsonSerializer.Deserialize<CourseGetDTO>(dataSubject, option);
            }

            HttpRequestMessage requestCourseSchedule = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5172/api/Course/ScheduleOfCourse?courseId={id}");
            var responseCourseSchedule = _httpClient.SendAsync(requestCourseSchedule).Result;
            if (responseCourseSchedule.IsSuccessStatusCode)
            {
                var dataSubject = responseCourseSchedule.Content.ReadAsStringAsync().Result;

                CourseScheduleGetDTOs = JsonSerializer.Deserialize<List<CourseScheduleGetDTO>>(dataSubject, option);
            }






            return Page();

        }
    }
}
