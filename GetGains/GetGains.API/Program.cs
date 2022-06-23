using GetGains.Data.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<GainsDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "WorkoutDatabase"));

builder.Services.AddDbContext<GainsDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration.GetConnectionString("GetGainsDBContext")));

builder.Services.AddScoped<IExerciseData, ExerciseData>();
builder.Services.AddScoped<IWorkoutData, WorkoutData>();
builder.Services.AddScoped<ITemplateData, TemplateData>();

builder.Services.AddCors();

builder.Services.AddControllers();

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

app.UseRouting();

app.UseCors(builder =>
    builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
