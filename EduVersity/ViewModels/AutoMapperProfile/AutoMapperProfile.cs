using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Semester;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;
using NuGet.Packaging.Signing;


namespace EduVersity.ViewModels.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Student
            CreateMap<Data.Models.Student, StudentReadVm>();
            CreateMap<Data.Models.Student, StudentDetailsVm>();
            CreateMap<StudentAddVm,Data.Models.Student>();
            CreateMap<StudentUpdateVm,Data.Models.Student>();
            CreateMap<StudentDetailsVm, StudentUpdateVm>();
            CreateMap<StudentDetailsVm, StudentDeleteVm>();

            //Studet To User
            CreateMap<Data.Models.Student,UserEditVm>();

            //Course
            CreateMap<Data.Models.Course, CourseEditVm>();
            CreateMap<CourseEditVm, Data.Models.Course>();
            CreateMap<CourseAddVm, Data.Models.Course>();

            //level
            CreateMap<Data.Models.Level, LevelReadVm>();
            CreateMap<LevelAddVm, Data.Models.Level>();
            CreateMap<LevelUpdateVm, Data.Models.Level>();
            CreateMap<LevelReadVm, LevelUpdateVm>();

            //Semester
            CreateMap<Data.Models.Semester, SemesterReadVm>();
            CreateMap<Data.Models.Semester, SemesterUpdateVm>();
            CreateMap<SemesterAddVm, Data.Models.Semester>();
            CreateMap<SemesterUpdateVm, Data.Models.Semester>();

            //UserDepartment
            CreateMap<Data.Models.UserDepartment, UserDepartmentUpdateVm>();



        }
    }
}
