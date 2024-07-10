namespace WebApi.Dtos.PolicyDtos
{
    public class PolicyCreateDto
    {
        public string? PolicyId { get; set; }

        public string? PolicyHolderName { get; set; }

        public DateTime? PolicyStartDate { get; set; }

        public DateTime? PolicyEndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
