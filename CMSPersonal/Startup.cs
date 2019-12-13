using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSPersonal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CMSPersonal
{
    public class Startup
    {
        private readonly IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            // ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            

            services.AddDbContext<AppIdentityDbContext>(c => c.UseSqlServer(
                Configuration.GetConnectionString("IdentityConnection")));

            

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            ConfigureServices(services);
        }


        public void ConfigureServices(IServiceCollection services)
        {



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddEntityFrameworkSqlServer();
            services.AddSession();

            AddResponseCompression(services);
            services.AddMvc()
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
             })            
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            /*Identity Login Url */
            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Login");

        }
            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
                
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                /*Menu*/
                routes.MapRoute(
                name: "PagingPageOne-menu",
                template: "Menu",
                defaults: new { controller = "Menu", action = "Index", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-menu",
                template: "Menu/{id:int?}",
                defaults: new { controller = "Menu", action = "Index" });
                /*End*/

                /*Page*/
                routes.MapRoute(
                name: "PagingPageOne-page",
                template: "Page",
                defaults: new { controller = "Page", action = "Index", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-page",
                template: "Page/{id:int?}",
                defaults: new { controller = "Page", action = "Index" });
                /*End*/

                /*MyBlogCategory*/
                routes.MapRoute(
                name: "PagingPageOne-myblogcategory",
                template: "MyBlogCategory/{url}",
                defaults: new { controller = "Home", action = "MyBlogCategory", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-myblogcategory",
                template: "MyBlogCategory/{url}/{id:int?}",
                defaults: new { controller = "Home", action = "MyBlogCategory" });
                /*End*/

                /*MyBlog*/
                routes.MapRoute(
                name: "PagingPageOne-myblog",
                template: "MyBlog",
                defaults: new { controller = "Home", action = "MyBlog", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-myblog",
                template: "MyBlog/{id:int?}",
                defaults: new { controller = "Home", action = "MyBlog" });
                /*End*/

                /*Blog*/
                routes.MapRoute(
                name: "PagingPageOne-blog",
                template: "Blog",
                defaults: new { controller = "Blog", action = "Index", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-blog",
                template: "Blog/{id:int?}",
                defaults: new { controller = "Blog", action = "Index" });
                /*End*/

                /*Blog Category*/
                routes.MapRoute(
                name: "PagingPageOne-blogcategory",
                template: "BlogCategory",
                defaults: new { controller = "BlogCategory", action = "Index", id = 1 }
                );

                routes.MapRoute(
                name: "Paging-blogcategory",
                template: "BlogCategory/{id:int?}",
                defaults: new { controller = "BlogCategory", action = "Index" });
                /*End*/

                /*View Blog*/
                routes.MapRoute(
                name: "View-Blog",
                template: "{url}-id-{id}",
                defaults: new { controller = "Home", action = "ViewBlog" });
                /*End*/

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                /*View Page*/
                routes.MapRoute(
                name: "View-Page",
                template: "{url}",
                defaults: new { controller = "Home", action = "Page" });
                /*End*/
            });
        }

        private static void AddResponseCompression(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "imagejpeg" });
            });

            services.Configure<GzipCompressionProviderOptions>
            (options => options.Level =
                System.IO.Compression.CompressionLevel.Optimal);
        }
    }
}
