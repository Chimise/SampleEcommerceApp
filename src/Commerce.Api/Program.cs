using Microsoft.EntityFrameworkCore.SqlServer;
using Commerce.SqlDataAccess;
using Commerce.SqlDataAccess.Aspects;
using Microsoft.EntityFrameworkCore;
using Commerce.Domain.ProductInventories.Commands;
using Commerce.Domain.Products.Repositories;
using Commerce.Domain.Common;
using Commerce.Domain.ProductInventories.Repositories;
using Commerce.Domain.ProductInventories.Events;
using SimpleInjector;
using System.Reflection;
using System.Runtime.CompilerServices;
using Commerce.Domain;
using Commerce.Domain.Users;
using Commerce.Api.Authentication;
using Commerce.ExternalConnections;


namespace Commerce.Api
{
    public class Program
    {
        private static Container _container { get; set; } = new Container();
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
            var container = _container;
            
            builder.Services.AddSimpleInjector(container, options =>
            {
                // AddAspNetCore() wraps web requests in a Simple Injector scope and
                // allows request-scoped framework services to be resolved.
                options.AddAspNetCore()

                    // Ensure activation of a specific framework type to be created by
                    // Simple Injector instead of the built-in configuration system.
                    .AddControllerActivation();

                // Optionally, allow application components to depend on the non-generic
                // ILogger (Microsoft.Extensions.Logging) or IStringLocalizer
                // (Microsoft.Extensions.Localization) abstractions.
                options.AddLogging();
            });


            var domainAssembly = typeof(AdjustInventoryCommandHandler).Assembly;
            container.Register<ITimeProvider, DefaultTimeProvider>(Lifestyle.Singleton);
            container.Register<IUserContext, AspNetUserContextAdapter>(Lifestyle.Scoped);

            container.Register(typeof(ICommandHandler<>), domainAssembly);

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(AuditingCommandServiceDecorator<>));

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionCommandServiceDecorator<>));

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(SaveChangesCommandServiceDecorator<>));

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(SecureCommandServiceDecorator<>));

            container.Register(() =>
            {
                var optionBuilder = new DbContextOptionsBuilder<CommerceContext>()
                .UseSqlServer(connStr);
                return new CommerceContext(optionBuilder.Options);
            }, Lifestyle.Scoped);

            container.Collection.Register(typeof(IEventHandler<>), domainAssembly);
            container.Register(typeof(IEventHandler<>), typeof(CompositeEventHandler<>));

            RegisterAsImplementedInterfaces(typeof(SqlInventoryRepository).Assembly, type => type.Name.EndsWith("Repository"));
            RegisterAsImplementedInterfaces(typeof(WCFBillingSystem).Assembly, type => true);
            var app = builder.Build();

            // UseSimpleInjector() finalizes the integration process.
            app.Services.UseSimpleInjector(container);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); 

            app.UseAuthorization();


            app.MapControllers();

            container.Verify();

            app.Run();
        }

        private static void RegisterAsImplementedInterfaces(Assembly asm, Func<Type, bool> predicate) =>
            RegisterAsImplementedInterfaces(asm.ExportedTypes.Where(predicate));

        private static void RegisterAsImplementedInterfaces(IEnumerable<Type> implementationTypes)
        {
            foreach (Type type in implementationTypes)
            {
                foreach (Type service in type.GetInterfaces())
                {
                    _container.Register(service, type);
                }
            }
        }
    }
}
