using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using StoryFlow;
using StoryFlow.Aggregates;
using StoryFlow.Builders;
using StoryFlow.Helpers;
using StoryFlow.Interfaces;
using StoryFlow.Interfaces.Aggregates;
using StoryFlow.Middleware;
using StoryFlow.Repositories;
using StoryFlow.Services;
using StoryFlow.Validators;
using StoryFlow_Database;
using StoryFlow_Database.Entities;
using StoryFlow_Shared.Interfaces;
using StoryFlow_Shared.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnectionString")));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IAggregateStoryValidator, AggregateStoryValidator>();
builder.Services.AddScoped<IAggregateStoryRepository, AggregateStoryRepository>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IRepository<Story>, StoryRepository>();
builder.Services.AddScoped<IGetStoryRepository, StoryRepository>();
builder.Services.AddScoped<IAdd<AddStoryDto>, StoryService>();
builder.Services.AddScoped<IGetStory, StoryService>();

builder.Services.AddScoped<IValidator<AddStoryDto>, StoryValidator>();
builder.Services.AddScoped<IEntityValidator<Story>, StoryEntityValidator>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAggregateUserValidator, AggregateUserValidator>();
builder.Services.AddScoped<IAggregateUserRepository, AggregateUserRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IEntityValidator<User>, UserEntityValidator>();
builder.Services.AddScoped<IGetAllRepository<User>, UserRepository>();
builder.Services.AddScoped<IGetByEmailRepository, UserRepository>();
builder.Services.AddScoped<IUserValidator, UserValidator>();

builder.Services.AddScoped<IValidatorId, ValidatorId>();
builder.Services.AddScoped<ISentenceBuilder, SentenceBuilder>();
builder.Services.AddScoped<ITextConverter, TextConverter>();
builder.Services.AddScoped<ITextCounter, TextCounter>();
builder.Services.AddScoped<IPointsCalculator, PointsCalculator>();
builder.Services.AddScoped<GeminiSchemaGenerator>();

builder.Services.AddScoped<HttpClient>();

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

builder.Services.Configure<GeminiSettings>(
    builder.Configuration.GetSection("GeminiSettings"));

var authenticationSettings = new AuthenticationSettings();

builder.Services.AddSingleton(authenticationSettings);
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

var app = builder.Build();

//app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("StoryFlowPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryFlow API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
