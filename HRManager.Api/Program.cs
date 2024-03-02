using AutoMapper;
using HRManager.Data;
using HRManager.Data.Repositories.Interfaces;
using HRManager.Data.Repositories.Repositories;
using HRManager.Data.UnitOfWork;
using HRManager.Services.Interfaces;
using HRManager.Services.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

builder.Services.AddDbContext<HRManagerDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAbsenceService, AbsenceService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ITeamService, TeamService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapControllers();

app.Run();
