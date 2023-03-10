using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.DBContexts;
using Microsoft.EntityFrameworkCore;
using TestPichincha.Repository;

namespace TestPichincha
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
            services.AddRazorPages();
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 32));
            services.AddDbContextPool<MyDbContext>(options => options.UseMySql(mySqlConnectionStr, serverVersion));
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ICuentaRepository, CuentaRepository>();
            services.AddTransient<IPersonaRepository, PersonaRepository>();
            services.AddTransient <IMovimientoRepository, MovimientoRepository>();
        
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "api/ClienteController",
                    pattern: "api/Cliente"
                );
                endpoints.MapControllerRoute(
                    name: "CuentaController",
                    pattern: "api/Cuenta"
                );
                endpoints.MapControllerRoute(
                    name: "MovimientoController",
                    pattern: "api/Movimiento"
                );
            });
        }
    }
}
