using HRManager.Models.Entities;

namespace HRManager.Services.DTOs.TeamDTO
{
    public class TeamRequest
    {
        public required string TeamName { get; set; }
        public required string TeamDescription { get; set; }
        public int DepartmentID {  get; set; }
    }
}
