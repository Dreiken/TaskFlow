using Microsoft.EntityFrameworkCore;
using TaskFlow.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlite("Data Source=taskflow.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Add Swagger services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();