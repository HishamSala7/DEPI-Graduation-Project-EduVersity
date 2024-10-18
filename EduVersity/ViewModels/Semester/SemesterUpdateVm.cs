using EduVersity.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace EduVersity.ViewModels.Semester
{
    public class SemesterUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [UniqueStartDate]
        public DateOnly StartDate { get; set; }
        [UniqueEndDate]
        [Remote("CheckDates", "Semester", AdditionalFields = "StartDate", ErrorMessage = "Invalid Dates")]
        public DateOnly EndDate { get; set; }
        public int SemesterOrder {  get; set; }
    }
}
