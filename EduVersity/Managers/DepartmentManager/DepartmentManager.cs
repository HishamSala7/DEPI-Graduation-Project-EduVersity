using AutoMapper;
using EduVersity.Models;
using EduVersity.Repos.DepartmentRepo;
using EduVersity.ViewModels.Department;

namespace EduVersity.Managers.DepartmentManager
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IMapper _mapper;

        public DepartmentManager(IDepartmentRepo departmentRepo , IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
        }

        public List<DepartmentReadVm> GetAllDepartments()
        {
            var depts = _departmentRepo.GetAll();

            return depts.Select(x => new DepartmentReadVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public DepartmentReadVm? GetDepartmentById(int id)
        {
            var dept = _departmentRepo.GetById(id);
            if(dept == null)
                return null;
            
            return new DepartmentReadVm() { Name = dept.Name, Id = dept.Id }; 
        }


        public void AddDepartment(DepartmentAddVm model)
        {
            if(model.Name != null)
            {
                _departmentRepo.Add(new Data.Models.Department { Name = model.Name });
                _departmentRepo.SaveChanges();
            }
        }

        public void DeleteDepartment(int Id)
        {
            _departmentRepo.Delete(Id);
            _departmentRepo.SaveChanges();
        }

        

        public void UpdateDepartment(DepartmentUpdateVm model)
        {
            if(model.Name != null)
            {
                var dbDept = _departmentRepo.GetById(model.Id);
                
                if (dbDept == null)
                    return;

                dbDept.Name = model.Name;
                _departmentRepo.Update(dbDept);
                _departmentRepo.SaveChanges();
            }
        }
    }
}
