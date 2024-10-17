using EduVersity.ValidationAttributes;
using EduVersity.ViewModels.Course;
using EduVersity.ViewModels.Level;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.ViewModels.Semester
{
    public class SemesterAddVm
    {
        public string Name { get; set; }
        [UniqueStartDate]
        public DateOnly StartDate { get; set; }
        [UniqueEndDate]
        [Remote("CheckDates","Semester",AdditionalFields ="StartDate",ErrorMessage = "Invalid Dates")]
        public DateOnly EndDate { get; set; }
        public int SemesterOrder {  get; set; }
        
        //Dictionary<int, List<int>> Data { get; set; }
    }
}
