//using AspNetCore;
using EduVersity.Data.Models;
using EduVersity.Managers.AccountManager;
using EduVersity.Managers.CourseLevelSemesterManager;
using EduVersity.Managers.CourseManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Managers.StudentManager;
using EduVersity.Managers.UserDepartmentManager;
using EduVersity.Repos.EnrollmentRepo;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Enrollment;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
//using NuGet.Protocol;

namespace EduVersity.Managers.EnrollmentManager
{
    public class EnrollmentManager : IEnrollmentManager
    {
        private readonly IStudentManager _studentManager;
        private readonly ICourseManager _courseManager;
        private readonly IEnrollmentRepo _enrollmentRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly IUserDepartmentManager _userDepartmentManager;

        public EnrollmentManager(IStudentManager studentManager, ICourseManager courseManager,
            IEnrollmentRepo enrollmentRepo, UserManager<ApplicationUser> userManager,
            IDepartmentManager departmentManager, IUserDepartmentManager userDepartmentManager)
        {
            _studentManager = studentManager;
            _courseManager = courseManager;
            _enrollmentRepo = enrollmentRepo;
            _userManager = userManager;
            _departmentManager = departmentManager;
            _userDepartmentManager = userDepartmentManager;
        }

        public StudentDetailsVm GetLoggedInStudent(string Username)
        {
            var stud = _studentManager.GetStudentDetails(Username);
            
            if (stud == null)
                return null;

            return stud;
        }

        public ProfessorLoggedInVm GetLoggedInProfessor(string Username)
        {
            var prof = _userManager.Users.FirstOrDefault(x => x.UserName == Username && x.Discriminator == 'P');

            if (prof == null)
                return null;

            return new ProfessorLoggedInVm { ProfessorId = prof.Id };
        }

        public List<CourseLevelSemesterDepartmentVm> GetSemesterCourses(int SemesterId, int DepartmentId, int LevelId)
        {
            return _courseManager.GetCoursesInDepartmentInSemester(DepartmentId, SemesterId, LevelId);
        }

        public void AddEnrolledCourses(string Username, List<CourseLevelSemesterDepartmentVm> model)
        {
            List<Enrollment> enrollments = new List<Enrollment>();
            var StudentId = GetLoggedInStudent(Username).Id;

            foreach (var item in model)
            {
                if (item.IsSelected)
                {
                    enrollments.Add(new Enrollment
                    {
                        CourseId = item.CourseId,
                        StudentId = StudentId
                    });
                }
            }

            _enrollmentRepo.AddEnrollmentList(enrollments);
            _enrollmentRepo.SaveChanges();
        }

        public List<CourseLevelSemesterDepartmentVm> GetUnEnrolledCourses(string Username, int SemesterId,
            int DepartmentId, int LevelId)
        {
            var stud = GetLoggedInStudent(Username);
            var semesterCourses = GetSemesterCourses(SemesterId, DepartmentId, LevelId);
            var enrollments = _enrollmentRepo.GetEnrollments(stud.Id);

            if (enrollments == null || enrollments.Count == 0)
                return semesterCourses;

            List<CourseLevelSemesterDepartmentVm> courses = new List<CourseLevelSemesterDepartmentVm>();

            foreach (var semCourse in semesterCourses)
            {
                foreach(var enroll in enrollments)
                {
                    if (semCourse.CourseId == enroll.CourseId)
                        courses.Add(semCourse);
                }
            }

            return semesterCourses.Except(courses).ToList();
        }


        public List<EnrollmentDetailsVm> GetStudentEnrollmentsDetails(int StudentId)
        {
            var enrollments = _enrollmentRepo.GetEnrollments(StudentId);

            var res = (from crs in _courseManager.GetAllCourses()
                       join enroll in enrollments
                       on crs.Id equals enroll.CourseId
                       select new EnrollmentDetailsVm
                       {
                           CourseName = crs.Name,
                           Grade = enroll.Grade,
                           ProfessorId = enroll.ApplicationUserId
                       }).ToList();

            
            return res;
        }

        public List<ProfessorCourseReadVm> GetProfessorEnrollmetsDetails(string ProfessorId)
        {
            return (from enroll in GetProfessorEnrollments(ProfessorId)
                       join crs in _courseManager.GetAllCourses()
                       on enroll.CourseId equals crs.Id
                       select new ProfessorCourseReadVm
                       {
                           CourseName = crs.Name,
                           CourseId = enroll.CourseId,
                           ProfessorId = enroll.ProfessorId
                       }).ToList();
        }

