using EduVersity.ViewModels.Semester;

namespace EduVersity.ViewModels.Student
{
    public class StudentSemesterAddVm
    {
        public List<StudentReadVm> Students { get; set; }
        public List<StudentSemesterReadVm> semesters { get; set; }
        public SemesterReadVm SelectedSemester { get; set; }
        public List<SelectStudentVm> SelectedStudents { get; set; }
        
    }

}
