
using AutoMapper;
using EduVersity.Data.Models;
using EduVersity.Repos.CourseLevelSemesterRepo;
using EduVersity.ViewModels.CourseLevelSemester;

namespace EduVersity.Managers.CourseLevelSemesterManager
{
    public class CourseLevelSemesterManager : ICourseLevelSemesterManager
    {
        private readonly ICourseLevelSemesterRepo _courseLevelSemesterRepo;
        private readonly IMapper _mapper;

        public CourseLevelSemesterManager(ICourseLevelSemesterRepo courseLevelSemesterRepo, IMapper mapper)
        {
            _courseLevelSemesterRepo = courseLevelSemesterRepo;
            _mapper = mapper;
        }

        public List<CourseLevelSemesterVm> GetAll()
        {
            var CourseLevelSemester = _courseLevelSemesterRepo.GetAll();
            return _mapper.Map<List<CourseLevelSemesterVm>>(CourseLevelSemester);
        }

        public void AddList(List<CourseLevelSemesterVm> model)
        {
            List<CourseLevelSemesterVm> CoursesToAdd = new List<CourseLevelSemesterVm>();
            
            foreach (var item in model)
                if (item.IsSelected)
                    CoursesToAdd.Add(item);

            _courseLevelSemesterRepo.AddList(_mapper.Map<List<CourseLevelSemester>>(CoursesToAdd));
            _courseLevelSemesterRepo.SaveChanges();
        }

        public void DeleteCoursesInSemester(int SemesterId)
        {
            _courseLevelSemesterRepo.DeleteAllCoursesInSemester(SemesterId);
            _courseLevelSemesterRepo.SaveChanges();
        }

        public List<CourseLevelSemesterVm> GetCoursesToExclude(int SemesterId)
        {
            var courses = _courseLevelSemesterRepo.GetCoursesToExclude(SemesterId);
            var res =  _mapper.Map<List<CourseLevelSemesterVm>>(courses);
            return res;
        }

        public List<CourseLevelSemesterVm> GetCoursesByLevelAndSemester(int LevelId, int SemesterId)
        {
            return GetAll().Where(x => x.LevelId == LevelId && x.SemesterId == SemesterId).ToList();
        }
    }

}
