using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using RobDroneGo.Infrastructure;
using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Infrastructure;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Services.Users;
using Microsoft.Extensions.Options;
using RobDroneGoAuth.Infrastructure.Users;

namespace Application
{

    public class StartUp
    {

        private readonly ILogger<StartUp> _logger;

        public StartUp(IConfiguration configuration, ILogger<StartUp> logger)
        {
            Configuration = configuration;
            this._logger = logger;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            _logger.LogInformation("StartUp: ConfigureServices");
            _logger.LogInformation("Adding DbContext");

            services.AddDbContext<RobDroneGoAuthContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                 ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));

            _logger.LogInformation("Adding Repositories and Services");

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();

            _logger.LogInformation("Adding Controllers");

            services.AddEndpointsApiExplorer();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAnyOrigin",
                policy => 
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                });
            });



            _logger.LogInformation("StartUp: ConfigureServices has finished\n\n");
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
                applicationBuilder.UseSwagger();
                applicationBuilder.UseSwaggerUI();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                applicationBuilder.UseHsts();
            }

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseCors("AllowAnyOrigin");
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}