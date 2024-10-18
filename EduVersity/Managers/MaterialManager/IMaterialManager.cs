using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Department;
using EduVersity.ViewModels.Material;
using EduVersity.ViewModels.Student;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Drawing2D;

namespace EduVersity.Managers.MaterialManager
{
    public interface IMaterialManager
    {
        //MaterialEditVM? GetMaterialById(int id);
        List<MaterialReadVM> GetAllMaterials(int CourseId);
        void Delete(int Id);
        void Add(MaterialAddVM model);
        void Update(MaterialEditVM material);
    }
}
