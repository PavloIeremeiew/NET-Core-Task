namespace NET_Core_Task.BLL.DTO.Teachers
{
    public class TeacherUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
