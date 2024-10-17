using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.AccountManager;
using EduVersity.Repos.MaterialRepo;
using EduVersity.Repos.StudentRepo;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Material;
using EduVersity.ViewModels.Student;
using EduVersity.ViewModels.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduVersity.Managers.MaterialManager
{
    public class MaterialManager : IMaterialManager
    {
        private readonly IMaterialRepo _materialRepo;
        private readonly IMapper _mapper;
        
        public MaterialManager(IMaterialRepo materialRepo, IMapper mapper)
        {
            _materialRepo = materialRepo;
            _mapper = mapper;
        }

        public void Add(MaterialAddVM model)
        {
            _materialRepo.Add(_mapper.Map<Material>(model));
            _materialRepo.SaveChanges();
        }

        public void Delete(int Id)
        {
            _materialRepo.Delete(Id);
            _materialRepo.SaveChanges();
        }

        public List<MaterialReadVM> GetAllMaterials()
        {
            var materials = _materialRepo.GetAll();
            return _mapper.Map<List<MaterialReadVM>>(materials);
        }

        public MaterialEditVM? GetMaterialById(int id)
        {
            var material = _materialRepo.GetById(id);

            if (material == null)
                return null;

            return _mapper.Map<MaterialEditVM>(material);
        }
      
        public void Update(MaterialEditVM model)
        {
            var material = _materialRepo.GetById(model.Id);

            if (material == null)
                return;

            _mapper.Map(model, material);
            _materialRepo.Update(material);
            _materialRepo.SaveChanges();
        }
    }





}


