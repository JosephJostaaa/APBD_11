using Microsoft.EntityFrameworkCore;
using Task11.Data;
using Task11.Repositories;
using Task11.Repositories.abstr;
using Task11.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<HospitalDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddControllers();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IMedicamentRepository, MedicamentRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
builder.Services.AddScoped<IPrescriptionMedicamentRepository, PrescriptionMedicamentRepository>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPatientService, PatientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.UseHttpsRedirection();
app.MapControllers();
app.Run();
