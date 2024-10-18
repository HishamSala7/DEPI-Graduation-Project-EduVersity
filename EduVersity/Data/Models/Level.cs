namespace EduVersity.Data.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelOrder { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<CourseLevelSemester> Semesters { get; set; }

    }
}
