using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Application {

    public class Program {

        public static void Main(String[] args) {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(String[] args) => 
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(logging => {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .UseStartup<StartUp>(); 
    }
}
