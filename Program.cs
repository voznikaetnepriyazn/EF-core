using ByuerApp.Domain.Entities;
using ByuerApp.Domain.Interfaces;
using ByuerApp.Domain.Servises;
using ByuerApp.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);//обьект для настройки параметров и сервисов приложения, args - аргументы командной строки

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log")
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IGoodService, GoodService>();
builder.Services.AddScoped<ITypesService, TypesService>();

var app = builder.Build();//


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//находится ли приложение в девелопмент среде, системная переменная app?? - на продакшене её не будет
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
