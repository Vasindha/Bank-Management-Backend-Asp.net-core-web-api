using BANK_MANAGEMENT.Managers;
using BANK_MANAGEMENT.Models;
using BANK_MANAGEMENT.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
//jwt configuration 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        
    };
});

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

//for dynamic dependancy injeciton
builder.Services.AddTransient<ICustomerManager, CustomerManager>();
builder.Services.AddTransient<IAccountManager, AccountManager>();
builder.Services.AddTransient<ITransactionManager, TransactionManager>();
builder.Services.AddTransient<IAccountType, SavingAccount>();
builder.Services.AddTransient<IAccountType, CurrentAccount>();
builder.Services.AddTransient<IAuthManager, AuthManager>();

//database connection 
builder.Services.AddDbContext<BankContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("dbconection")));
var app = builder.Build();

//cors policy configuration 
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.Run();
