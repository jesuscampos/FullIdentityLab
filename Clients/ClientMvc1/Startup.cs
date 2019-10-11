using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using ClientMvc1.CookieHandler;
using CommonConstants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClientMvc1
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
            services.AddHttpClient();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";
                options.Authority = Constants.Authority;
                options.ClientId = "client";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";
                options.SaveTokens = false; //(*) aquí hay que hacer unas cuantas pruebas
                options.RequireHttpsMetadata = true;

                // logout
                // options.CallbackPath = null;
                // options.BackchannelHttpHandler = null;
                // options.SignedOutRedirectUri = "";
                // options.SignedOutCallbackPath = null;
                
                // scopes
                options.Scope.Clear();
                options.Scope.Add("openid");
                //options.Scope.Add("api1");

                // claims
                // options.GetClaimsFromUserInfoEndpoint = false;
                //options.ClaimActions.Clear();
                // options.TokenValidationParameters.NameClaimType = "name";
                // options.TokenValidationParameters.RoleClaimType = "role";

                // varios
                // options.MaxAge = TimeSpan.FromMinutes(10);
            });

            services.AddTransient<MyCookieEventHandler>();

            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
