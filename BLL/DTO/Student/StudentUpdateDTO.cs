namespace NET_Core_Task.BLL.DTO.Student
{
    public class StudentUpdateDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }

        public List<int>? CoursesIds { get; set; }
    }
}
