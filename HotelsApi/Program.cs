using FluentValidation;
using HotelsApi.Context;
using HotelsApi.Dtos;
using HotelsApi.Repositories;
using HotelsApi.Services;
using HotelsApi.Validator;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static HotelsApi.Validator.CreateStateValidator;
using static HotelsApi.Validator.CreateTransactionValidator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Services
builder.Services.AddScoped<IHotelService, HotelServices>();
builder.Services.AddScoped<IValidator<CreateHotel>, CreateHotelValidator>();
builder.Services.AddScoped<IValidator<UpdateHotel>, UpdateHotelValidator>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IValidator<CreateState>, CreateStateValidator>();
builder.Services.AddScoped<IValidator<UpdateState>, UpdateStateValidator>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IValidator<CreateCity>, CreateCityValidator>();
builder.Services.AddScoped<IValidator<UpdateCity>, UpdateCityValidator>();
builder.Services.AddScoped<IBarangayService, BarangayService>();
builder.Services.AddScoped<IValidator<CreateBarangay>, CreateBarangayValidator>();
builder.Services.AddScoped<IValidator<UpdateBarangay>, UpdateBarangayValidator>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IValidator<CreateCountry>, CreateCountryValidator>();
builder.Services.AddScoped<IValidator<UpdateCountry>, UpdateCountryValidator>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IValidator<CreateTransaction>, CreateTransactionValidator>();
builder.Services.AddScoped<IValidator<UpdateTransaction>, UpdateTransactionValidator>();

// Repositories
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IBarangayRepository, BarangayRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

// Database Configuration
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
string connectionString = "server=localhost; port=3306; database=Hotel-Api; user=root; password=Kahitano; SslMode=Required;Allow User Variables=true;";

builder.Services.AddDbContext<DatabaseContext>(
            dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion).EnableDetailedErrors()
        );

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
