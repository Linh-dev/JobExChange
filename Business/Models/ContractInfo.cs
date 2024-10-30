namespace Business.Models
{
    public class ContractInfo : BaseModel
    {
        public string ProjectId { get; set; } // ID dự án
        public string FreelancerId { get; set; } // ID freelancer
        public string EmployerId { get; set; } // ID người tạo việc
        public string Terms { get; set; } // Điều khoản hợp đồng
        public decimal PaymentAmount { get; set; } // Số tiền thanh toán
    }
}
