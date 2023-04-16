using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Infra.CrossCuting.Bus;
using Eventos.IO.Infra.CrossCuting.IoC;
using Eventos.IO.Infra.CrossCutting.AspNetFilters;
using Eventos.IO.Site.Data;
using Eventos.IO.Site.Models;
using Eventos.IO.Site.Services;
using Microsoft.AspNetCore.Mvc;

namespace Eventos.IO.Site;

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
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
        //services.AddDefaultIdentity<ApplicationUser>()
        //    .AddRoles<IdentityRole>()
        //    .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddMvc(options =>
        {
            options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalExceptionHandlingFilter)));

            options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
        });
        
        // Add application services.
        //services.AddTransient<IEmailSender, EmailSender>();
        //services.AddScoped<IUser, AspNetUser>();

         //RegisterServices(services);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();
        }
        else
        {
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
        InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
    }

    private static void RegisterServices(IServiceCollection services)
    {
        NativeInjectorBootStrapper.RegisterServices(services);
    }
}