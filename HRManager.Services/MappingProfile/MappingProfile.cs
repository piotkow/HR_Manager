using AutoMapper;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.AbsenceDTO;
using HRManager.Services.DTOs.AccountDTO;
using HRManager.Services.DTOs.DocumentDTO;
using HRManager.Services.DTOs.EmployeeDTO;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.DTOs.PositionDTO;
using HRManager.Services.DTOs.ReportDTO;
using HRManager.Services.DTOs.TeamDTO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Absence, AbsencesEmployeeResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason));

        CreateMap<Account, AccountEmployeeResponse>()
            .ForMember(dest => dest.AccountID, opt => opt.MapFrom(src => src.AccountID))
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Employee.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Employee.Phone))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Employee.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Employee.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Employee.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Employee.PostalCode))
            .ForMember(dest => dest.DateOfEmployment, opt => opt.MapFrom(src => src.Employee.DateOfEmployment))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Employee.Photo))
            .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => src.Employee.TeamID));



        CreateMap<Document, DocumentEmployeeResponse>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Employee.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Employee.LastName));

        CreateMap<Photo, FileResponse>()
            .ForMember(dest => dest.FileID, opt => opt.MapFrom(src => src.PhotoID))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));

        CreateMap<Document, FileResponse>()
            .ForMember(dest => dest.FileID, opt => opt.MapFrom(src => src.DocumentID))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));

        CreateMap<Employee, EmployeePositionTeamResponse>()
            .ForMember(dest => dest.PositionID, opt => opt.MapFrom(src => src.Position.PositionID))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.PositionName))
            .ForMember(dest => dest.PositionDescription, opt => opt.MapFrom(src => src.Position.PositionDescription))
            .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => src.Team.TeamID))
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.TeamName))
            .ForMember(dest => dest.TeamDescription, opt => opt.MapFrom(src => src.Team.TeamDescription))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Team.Department))
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.Photo));

        CreateMap<Report, ReportEmployeeResponse>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => ConvertToByteArray(src.Content)))
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity.ToString()));

        CreateMap<AbsenceRequest, Absence>()
            .ForMember(dest => dest.AbsenceID, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason));

        CreateMap<AccountRequest, Account>()
            .ForMember(dest => dest.AccountID, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType));

        CreateMap<DocumentRequest, Document>()
            .ForMember(dest => dest.DocumentID, opt => opt.Ignore())
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => src.IssueDate))
            .ForMember(dest => dest.Filename, opt => opt.MapFrom(src => src.Filename))
            .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.Uri));

        CreateMap<EmployeeRequest, Employee>()
            .ForMember(dest => dest.EmployeeID, opt => opt.Ignore())
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.DateOfEmployment, opt => opt.MapFrom(src => src.DateOfEmployment))
            .ForMember(dest => dest.PositionID, opt => opt.MapFrom(src => src.PositionID))
            .ForMember(dest => dest.TeamID, opt => opt.MapFrom(src => src.TeamID));


        CreateMap<PositionRequest, Position>()
            .ForMember(dest => dest.PositionID, opt => opt.Ignore())
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.PositionName))
            .ForMember(dest => dest.PositionDescription, opt => opt.MapFrom(src => src.PositionDescription));

        CreateMap<ReportRequest, Report>()
            .ForMember(dest => dest.ReportID, opt => opt.Ignore())
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dest => dest.AuthorID, opt => opt.MapFrom(src => src.AuthorID))
            .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeID));

        CreateMap<TeamRequest, Team>()
            .ForMember(dest => dest.TeamID, opt => opt.Ignore())
            .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.TeamName))
            .ForMember(dest => dest.TeamDescription, opt => opt.MapFrom(src => src.TeamDescription))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

    }

    private byte[] ConvertToByteArray(string content)
    {
        return Encoding.UTF8.GetBytes(content);
    }
}
