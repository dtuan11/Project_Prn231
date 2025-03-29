using API.Models;
using API.DAO;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using API.Repositories.IRepositories;
using API.DAO.IDAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Session configuration
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// DbContext registration
builder.Services.AddDbContext<PRN221_Project_1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("value")));
builder.Services.AddHttpClient();
// Register DAOs
builder.Services.AddScoped<IBookDao, BookDAO>();
builder.Services.AddScoped<IChapterDao, ChapterDAO>();
builder.Services.AddScoped<UserDAO>();

// Register Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseCors("AllowAll");

app.UseAuthorization();


app.MapControllers();
app.Run();