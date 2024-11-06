
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatikaWeek14Homework2.Data.Context;
using PatikaWeek14Homework2.Data.Repositories;
using PatikaWeek14Homework2.Data.UnitOfWork;
using PatikaWeek14Homework2.DataProtection;
using PatikaWeek14Homework2.User;
using System.Text;

namespace PatikaWeek14Homework2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwaggerGen(options =>
            { //Swagger �zerinden key girilebilecek ve tokenin kullan�laca�� yap� olu�turmak i�in
                var JwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    Name = "Jwt Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer Token on Texbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme,
                    }

                };

                options.AddSecurityDefinition(JwtSecurityScheme.Reference.Id, JwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {JwtSecurityScheme,Array.Empty<string>()},
                });
            }


         );



            // Add services to the container.
            builder.Services.AddScoped<IDataProtection, DataProtection.DataProtection>();

            var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));

            builder.Services.AddDataProtection()
                    .SetApplicationName("CityAddressRegistrySystem")//ba�ka bir yere ta��nd���nda �ifreler a��lmas� i�in eklendi.
                    .PersistKeysToFileSystem(keysDirectory);//ba�ka bir yerde a��lmas� i�in



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(option =>
                   {
                       option.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidIssuer = builder.Configuration["Jwt:Issuer"],

                           ValidateAudience = true,
                           ValidAudience = builder.Configuration["Jwt:Audience"],

                           ValidateLifetime = true,

                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))

                       };


                   }
                   );

            var connectionString = builder.Configuration.GetConnectionString("default");
            builder.Services.AddDbContext<JwtDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//Generic oldu�u i�in
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserManager>();

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
        }
    }
}
