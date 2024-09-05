namespace NET_Core_Task.DAL.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Teacher? Teacher { get; set; }
        public List<Student>? Students { get; set; }
    }
}
