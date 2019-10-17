using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgramPlanning.Shared.Services;

namespace ProgramPlanning.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            services.AddTransient<IFileService, FileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTUwMTIzQDMxMzcyZTMzMmUzMElhUDNrSmV0MzVWRDFDMk5VYnB1QVNoRHlYOHF6dEk1OXJYcFNkUUFydG89;MTUwMTI0QDMxMzcyZTMzMmUzMEl0OWI3cVI1Sm11WlEyZDdGN3lyYmh6VlNaTndKN3BBTlZFUlhkSUVCTG89;MTUwMTI1QDMxMzcyZTMzMmUzME5BbjFGT3p2SlZmSStzb2JqRk9uMHNBSTNpMXdoaHVrMXlvc2lYbmhTYkE9;MTUwMTI2QDMxMzcyZTMzMmUzMElCNWhnQVFXOVNpb0h4aC8wa0NnVmdDM2l1N29xQU93SU9Tck5OdGcrcHM9;MTUwMTI3QDMxMzcyZTMzMmUzMFgrZDU1Nk5PQ052L0xRTHo1dkxqN1I3cFgwV2ZieXR1YlNMRUFDTzZTSmc9;MTUwMTI4QDMxMzcyZTMzMmUzMGs2TDdXQU56U05UWGI3bVQrWU5RRWxMYUtDZ296amN3d1VVOEpGTkQ1dTA9;MTUwMTI5QDMxMzcyZTMzMmUzMFpZUXhmbmpHdUlCalZuTnFTRml0VVlxU1JORE9xNmYyZytycnpjaXVndW89;MTUwMTMwQDMxMzcyZTMzMmUzMGgrMm14eGNYUVJCampTTFFLbXRCOWdvbDNzYWowTS9UemRMSDBGbWJzVG89;MTUwMTMxQDMxMzcyZTMzMmUzMGM3a1VrUy9nclZsa2dKdTY3MjQ0OTVTQlRDS0I1Ym0raGVTQ3ZPbmZ1VDg9;MTUwMTMyQDMxMzcyZTMzMmUzMFgrZDU1Nk5PQ052L0xRTHo1dkxqN1I3cFgwV2ZieXR1YlNMRUFDTzZTSmc9");
        }
    }
}
