using Microsoft.EntityFrameworkCore.SqlServer;
using Commerce.SqlDataAccess;
using Microsoft.EntityFrameworkCore;
using Commerce.Domain.ProductInventories.Commands;
using Commerce.Domain.Products.Repositories;
using Commerce.Domain.Common;


namespace Commerce.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connStr = builder.Configuration.GetConnectionString("CommerceConn");
            builder.Services.AddDbContext<CommerceContext>(op => op.UseSqlServer(connStr));
            builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
            builder.Services.AddScoped<ICommandHandler<AdjustInventoryCommand>, AdjustInventoryCommandHandler>();

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
