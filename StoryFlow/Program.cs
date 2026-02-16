using Microsoft.EntityFrameworkCore;
using NLog.Web;
using StoryFlow.Aggregates;
using StoryFlow.Builders;
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

builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<ISentenceService, SentenceService>();
builder.Services.AddScoped<IAdd<AddStoryDto>, StoryService>();
builder.Services.AddScoped<IGet<GetStoryDto>, StoryService>();
builder.Services.AddScoped<IRemove, StoryService>();
builder.Services.AddScoped<IRepository<Story>, StoryRepository>();
builder.Services.AddScoped<ITextConverter, TextConverter>();
builder.Services.AddScoped<ITextCounter, TextCounter>();
builder.Services.AddScoped<IAggregateServiceValidator, AggregateServiceValidator>();
builder.Services.AddScoped<IValidator<AddStoryDto>, StoryValidator>();
builder.Services.AddScoped<IValidatorId, ValidatorId>();
builder.Services.AddScoped<IEntityValidator<Story>, StoryEntityValidator>();
builder.Services.AddScoped<ISentenceBuilder, SentenceBuilder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("StoryFlowPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors("StoryFlowPolicy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
