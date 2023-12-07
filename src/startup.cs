using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using RobDroneGo.Infrastructure;
using DDDSample1.Domain.Shared;
using RobDroneGoAuth.Infrastructure;
using RobDroneGoAuth.Domain.Users;
using RobDroneGoAuth.Infrastructure.Users;

namespace Application
{

    public class StartUp
    {

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RobDroneGoAuthContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                 ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserService, UserService>();

            services.AddEndpointsApiExplorer();
            services.AddControllers().AddNewtonsoftJson();
        }

        public void Configure(IApplicationBuilder applicationBuilder, IWebHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                applicationBuilder.UseHsts();
            }

            applicationBuilder.UseHttpsRedirection();
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthorization();
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}