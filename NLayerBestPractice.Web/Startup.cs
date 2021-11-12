using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.EntityFrameworkCore;  //TODO BUNUN KALDIRILCAGINDN EMN DEGLIM
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLayerBestPractice.Web.Filters;
//using NLayerBestPractice.Core.Repositories;
//using NLayerBestPractice.Core.Services;
//using NLayerBestPractice.Core.UnitOfWorks;
//using NLayerBestPractice.Data;
//using NLayerBestPractice.Data.Repositories;
//using NLayerBestPractice.Data.UnitOfWorks;
//using NLayerBestPractice.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLayerBestPractice.Web.ApiService;

namespace NLayerBestPractice.Web
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
            services.AddControllersWithViews();

            services.AddHttpClient<CategoryApiService>(opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            });

            services.AddScoped<CategoryNotFoundFilter>();
            //addscoped  interface ile karıslasınca ona karsılık gelen implementasyondan uret aynı yerde brden fazla karsılasınca tekrr uretme ondan dvm et
            //addtransient her kersılasınca yeni uretir, singleton 1 sefer olusturur

            //GUNCELLEME API ILE HABERLESECEGIMZDEN ALT SATIRDAKI YORUMLARA ARTIK IHTIYAC YOK
            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //byle bir irepo karsılasnca repositry den nesne uret
            //services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            //services.AddScoped<ICategoryService, CategoryService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(Startup));

            /*services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("NLayerBestPractice.Data");
                });
            });*/
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
                app.UseExceptionHandler("/Home/Error");
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
