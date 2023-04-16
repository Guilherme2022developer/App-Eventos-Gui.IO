using Eventos.Api.Configurations;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Infra.CrossCuting.Bus;
using Eventos.IO.Infra.CrossCuting.IoC;
using Eventos.IO.Site.Data;
using Eventos.IO.Site.Models;
using Eventos.IO.Site.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Eventos.Api;
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("DefaultConnection"));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddOptions();
            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
                options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "V1",
                    Title = "Eventos.Io API",
                    Description = "API do site Evntos",
                    Contact = new OpenApiContact() { Name = "Guilherme", Email = "guilhermealves458@gmail.com" }
                });

            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUser, AspNetUser>();

            RegisterServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor accessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("swagger/v1/swagger.json", "Eventos.IO API V1.0");
            });

            app.UseHttpsRedirection();

            InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;

        }


        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }




