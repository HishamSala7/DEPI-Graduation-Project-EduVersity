using EduVersity.Data.Models;
using EduVersity.Models.AppContext;
using EduVersity.Repos.GenericRepo;
using System.Xml.Linq;

namespace EduVersity.Repos.SemesterRepo
{
    public class SemesterRepo : GenericRepo<Semester>, ISemesterRepo
    {
        private readonly WebAppContext _Context;

        public SemesterRepo(WebAppContext webAppContext) : base(webAppContext)
        {
            _Context = webAppContext;
        }
        
    }
}
