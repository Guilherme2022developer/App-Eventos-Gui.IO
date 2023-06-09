﻿using Eventos.IO.Domain.INTERFACES;
using Eventos.IO.Infra.CrossCuting.Bus;
using Eventos.IO.Infra.CrossCuting.IoC;
using Eventos.IO.Services.Api.Configurations;
using Eventos.IO.Services.Api.Middlewares;
using Eventos.IO.Services.Api.Models;
using Eventos.IO.Services.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using ApplicationDbContext = Eventos.IO.Services.Api.Data.ApplicationDbContext;
using Microsoft.IdentityModel.Tokens;

namespace Eventos.IO.Services.Api;

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
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        services.AddControllersWithViews();
        services.AddAuthorization(options =>
        {
            options.AddPolicy("PodeLerEventos", policy => policy.RequireClaim("Eventos", "Ler"));
            options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Eventos", "Gravar"));
        });
        services.AddRazorPages();
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
                Description = "API do site Eventos",
                Contact = new OpenApiContact() { Name = "Guilherme", Email = "guilhermealves458@gmail.com" }
            });

        });

        // Add application services.
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped<IUser, AspNetUser>();
        services.AddCors();
        RegisterServices(services);

        byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor accessor)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
           // app.UseDatabaseErrorPage();
        }
        else
        {
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
           // app.UseHsts();
        }
        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

       // app.UserSwaggerAuthorized(); //Remover pra testes

        app.UseSwagger();
        app.UseSwaggerUI();

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

public static class Settings
{
    public static string Secret = "43e4dbf0-52ed-4203-895d-42b586496bd4";


}
