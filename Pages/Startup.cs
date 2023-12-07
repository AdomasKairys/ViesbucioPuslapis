//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using ViesbucioPuslapis.Data;

//namespace ViesbucioPuslapis
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        public void ConfigureServices(IServiceCollection services)
//        {

//            services.AddRazorPages();

//            services.AddDbContext<ViesbutisDbContext>(options =>
//              options.UseSqlServer(Configuration.GetConnectionString("Connection")));


//            //services.AddDbContext<ApplicationDbContext>(options =>
//            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//            //services.AddIdentity<IdentityUser, IdentityRole>()
//            //    .AddEntityFrameworkStores<ApplicationDbContext>()
//            //    .AddDefaultTokenProviders();

//            //services.Configure<IdentityOptions>(options =>
//            //{
//            //    options.Password.RequireDigit = false;
//            //    options.Password.RequireLowercase = false;
//            //    options.Password.RequireNonAlphanumeric = false;
//            //    options.Password.RequireUppercase = false;
//            //    options.Password.RequiredLength = 6;
//            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
//            //    options.Lockout.MaxFailedAccessAttempts = 5;
//            //    options.Lockout.AllowedForNewUsers = true;

//            //    // Išjunkite reikalavimą patvirtinti el. paštą, jei norite hardcode'intų prisijungimo duomenų.
//            //    options.SignIn.RequireConfirmedEmail = false;
//            //});

//            //services.ConfigureApplicationCookie(options =>
//            //{
//            //    options.Cookie.HttpOnly = true;
//            //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
//            //    options.LoginPath = "/Login/Index"; // Nurodomas prisijungimo puslapio kelias
//            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
//            //    options.SlidingExpiration = true;
//            //});

//            //// Čia pridedame metodą, kuris sukuria vartotoją su hardcode'intais duomenimis.
//            //services.AddTransient<SeedData>();

//            //services.AddControllersWithViews();
//            //services.AddRazorPages();
//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapRazorPages();
//            });
//        }

//        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedData seedData)
//        //{
//        //    if (env.IsDevelopment())
//        //    {
//        //        app.UseDeveloperExceptionPage();
//        //        //app.UseDatabaseErrorPage();
//        //    }
//        //    else
//        //    {
//        //        app.UseExceptionHandler("/Home/Error");
//        //        app.UseHsts();
//        //    }

//        //    app.UseHttpsRedirection();
//        //    app.UseStaticFiles();
//        //    app.UseRouting();
//        //    app.UseAuthentication();
//        //    app.UseAuthorization();

//        //    app.UseEndpoints(endpoints =>
//        //    {
//        //        endpoints.MapControllerRoute(
//        //            name: "default",
//        //            pattern: "{controller=Home}/{action=Index}/{id?}");
//        //        endpoints.MapRazorPages();
//        //    });

//        //    // Čia pritaikome hardcode'intus duomenis
//        //    seedData.Initialize().GetAwaiter().GetResult();
//        //}
//    }
//}
