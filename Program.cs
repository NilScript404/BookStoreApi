using Microsoft.EntityFrameworkCore;
using BookStore.Data;
using System.Text.Json.Serialization;
using BookStore.BookService;
using BookStore.BookRepositoryService;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });


/* todo , could have also chained .AddDefaultTokenProviders() , for managing stuff like
password reset , sign up notificiation , etc... 

builder.Services.AddIdentity<User, Role>(
    .AddEntityFrameworkStores<BookStoreDbContext>();
    .AddDefaultTokenProviders()
*/

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BookStoreDbContext>()
    .AddDefaultTokenProviders();    

/* todo 
builder.Services.AddIdentity<User , Role>( options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    }
);
*/

builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IBookRepository , BookRepository>();

builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    await SeedRole(rolemanager);
}


// Configure the HTTP request pipeline.
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