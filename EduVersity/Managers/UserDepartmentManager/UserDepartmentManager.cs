using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.AccountManager;
using EduVersity.Managers.DepartmentManager;
using EduVersity.Repos.UserDepartmentRepo;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.User;
using EduVersity.ViewModels.UserDepartment;

namespace EduVersity.Managers.UserDepartmentManager
{
    public class UserDepartmentManager : IUserDepartmentManager
    {
        private readonly IUserDepartmentRepo _userDepartmentRepo;
        private readonly IDepartmentManager _departmentManager;
        private readonly IMapper _mapper;

        public UserDepartmentManager(IUserDepartmentRepo userDepartmentRepo, IDepartmentManager departmentManager,
            IMapper mapper)
        {
            _userDepartmentRepo = userDepartmentRepo;
            _departmentManager = departmentManager;
            _mapper = mapper;
        }

        public List<UserDepartmentReadVm> GetAll()
        {
            return _userDepartmentRepo.GetAll().Select(x => new UserDepartmentReadVm
            {
                UserId = x.ApplicationUserId,
                DepartmentId = x.DepartmentId
            }).ToList();

        }

        public bool AddProfessorToDepartment(UserDepartmentAddVm model)
        {
            bool IsExists = _userDepartmentRepo.IsExists(model.UserId);

            if (IsExists)
                return false;

            UserDepartment userDepartment = new UserDepartment();
            userDepartment.ApplicationUserId = model.UserId;
            userDepartment.DepartmentId = model.DepartmentId;

            _userDepartmentRepo.Add(userDepartment);
            _userDepartmentRepo.SaveChanges();

            return true;
        }

        public List<DepartmentReadVm> GetDepartments()
        {
            var depts = _departmentManager.GetAllDepartments();
            return depts;
        }

        public string GetProfessorDepartmentName(string UserId)
        {
            var userDepartmentRecord = _userDepartmentRepo.GetById(UserId);

            if (userDepartmentRecord == null)
                return "NA";

            var depts = GetDepartments();
            var res = depts.FirstOrDefault(x => x.Id == userDepartmentRecord.DepartmentId);

            return res.Name;
        }

        public UserDepartmentUpdateVm GetUserDepartmentById(string Id)
        {
            var userDepartment =  _userDepartmentRepo.GetById(Id);

            if (userDepartment == null)
                return null;

            return _mapper.Map<UserDepartmentUpdateVm>(userDepartment);
        }

        public void Update(UserDepartmentUpdateVm model, string UserId)
        {
            var userDepartment = _userDepartmentRepo.GetById(UserId);
            userDepartment.DepartmentId = model.DepartmentId;

            _userDepartmentRepo.Update(userDepartment);
            _userDepartmentRepo.SaveChanges();
        }

        public void Delete(string UserId)
        {
            _userDepartmentRepo.Delete(UserId);
            _userDepartmentRepo.SaveChanges();
        }

        //public List<UserDepartmentVm> GetPrfessorsNameWithDepartmentName()
        //{
        //    var res = (from userDept in _userDepartmentRepo.GetAll()
        //               join dept in GetDepartments()
        //               on userDept.DepartmentId equals dept.Id
        //               join user in _accountManager.GetAll()
        //               on userDept.ApplicationUserId equals user.UserId
        //               select new UserDepartmentVm
        //               {
        //                   FirstName = user.FirstName,
        //                   LastName = user.LastName,
        //                   UserId = userDept.ApplicationUserId,
        //                   DepartmentId = userDept.DepartmentId,
        //                   DepartmentName = dept.Name
        //               }).ToList();
        //    return res;
        //}

    }
}
