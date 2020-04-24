using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Data;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MiniFacebook.Models.Entities;
using MiniFacebook.Models.RepoInterface;
using MiniFacebook.Models.RepoClass;
using MiniFacebook.Extension;

namespace MiniFacebook
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddIdentity<User, Role>(options=> {
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();
            services.AddScoped<IPostRepo, PostRepo>();
            services.AddScoped<ICommentRepo, CommentRepo>();
            services.AddScoped<ICommentLikeRepo, CommentLikeRepo>();
            services.AddScoped<IPostLikeRepo, PostLikeRepo>();
            services.AddScoped<IFriendRepo, FriendRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddSession(options => { });
            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "582746702048-1kftodbtd22ah60jg3gaog4f1oaj3nme.apps.googleusercontent.com";
                    options.ClientSecret = "PRALaGagZ6bbtNSqJHUI6iyg";
                })
            .AddFacebook(options =>
             {
                 options.ClientId = "213078056693180";
                 options.ClientSecret = "9eec6c17f3b9104ea35e22572dcb1259";
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            IdentityDataInitializer.seed(userManager, roleManager);

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
