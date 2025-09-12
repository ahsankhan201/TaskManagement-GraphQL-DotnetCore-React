
using Microsoft.EntityFrameworkCore;
using GraphQL.Data;
using GraphQL.GraphQL.Queries;
using GraphQL.GraphQL.Mutations;
using GraphQL.Services;

namespace GraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Entity Framework
            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services
            builder.Services.AddScoped<ITaskService, TaskService>();

            // Add GraphQL
            builder.Services
                .AddGraphQLServer()
                .AddQueryType<TaskQueries>()
                .AddMutationType<TaskMutations>();

            // Add CORS services and configure policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            // Add health checks
            builder.Services.AddHealthChecks()
                .AddDbContextCheck<TaskDbContext>();

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Initialize database
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                
                try
                {
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    // If table already exists, continue - the app will still work
                    Console.WriteLine($"Database initialization note: {ex.Message}");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // Use CORS policy BEFORE MapGraphQL
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Add health check endpoint
            app.MapHealthChecks("/health");

            // Map GraphQL endpoint
            app.MapGraphQL("/graphql");

            app.MapControllers();

            app.Run();
        }
    }
}
