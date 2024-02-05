using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using HizliSec.Business.Abstract;
using HizliSec.Business.DependencyResolvers.AutoFac;
using HizliSec.DataAccess.Abstract;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore;
using HizliSec.DataAccess.Concrete.EntityFrameworkCore.Context;
using HizliSec.Entities.Concrete;
using HizliSec.Entities.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new BusinessModule()));
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICustomerDal, CustomerDal>();
builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddScoped<IOrderDetailDal, OrderDetailDal>();
builder.Services.AddScoped<IProductDal, ProductDal>();
builder.Services.AddScoped<ISellerDal, SellerDal>();
builder.Services.AddScoped<ISellerProductDal, SellerProductDal>();
// AddDbContext
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HizliSecContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddIdentity<AppUser, AppRole>(
      x => x.Password = new PasswordOptions()
      {
          RequiredLength = 0,
          RequiredUniqueChars = 0,
          RequireLowercase = false,
          RequireUppercase = false,
          RequireNonAlphanumeric = false,
          RequireDigit = false
      }
    ).AddEntityFrameworkStores<HizliSecContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("guvenlik", x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors("guvenlik");

app.MapGet("/Categories/{id}", async (int id, [FromServices] ICategoryService categoryService) =>
{
    CategoryDto categoryDto = await categoryService.GetByIdAsync(id);
    return Results.Ok(categoryDto);
});

app.MapGet("/Categories", async ([FromServices] ICategoryService categoryService) =>
{
    List<CategoryDto> categories = await categoryService.GetAllAsync();
    return Results.Ok(categories);
});

app.MapPost("/AddCategories", async (CategoryAddDto categoryAddDto, [FromServices] ICategoryService categoryService) =>
{
    int response = await categoryService.AddAsync(categoryAddDto);
    return Results.Ok(response);
});

app.MapPut("/UpdateCategories", async (CategoryDto categoryDto, [FromServices] ICategoryService categoryService) =>
{
    int response = await categoryService.UpdateAsync(categoryDto);
    return Results.Ok(response);
});

app.MapDelete("/DeleteCategories/{id}", async (int id, [FromServices] ICategoryService categoryService) =>
{
    int response = await categoryService.DeleteAsync(id);
    return Results.Ok(response);
});

app.Run();
