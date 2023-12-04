using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Application {

    public class StartUp {

        public StartUp(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services) {
            
        }

        public void Configure (IApplicationBuilder applicationBuilder, IWebHostEnvironment hostEnvironment) {

        }

        public void ConfigureMyServices(IServiceCollection services) {

        }

    }
}