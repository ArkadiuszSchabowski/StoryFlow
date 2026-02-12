using Microsoft.EntityFrameworkCore;
using NLog.Web;
using StoryFlow.Aggregates;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Middleware;
using StoryFlow.Repositories;
using StoryFlow.Services;
using StoryFlow.Validators;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnectionString")));

builder.Services.AddScoped<IService, StoryService>();
builder.Services.AddScoped<IAdd<AddStoryDto>, StoryService>();
builder.Services.AddScoped<IGet<GetStoryDto>, StoryService>();
builder.Services.AddScoped<IRemove, StoryService>();
builder.Services.AddScoped<IRepository<Story>, StoryRepository>();
builder.Services.AddScoped<ITextConverter, TextConverter>();
builder.Services.AddScoped<ITextCounter, TextCounter>();
builder.Services.AddScoped<IAggregateServiceValidator, AggregateServiceValidator>();
builder.Services.AddScoped<IValidator<AddStoryDto>, StoryValidator>();
builder.Services.AddScoped<IValidatorId, ValidatorId>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
