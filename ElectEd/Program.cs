using Microsoft.EntityFrameworkCore;
using ElectEd;
using ElectEd.Services.Candidate;
using ElectEd.Services.Election;
using ElectEd.Services.Position;
using ElectEd.Services.VoteSlip;
using ElectEd.Services.Student; // Add this namespace to use ApplicationDbContext


using ElectEd.Repositories.Candidate;
using ElectEd.Repositories.Election;
using ElectEd.Repositories.Position;
using ElectEd.Repositories.VoteSlip;
using ElectEd.Repositories.Student;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IElectionInfoService, ElectionInfoService>();
builder.Services.AddTransient<IPositionInfoService,PositionInfoService >();
builder.Services.AddTransient<IVoteSlipInfoService, VoteSlipInfoService>();
builder.Services.AddTransient<IStudentInfoService,StudentInfoService>();
builder.Services.AddTransient<ICandidateInfoService, CandidateInfoService>();






builder.Services.AddScoped<ICandidateInfoRepository, CandidateInfoRepository>();
builder.Services.AddScoped<IElectionInfoRepository, ElectionInfoRepository>();
builder.Services.AddScoped<IPositionInfoRepository, PositionInfoRepository>();
builder.Services.AddScoped<IVoteSlipInfoRepository, VoteSlipInfoRepository>();
builder.Services.AddScoped<IStudentInfoRepository, StudentInfoRepository>();












builder.Services.AddControllers();

// Register the ApplicationDbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger/OpenAPI configuration
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

app.MapControllers();

app.Run();
