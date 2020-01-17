using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Webapp
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

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => {
                    this.Configuration.Bind("AzureAdB2C", options);
                })
                .AddOpenIdConnect("o365", options =>
                {
                    AzureADB2COptions b2cOptions = new AzureADB2COptions();
                    this.Configuration.Bind("AzureAdB2C", b2cOptions);

                    var tenantName = b2cOptions.Domain.Substring(0, b2cOptions.Domain.IndexOf('.'));
                    options.Authority = $"https://{tenantName}.b2clogin.com/tfp/{b2cOptions.Domain}/{b2cOptions.SignUpSignInPolicyId}-o365/v2.0";
                    options.ClientId = b2cOptions.ClientId;
                    options.CallbackPath = b2cOptions.CallbackPath + "-o365";
                    options.TokenValidationParameters.NameClaimType = "name";
                })
                ;
            services.AddRazorPages();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
