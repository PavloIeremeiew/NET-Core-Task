namespace NET_Core_Task.DAL.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public List<Course>? Courses { get; set; }
    }
}
