using DTO.GetDTO;
using DTO.PostDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Student_Management_Client.Pages.Admin.Courses
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
        public CoursePostDTO CoursePost { get; set; }


        [BindProperty]
        public List<SlotOfWeekDTO> SlotOfWeekDTOs { get; set; }

        [BindProperty]
        public List<DayOfWeekDTO> DayOfWeekDTOs { get; set; }
        [BindProperty]
        public List<SlotGetDTO> SlotGetDTOs { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<int> SlotOfWeekSelected { get; set; }


        [BindProperty(SupportsGet = true)]
        public int RoomId { get; set; }


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

            HttpRequestMessage requestSlotOfWeek = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Slot/SlotInWeek");
            var responseSlotOfWeek = _httpClient.SendAsync(requestSlotOfWeek).Result;
            if (responseSlotOfWeek.IsSuccessStatusCode)
            {
                var dataMajor = responseSlotOfWeek.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                SlotOfWeekDTOs = JsonSerializer.Deserialize<List<SlotOfWeekDTO>>(dataMajor, option);

            }

            HttpRequestMessage requestSlot = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Slot");
            var responseSlot = _httpClient.SendAsync(requestSlot).Result;
            if (responseSlot.IsSuccessStatusCode)
            {
                var dataMajor = responseSlot.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                SlotGetDTOs = JsonSerializer.Deserialize<List<SlotGetDTO>>(dataMajor, option);

            }



            HttpRequestMessage requestDay = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Day");
            var responseDay = _httpClient.SendAsync(requestDay).Result;
            if (responseDay.IsSuccessStatusCode)
            {
                var dataMajor = responseDay.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                DayOfWeekDTOs = JsonSerializer.Deserialize<List<DayOfWeekDTO>>(dataMajor, option);

            }

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
                var semesterDisplayList = semesters.Select(s => new
                {
                    SemesterId = s.SemesterId,
                    SemesterDisplayName = $"{s.SemesterName} {s.Year}"
                }).ToList();

                ViewData["SemesterSelectList"] = new SelectList(semesterDisplayList, "SemesterId", "SemesterDisplayName");
            }


            HttpRequestMessage requestBuilding = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5172/api/Building");
            var responseBuilding = _httpClient.SendAsync(requestBuilding).Result;
            if (responseBuilding.IsSuccessStatusCode)
            {
                var dataMajor = responseBuilding.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var buildings = JsonSerializer.Deserialize<List<BuildingGetDTO>>(dataMajor, option);
                ViewData["BuildingSelectList"] = new SelectList(buildings, "BuildingId", "BuildingName");

            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            CoursePost.CreatedBy = 1;


            var json = JsonSerializer.Serialize(CoursePost);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Send the POST request
            var apiUrl = "http://localhost:5172/api/Course/AddCourse";
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var courseId = JsonSerializer.Deserialize<int>(data, option);

                var jsonContent = JsonSerializer.Serialize(SlotOfWeekSelected);

                var request = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5172/api/Course/AddCourseSchedule?courseId={courseId}&roomId={RoomId}");
                request.Headers.Add("accept", "*/*");
                request.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var responseSchedule = await _httpClient.SendAsync(request);
                if (responseSchedule.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Admin/Courses/Index");
                }
                return Page();

            }
            return OnGet();
        }
    }
}
