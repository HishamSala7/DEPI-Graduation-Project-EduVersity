namespace EduVersity.Data.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string? HallName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
