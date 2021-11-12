using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLayerBestPractice.API.DTOs;
using NLayerBestPractice.API.Filters;
using NLayerBestPractice.Core.Repositories;
using NLayerBestPractice.Core.Services;
using NLayerBestPractice.Core.UnitOfWorks;
using NLayerBestPractice.Data;
using NLayerBestPractice.Data.Repositories;
using NLayerBestPractice.Data.UnitOfWorks;
using NLayerBestPractice.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLayerBestPractice.API.Extensions;

namespace NLayerBestPractice.API
{
    public class Startup
    {
        //ornegin bir yerde startup gorunce buradan nesne uretecek snrada ıconfiguraton ustunde nesne uretiyor bu default var 
        //ama bizim yazdıgımız seyler icin buraya nesneyi nerden uretceginiz vermemiz laızm
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //options olarak db yi veriyoruz burada sql server verilecek
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
                {
                    o.MigrationsAssembly("NLayerBestPractice.Data");
                });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<NotFoundFilter>(); //contructorda nesne aldıgında burada tanımlanmalı diger filtrede olmadıgında grek  yok
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); //byle bir irepo karsılasnca repositry den nesne uret
            services.AddScoped(typeof(IService<>), typeof(Service.Services.Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            //Addscoped bir request esnasandabir contruction da karsılasırsa nene uretcek snra o nesne ustudnen devam etcek ama bunn yerine AddTransient dersen her seferince yeni nesne uretir
            //1 request isnasında birden fazla ihtiyac olursa aynı nesne orneginden devam edecektir transient aynı requestte bile brden fazla nesne uretir performansı
            //scoped daha iyi
            services.AddScoped<IUnitOfWork, UnitOfWork>();  //IUnitOfWork interface ile karslasırsan UnitOfWork buraya git bundan nesne olustur

            //services.AddControllers(); eskiden buydu validationfilter butun controllerlarda etki etsin diye allttaki olrak degistiirldi
            services.AddControllers(o =>
            {
                o.Filters.Add(new ValidationFilter());
            });//global duzeyde butun controllerlara eklemek merkezilestirildi

            //validasyonlara sen karısma ben custom yapıcam
            //bunu yorum yap apiden direk hata mesajları donuyor kendisinin
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;  //filterları kontrol etme
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseCustomException();
            //ilk metodu buraya yazıcaz sonra baska yere tasınıp ust satırdan cekecegiz
            //bunu ya attaki gibi yapcagız yada updatedto olustuup id alanı eksiktir geri donecegiz alttaki gibi dnennide loglamak lazım
            //begin:metod
            /*app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;   //bu hatalar serverda baslayacagndan
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var ex = error.Error;   //bu geriye ezception donuyor istedigimiz seyi yakaladık

                        ErrorDto errorDto = new ErrorDto();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(ex.Message);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));  //jsona donustur ve datayı dndr
                        //normalde geri dnuslerde otoamtk yapıyor .net ama burada writeasync metodundan dolayı bizkendimiz yapmalıyız
                    }
                });
            });*/
            //end:metod
            //yukardkai blok yerine extension metod yazıldı app. diyince cıkıyor method zten buna exten metod deniyor
            app.UseCustomException(); //blok yerine tek satia indiridk

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
