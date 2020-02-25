using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IpertechCompany.WebAPI
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
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IDbContext, DbContext>(db => new DbContext(Configuration["ConnectionString"]));
            services.AddSingleton(Configuration);

            services.AddScoped<INotificationTypeRepository, NotificationTypeRepository>();
            services.AddScoped<INotificationTypeService, NotificationTypeService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IInternetRouterRepository, InternetRouterRepository>();
            services.AddScoped<IInternetRouterService, InternetRouterService>();

            services.AddScoped<IInternetPacketRepository, InternetPacketRepository>();
            services.AddScoped<IInternetPacketService, InternetPacketService>();

            services.AddScoped<IPhonePacketRepository, PhonePacketRepository>();
            services.AddScoped<IPhonePacketService, PhonePacketService>();

            services.AddScoped<ITvChannelRepository, TvChannelRepository>();
            services.AddScoped<ITvChannelService, TvChannelService>();

            services.AddScoped<ITvPacketRepository, TvPacketRepository>();
            services.AddScoped<ITvPacketService, TvPacketService>();

            services.AddScoped<ITvPacketTvChannelRepository, TvPacketTvChannelRepository>();
            services.AddScoped<ITvPacketTvChannelService, TvPacketTvChannelService>();

            services.AddScoped<IPacketCombinationRepository, PacketCombinationRepository>();
            services.AddScoped<IPacketCombinationService, PacketCombinationService>();

            services.AddScoped<IUserContractRepository, UserContractRepository>();
            services.AddScoped<IUserContractService, UserContractService>();

            var key = Encoding.ASCII.GetBytes(Configuration["Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
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
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
