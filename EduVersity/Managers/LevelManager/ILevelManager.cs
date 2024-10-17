using EduVersity.Data.Models;
using EduVersity.ViewModels.Level;

namespace EduVersity.Managers.LevelManager
{
    public interface ILevelManager
    {
        List<LevelReadVm> GetAllLevels();
        LevelReadVm? GetLevelById(int Id);
        void Add(LevelAddVm model);
        void Update(LevelUpdateVm model);
        void Delete(int Id);
        LevelUpdateVm? MapLevelReadVmToLevelUpdateVm(LevelReadVm model);
        //bool CheckForDelete(int Id);
    }
}
