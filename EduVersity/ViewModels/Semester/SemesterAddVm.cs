using EduVersity.ValidationAttributes;
using EduVersity.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.ViewModels.Semester
{
    public class SemesterAddVm
    {
        public string Name { get; set; }

        //public DateOnly StartDate { get; set; }
        //[Remote("CheckDates","Semester",AdditionalFields ="StartDate",ErrorMessage = "Invalid Dates")]
        //public DateOnly EndDate { get; set; }
        
        //Dictionary<int, List<int>> Data { get; set; }
    }
}
