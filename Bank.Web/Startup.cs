using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using Bank.Data;
using Bank.Search;
using Bank.Web.Services.Classes;
using Bank.Web.Services.Interfaces;
using Bank.Web.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Bank.Web
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

            

            services.AddDbContext<BankAppDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
             services.AddIdentity<BankUser, IdentityRole>(options => options.User.RequireUniqueEmail = true)
               .AddEntityFrameworkStores<BankAppDataContext>()
               .AddDefaultTokenProviders();

             services.AddResponseCaching();

             services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));

            services.Configure<SearchSettings>(Configuration.GetSection("SearchSettings"));
            services.AddOptions();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.RequireHttpsMetadata = false;
                     options.SaveToken = true;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = Configuration["Jwt: Issuer"],
                         ValidAudience = Configuration["Jwt: Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt: SecretKey"])),
                         ClockSkew = TimeSpan.Zero
                     };
                 });

            services.AddSwaggerGen();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IInsertService, InsertService>();
            services.AddTransient<IWithdrawService, WithdrawService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<ISearchService, SearchService>();

            //services.AddTransient<ISearchCustomers, SearchCustomers>();
            //services.AddTransient<IManageSearchData, ManageSearchData>();
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
            }

            app.UseSwagger();

            app.UseResponseCaching();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                
            });
        }
    }
}
