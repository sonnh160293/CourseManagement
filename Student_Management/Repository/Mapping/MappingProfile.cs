using AutoMapper;
using BusinessObject.Models;
using DTO.GetDTO;
using DTO.PostDTO;
using DTO.UpdateDTO;
using DayOfWeek = BusinessObject.Models.DayOfWeek;
using Type = BusinessObject.Models.Type;

namespace Repository.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //room
            CreateMap<Building, BuildingGetDTO>().ReverseMap();
            CreateMap<Room, RoomGetDTO>().ReverseMap();

            //major
            CreateMap<Major, MajorGetDTO>().ReverseMap();

            //slot
            CreateMap<Slot, SlotGetDTO>().ReverseMap();
            CreateMap<SlotOfWeek, SlotOfWeekDTO>().ReverseMap();
            CreateMap<DayOfWeek, DayOfWeekDTO>().ReverseMap();

            //admin
            CreateMap<Admin, AdminGetDTO>().ReverseMap();

            //subject
            CreateMap<Subject, SubjectPrequisiteDTO>().ReverseMap();
            CreateMap<Subject, SubjectGetDTO>().ReverseMap();
            CreateMap<Subject, SubjectPostDTO>().ReverseMap();

            //account
            CreateMap<Account, AccountGetDTO>().ReverseMap();
            CreateMap<Account, AccountPostDTO>().ReverseMap();
            CreateMap<Admin, AdminGetDTO>().ReverseMap();
            CreateMap<Admin, AdminPostDTO>().ReverseMap();

            //role
            CreateMap<Role, RoleDTO>().ReverseMap();

            //curriculum
            CreateMap<Curriculum, CurriculumGetDTO>().ReverseMap();
            CreateMap<Curriculum, CurriculumPostDTO>().ReverseMap();
            CreateMap<SubjectCurriculum, SubjectCurriculumGetDTO>().ReverseMap();
            CreateMap<SubjectCurriculum, SubjectCurriculumPostDTO>().ReverseMap();
            CreateMap<SubjectCurriculumGetDTO, SubjectCurriculumPostDTO>().ReverseMap();


            //course
            CreateMap<Course, CoursePostDTO>().ReverseMap();
            CreateMap<Course, CourseGetDTO>().ReverseMap();
            CreateMap<CourseSchedule, CourseSchedulePostDTO>().ReverseMap();
            CreateMap<CourseSchedule, CourseScheduleGetDTO>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseGetDTO>().ReverseMap();



            //Semester
            CreateMap<Semester, SemesterGerDTO>().ReverseMap();
            CreateMap<Semester, SemesterPostDTO>().ReverseMap();


            //grade
            CreateMap<SubjectGrade, GradeGetDTO>().ReverseMap();
            CreateMap<GradeCategory, GradeCategoryGetDTO>().ReverseMap();
            CreateMap<SubjectGrade, GradeUpdateDTO>().ReverseMap();
            CreateMap<SubjectGrade, GradeSubjecPostDTO>().ReverseMap();


            //student
            CreateMap<Student, StudentGetDTO>().ReverseMap();


            //teacher
            CreateMap<Teacher, TeacherGetDTO>().ReverseMap();


            //Status
            CreateMap<Status, StatusGetDTO>().ReverseMap();


            //Type
            CreateMap<Type, TypeGetDTO>().ReverseMap();

        }
    }
}
