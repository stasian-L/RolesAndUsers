using Microsoft.EntityFrameworkCore;
using RolesAndUsers.Data;

using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using System.Text.Json.Serialization;
using RolesAndUsers.Repositories;
using RolesAndUsers.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>policy.WithOrigins("*"))
);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

//For cofiguring AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder
              .AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed((host) => true)
              .AllowCredentials()
              );

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