        public List<EnrollCourseDepartmentVm> GetEnrolledCourses()
        {
            var res = (from enroll in _enrollmentRepo.GetAll()
                       join crs in _courseManager.GetAllCourses()
                       on enroll.CourseId equals crs.Id
                       join dept in _departmentManager.GetAllDepartments()
                       on crs.DepartmentId equals dept.Id
                       select new EnrollCourseDepartmentVm
                       {
                           CourseId = enroll.CourseId,
                           CourseName = crs.Name,
                           DepartmentName = dept.Name,
                           ProfessorId = enroll.ApplicationUserId
                       }).DistinctBy(x=>x.CourseName).ToList(); 

            return res;
        }

        public List<ProfessorDepartmentVm> GetAllProfessors()
        {
            return (from user in _userManager.Users.Where(x => x.Discriminator == 'P').ToList()
                    join userDept in _userDepartmentManager.GetAll()
                    on user.Id equals userDept.UserId
                    join dept in _departmentManager.GetAllDepartments()
                    on userDept.DepartmentId equals dept.Id
                    select new ProfessorDepartmentVm
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.Id,
                        DepartmentName = dept.Name
                    }).ToList();
        }

        public void AssignTeachersToCourses(List<EnrollCourseDepartmentVm> model)
        {
            var Enrollments = _enrollmentRepo.GetAll();

            foreach(var item in model)
                foreach(var enroll in Enrollments)
                    if (item.CourseId == enroll.CourseId)
                        enroll.ApplicationUserId = item.ProfessorId;

            _enrollmentRepo.AssignProfessorsToCourses(Enrollments);
            _enrollmentRepo.SaveChanges();

        }

        public List<ProfessorCourseReadVm> GetCoursesTeacher()
        {
            var TeacherEnrollments = (from user in _userManager.Users.Where(x => x.Discriminator == 'P').ToList()
                                      join enroll in _enrollmentRepo.GetAll()
                                      on user.Id equals enroll.ApplicationUserId
                                      join crs in _courseManager.GetAllCourses()
                                      on enroll.CourseId equals crs.Id
                                      select new ProfessorCourseReadVm
                                      {
                                          FirstName = user.FirstName,
                                          LastName = user.LastName,
                                          CourseId = enroll.CourseId,
                                          ProfessorId = enroll.ApplicationUserId,
                                          CourseName = crs.Name
                                      }).DistinctBy(x=>x.CourseId).ToList();

            return TeacherEnrollments;
            
        }

        public List<StudentGradeEditVm> GetStudentsDataInCourse(int CourseId)
        {
            var res = (from enroll in _enrollmentRepo.GetAll().Where(x=>x.CourseId == CourseId).ToList()
                       join stud in _studentManager.GetAllStudents()
                       on enroll.StudentId equals stud.Id
                       select new StudentGradeEditVm
                       {
                           CourseId = enroll.CourseId,
                           StudentId = enroll.StudentId,
                           Grade = enroll.Grade,
                           FirstName = stud.FirstName,
                           LastName = stud.LastName
                       }).ToList();

            return res;
        }

        public void UpdateStudentsGrades(int CourseId, List<StudentGradeEditVm> model)
        {
            var StudentsGrades = _enrollmentRepo.GetAll().Where(x => x.CourseId == CourseId).ToList();

            foreach(var item in model)

                foreach(var studenGrade in StudentsGrades)

                    if (studenGrade.StudentId == item.StudentId)
                        studenGrade.Grade = item.Grade;

            _enrollmentRepo.UpdateStudentsGradesList(StudentsGrades);
            _enrollmentRepo.SaveChanges();

        }


        private List<ProfessorCourseReadVm> GetProfessorEnrollments(string ProfessorId)
        {
            var res = _enrollmentRepo.GetAll()
                .Where(x => x.ApplicationUserId == ProfessorId)
                .DistinctBy(x => x.CourseId)
                .ToList();

            return res.Select(x => new ProfessorCourseReadVm
            {
                CourseId = x.CourseId,
                ProfessorId = x.ApplicationUserId
            }).ToList();
        }

    }
}
