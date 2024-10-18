using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.CourseLevelSemester;
using EduVersity.ViewModels.Level;
using EduVersity.ViewModels.Material;
using EduVersity.ViewModels.Post;
using EduVersity.ViewModels.Semester;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;
using NuGet.Packaging.Signing;
using System.ComponentModel.Design;


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
            CreateMap<SemesterReadVm, Data.Models.Semester>();
            CreateMap<SemesterAddVm, Data.Models.Semester>();
            CreateMap<SemesterUpdateVm, Data.Models.Semester>();
            CreateMap<Data.Models.Semester, SemesterUpdateVm>();

            //UserDepartment
            CreateMap<Data.Models.UserDepartment, UserDepartmentUpdateVm>();
            CreateMap<Data.Models.UserDepartment, UserDepartmentReadVm>();

            //CourseLevelSemester
            CreateMap< CourseLevelSemesterVm, Data.Models.CourseLevelSemester>();
            CreateMap< Data.Models.CourseLevelSemester, CourseLevelSemesterVm>();
            CreateMap<CourseReadVm, CourseLevelSemesterVm>();
            CreateMap<CourseLevelSemesterVm, CourseReadVm>();
            CreateMap<SemesterUpdateVm, SemesterDetailsVm>();

            //post
            CreateMap<Data.Models.Post, PostReadVm>();
            CreateMap<PostAddVm, Data.Models.Post>();
            CreateMap<PostUpdateVm, Data.Models.Post>();

            //StudentSemester
            CreateMap< SemesterReadVm, StudentSemesterReadVm>();

            //Material
            CreateMap<MaterialAddVM, Data.Models.Material>();
            CreateMap<Data.Models.Material, MaterialReadVM>();
            CreateMap<Data.Models.Material, MaterialEditVM>();
            CreateMap<MaterialEditVM, Data.Models.Material>();


        }
    }
}
