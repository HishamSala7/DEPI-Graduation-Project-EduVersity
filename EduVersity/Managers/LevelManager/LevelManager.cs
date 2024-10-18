using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Managers.CourseLevelSemesterManager;
using EduVersity.Repos.LevelRepo;
using EduVersity.ViewModels.Level;

namespace EduVersity.Managers.LevelManager
{
    public class LevelManager : ILevelManager
    {
        private readonly ILevelRepo _levelRepo;
        private readonly IMapper _mapper;

        public LevelManager(ILevelRepo levelRepo, IMapper mapper )
        {
            _levelRepo = levelRepo;
            _mapper = mapper;
        }


        public LevelReadVm? GetLevelById(int Id)
        {
            var level = _levelRepo.GetById(Id);

            if (level == null)
                return null;

            return _mapper.Map<LevelReadVm>(level);
        }

        public List<LevelReadVm> GetAllLevels()
        {
            var levels = _levelRepo.GetAll();
            return _mapper.Map<List<LevelReadVm>>(levels);
        }

        public void Add(LevelAddVm model)
        {
            if(model.Name != null)
            {
                _levelRepo.Add(_mapper.Map<Level>(model));
                _levelRepo.SaveChanges();
            }
        }

        public void Delete(int Id)
        {
            _levelRepo.Delete(Id);
            _levelRepo.SaveChanges();
        }

        public void Update(LevelUpdateVm model)
        {
            var dbLevel = _levelRepo.GetById(model.Id);

            if (dbLevel == null)
                return;

            _mapper.Map(model, dbLevel);
            _levelRepo.Update(dbLevel);
            _levelRepo.SaveChanges();
        }

        public LevelUpdateVm? MapLevelReadVmToLevelUpdateVm(LevelReadVm model)
        {
            return _mapper.Map<LevelUpdateVm>(model);
        }

        //public bool CheckForDelete(int Id)
        //{
            
        //}

    }
}
