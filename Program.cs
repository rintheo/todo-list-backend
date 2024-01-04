using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using todo_list_backend.Data;
using todo_list_backend.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionStringBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("TodoTasksDBConnectionString"));
connectionStringBuilder.UserID = builder.Configuration["DbUserId"];
connectionStringBuilder.Password = builder.Configuration["DbPassword"];
var connection = connectionStringBuilder.ConnectionString;

builder.Services.AddDbContext<TodoTasksDbContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");

    if (app.Environment.IsDevelopment())
        options.RoutePrefix = "swagger";
    else
        options.RoutePrefix = string.Empty;
}
);

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();
app.MapControllers();
app.Run();

