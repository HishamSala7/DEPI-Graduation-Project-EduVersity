using AutoMapper;
using Azure.Core;
using EduVersity.Data.Models;
using EduVersity.Managers.AccountManager;
using EduVersity.Repos.StudentRepo;
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

        public StudentManager(IStudentRepo studentRepo, IMapper mapper, IAccountManager accountManager)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _accountManager = accountManager;
        }

        public List<StudentReadVm> GetAllStudents()
        {
            var studs = _studentRepo.GetAll();
            return _mapper.Map<List<StudentReadVm>>(studs);
        }

        public StudentDetailsVm? GetStudentDetails(int Id)
        {
            var stud = _studentRepo.GetById(Id);
            if (stud == null)
                return null;

            return _mapper.Map<StudentDetailsVm>(stud);
        }

        public async Task<IdentityResult> AddStudent(StudentAddVm model)
        {
            IdentityResult result =  await _accountManager.Add(GetUserOfStudent(model));
            
            if(!result.Succeeded)
                return result;

            var userId = await _accountManager.GetUserId(model.Username);

            var stud = _mapper.Map<Data.Models.Student>(model);
            stud.ApplicationUserId = userId;
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


	}
}
