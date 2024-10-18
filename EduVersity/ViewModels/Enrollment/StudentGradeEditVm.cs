namespace EduVersity.ViewModels.Enrollment
{
    public class StudentGradeEditVm
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public string MidName { get; set; }
        //public float CurrentGrade { get; set; }
        public float Grade { get; set; }

    }
}
