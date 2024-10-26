using CursosWebApi.Infrastructure;
using CursosWebApi.Interfaces;
using CursosWebApi.Respositories;
using CursosWebApi.Services;
using CursosWebApi.Util;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<CursosDbContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddScoped<IAlunoService,AlunoService>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();

builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

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
