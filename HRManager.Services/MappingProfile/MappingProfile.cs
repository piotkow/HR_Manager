using AutoMapper;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.DTOs.DocumentDTO;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.ReportDTO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Absence, AbsencesEmployeeResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason));


        CreateMap<Account, AccountEmployeeResponse>()
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.ToString()));

        CreateMap<Document, DocumentEmployeeResponse>();

        CreateMap<Employee, EmployeePositionTeamResponse>()
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.PositionName))
            .ForMember(dest => dest.PositionDescription, opt => opt.MapFrom(src => src.Position.PositionDescription))
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
            .ForMember(dest => dest.TeamDescription, opt => opt.MapFrom(src => src.Team.TeamDescription));

        CreateMap<Report, ReportEmployeeResponse>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => ConvertToByteArray(src.Content)))
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity.ToString()));
    }

    private byte[] ConvertToByteArray(string content)
    {
        return Encoding.UTF8.GetBytes(content);
    }
}
