using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.UserDepartmentRepo
{
    public class UserDepartmentRepo: IUserDepartmentRepo
    {
        private readonly WebAppContext _context;

        public UserDepartmentRepo(WebAppContext context) 
        {
            _context = context;
        }

        public List<UserDepartment> GetAll()
        {
            return _context.UserDepartments.ToList();
        }

        public UserDepartment? GetById(string UserId)
        {
            return _context.UserDepartments.Where(x=>x.ApplicationUserId == UserId).FirstOrDefault();
        }

        public bool IsExists(string UserId)
        {
            return _context.UserDepartments.Any(x=>x.ApplicationUserId==UserId);   
        }

        public void Add(UserDepartment model)
        {
            if(model.ApplicationUserId !=null)
                _context.UserDepartments.Add(model);
        }

        public void Delete(string Id)
        {
            var userDept = GetById(Id);
            if (userDept != null)
                _context.UserDepartments.Remove(userDept);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(UserDepartment model)
        {

        }
    }
}
