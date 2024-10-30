namespace Business.Models
{
    public class ProjectInfo : BaseModel
    {
        public string Title { get; set; } // Tiêu đề dự án
        public string Description { get; set; } // Mô tả dự án
        public decimal Budget { get; set; } // Ngân sách
        public DateTime Deadline { get; set; } // Thời gian hoàn thành
        public string EmployerId { get; set; } // ID người tạo việc
        public List<string> FreelancerIds { get; set; } // Danh sách ID freelancer đã nộp đơn
    }
}
