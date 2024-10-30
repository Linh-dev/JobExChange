namespace Business.Models
{
    public class ReviewInfo : BaseModel
    {
        public string ProjectId { get; set; } // ID dự án
        public string ReviewerId { get; set; } // ID người đánh giá (freelancer hoặc employer)
        public string RevieweeId { get; set; } // ID người được đánh giá
        public int Rating { get; set; } // Đánh giá (1-5)
        public string Comment { get; set; } // Nhận xét
    }
}
