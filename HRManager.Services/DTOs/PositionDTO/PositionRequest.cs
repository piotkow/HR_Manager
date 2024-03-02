namespace HRManager.Services.DTOs.PositionDTO
{
    public class PositionRequest
    {
        public required string PositionName { get; set; }
        public string? PositionDescription { get; set; }
    }
}
