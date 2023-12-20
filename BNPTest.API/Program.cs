using BNPTest.Infrastructure.Repositories;
using BNPTest.Logic.Interfaces.Repositories;
using BNPTest.Logic.Interfaces.Services;
using BNPTest.Logic.Models;

namespace BNPTest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<ISecurityService, SecurityService>();
            builder.Services.AddScoped<ISecurityRepository, SecurityRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
