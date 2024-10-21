//
// Todo => if the user isnt logged in , accessing restricted apis will result in error 404
// which should not be the case and they should result in error 401 or 403 unauthorized
//

using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using System.Text.Json.Serialization;
using BookStore.BookService;
using BookStore.BookRepositoryService;
using BookStore.Models;
using BookStore.DtoService;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

builder.Services.AddIdentity<User, Role>(options =>
    {
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(90);
        options.Lockout.MaxFailedAccessAttempts = 5;
        
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    }
).AddEntityFrameworkStores<BookStoreDbContext>()
.AddDefaultTokenProviders();
  
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IBookRepository , BookRepository>();
builder.Services.AddTransient<IDtoService, DtoService>();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await SeedRole(rolemanager);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
// todo c
// seed the db with admin and user 
// then mess around with the authentication system 
async Task SeedRole(RoleManager<Role> rolemanager)
{
    if (!await rolemanager.RoleExistsAsync("User"))
    {
        await rolemanager.CreateAsync(new Role { Name = "User" });
    }
    if (!await rolemanager.RoleExistsAsync("Admin"))
    {
        
        await rolemanager.CreateAsync(new Role { Name = "Admin" });
    }
}