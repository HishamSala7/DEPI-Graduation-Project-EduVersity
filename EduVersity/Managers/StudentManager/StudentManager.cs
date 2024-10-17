//using AspNetCore;
using AutoMapper;
//using Azure.Core;
using EduVersity.Data.Models;
using EduVersity.Managers.AccountManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Managers.LevelManager;
using EduVersity.Managers.SemesterManager;
using EduVersity.Repos.StudentRepo;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace EduVersity.Managers.StudentManager
{
    public class StudentManager : IStudentManager
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;
        private readonly IAccountManager _accountManager;
        private readonly ILevelManager _levelManager;
        private readonly IDepartmentManager _departmentManager;
        private readonly ISemesterManager _semesterManager;

        public StudentManager(IStudentRepo studentRepo, IMapper mapper, IAccountManager accountManager,
            ILevelManager levelManager, IDepartmentManager departmentManager,
            ISemesterManager semesterManager)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _accountManager = accountManager;
            _levelManager = levelManager;
            _departmentManager = departmentManager;
            _semesterManager = semesterManager;
        }

        public List<StudentReadVm> GetAllStudents()
        {
            var studs = _studentRepo.GetAll();
            var res = (from s in studs
                       join level in _levelManager.GetAllLevels()
                       on s.LevelId equals level.Id
                       select new StudentReadVm
                       {
                           Id = s.Id,
                           FirstName = s.FirstName,
                           LastName = s.LastName,
                           Username = s.Username,
                           Status = s.Status,
                           LevelName = level.Name
                       }).ToList();

            return res;
        }

        public StudentDetailsVm? GetStudentDetails(int Id)
        {
            var stud = _studentRepo.GetById(Id);
            if (stud == null)
                return null;

            var levelName = _levelManager.GetLevelById((int)stud.LevelId).Name;

            var studDetails =  _mapper.Map<StudentDetailsVm>(stud);
            studDetails.LevelName = levelName;

            return studDetails;
        }

        public StudentDetailsVm? GetStudentDetails(string Username)
        {
            var studs = _studentRepo.GetAll();
            var stud = studs.FirstOrDefault(x => x.Username == Username);
            
            if (stud == null)
                return null;

            var studDetails = _mapper.Map<StudentDetailsVm>(stud);

            return studDetails;
        }

        public StudentSemesterAddVm GetStudentSemesterAddVm()
        {
            StudentSemesterAddVm studentSemester = new StudentSemesterAddVm();
            studentSemester.Students = _mapper.Map<List<StudentReadVm>>(GetAllStudents());
            var semesters = _semesterManager.GetAllSemesters();
            studentSemester.semesters = semesters.Select(x => new StudentSemesterReadVm
            {
                 SemesterId = x.Id,
                  SemesterName = x.Name
            }).ToList();
            
            return studentSemester;
        }

        public void AddStudentsInSemester(StudentSemesterAddVm model)
        {
            var studs = _studentRepo.GetAll();

            foreach(var item in model.SelectedStudents)
            {
                if (item.IsSelected)
                {
                    var myStud = studs.FirstOrDefault(x=>x.Id == item.Id);
                    myStud.SemesterId = model.SelectedSemester.Id;
                    _studentRepo.Update(myStud);
                    _studentRepo.SaveChanges();
                }
            }
    }

        public async Task<IdentityResult> AddStudent(StudentAddVm model)
        {
            IdentityResult result =  await _accountManager.Add(GetUserOfStudent(model));
            
            if(!result.Succeeded)
                return result;

            var userId = await _accountManager.GetUserId(model.Username);
            var level = _levelManager.GetAllLevels().FirstOrDefault(x => x.LevelOrder == 1);
            
            var stud = _mapper.Map<Data.Models.Student>(model);
            stud.ApplicationUserId = userId;
            stud.LevelId = level.Id;
            _studentRepo.Add(stud);
            _studentRepo.SaveChanges();

            return result;
        }

        public async Task<IdentityResult> DeleteStudent(int Id, string Username)
        {
            _studentRepo.Delete(Id);
            _studentRepo.SaveChanges();

            IdentityResult result = await _accountManager.Delete(Username);

            return result;
        }

        public async Task<IdentityResult> UpdateStudent(StudentUpdateVm model)
        {
            var dbStudent = _studentRepo.GetById(model.Id);

            if (dbStudent == null)
                return null;

            _mapper.Map(model, dbStudent);
            _studentRepo.Update(dbStudent);
            
            IdentityResult result = await _accountManager.Update(_mapper.Map<UserEditVm>(dbStudent));
            if (result.Succeeded)
            {
                _studentRepo.SaveChanges();
                return result;
            }
            return result;
        }

        public string GetDepartmentName(int StudentId)
        {
            var deptId = _studentRepo.GetById(StudentId).DepartmentId;

            if (deptId == null)
                return "NA";

            return _departmentManager.GetDepartmentById((int)deptId).Name;
        }

        public List<DepartmentReadVm> GetDepartments()
        {
            return _departmentManager.GetAllDepartments();
        }

        public void AssignToDepartment(int StudentId, AssignToDepartmentVm model)
        {
            var stud = _studentRepo.GetById(StudentId);
            stud.DepartmentId = model.DepartmentId;
            _studentRepo.Update(stud);
            _studentRepo.SaveChanges();
        }

        public void DeleteDepartment(int StudentId)
        {
            var stud = _studentRepo.GetById(StudentId);
            stud.DepartmentId = null;
            _studentRepo.Update(stud);
            _studentRepo.SaveChanges();
        }

        private UserAddVm GetUserOfStudent(StudentAddVm model)
        {
            UserAddVm userVm = new UserAddVm();
            userVm.FirstName = model.FirstName;
            userVm.MidName = model.MidName;
            userVm.LastName = model.LastName;
            userVm.Username = model.Username;
            userVm.Gender = model.Gender;
            userVm.SSN = model.SSN;
            userVm.BirthDate = model.BirthDate;
            userVm.Discriminator = 'S';

            return userVm;
        }

        public StudentUpdateVm GetStudentUpdateVmFromDetails(StudentDetailsVm model)
        {
            return _mapper.Map<StudentUpdateVm>(model); 
        }

		public StudentDeleteVm GetStudentDeleteVmFromDetails(StudentDetailsVm model)
		{
			return _mapper.Map<StudentDeleteVm>(model);
		}

        //public List<Student> GetStudentsByLevelId(int LevelId)
        //{
        //    return _studentRepo.GetStudentsByLevelId(LevelId);
        //}
	}
}
