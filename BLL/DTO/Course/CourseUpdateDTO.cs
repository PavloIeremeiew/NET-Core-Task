using NET_Core_Task.DAL.Entities;

namespace NET_Core_Task.BLL.DTO.Course
{
    public class CourseUpdateDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int TeacherId { get; set; }
    }
}
