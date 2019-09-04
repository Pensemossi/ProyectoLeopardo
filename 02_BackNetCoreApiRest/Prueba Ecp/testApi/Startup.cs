
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using testEcpApi.Models;
using testEcpApi.Data;


namespace testEcpApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configuración de la BD donde se persistirán los datos
        /// XX= 1 --> InMemory   X=0 --> Base datos según configuración cnn_test
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            string strInMemory = "";
            // variable que indique si se sube un MOCK de la Base de datos. Trabajo en memoria
            // para independizar pruebas de la persistencia de la Base de datos.
            strInMemory = Configuration.GetValue<string>("BD_InMemory");

            services.AddCors(o => o.AddPolicy("Cors Politica", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            if (strInMemory == "Y")
            {
                // En Memoria
                services.AddDbContext<testContext>(opt =>
                 opt.UseInMemoryDatabase("BD_Memoria_Mock"));
            }
            else
            {
                // En BD
                services.AddDbContext<testContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("cnn_test")));
            }



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors("Cors Politica");
        }
    }
}
