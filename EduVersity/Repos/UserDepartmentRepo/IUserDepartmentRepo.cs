using EduVersity.Data.Models;
using EduVersity.Repos.GenericRepo;

namespace EduVersity.Repos.UserDepartmentRepo
{
    public interface IUserDepartmentRepo
    {
        List<UserDepartment> GetAll();
        UserDepartment? GetById(string UserId);
        void Add(UserDepartment model);
        void Update(UserDepartment model);
        void Delete(string Id);
        void SaveChanges();

        bool IsExists(string UserId);
    }
}
