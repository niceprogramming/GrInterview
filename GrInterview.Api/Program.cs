using GrInterview.Api.Db;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;
using GrInterview.Common.Parsers;

namespace GrInterview.Api
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IParser<User>>(new UserParser(new[] { ",", "|", " " }));
            builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
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