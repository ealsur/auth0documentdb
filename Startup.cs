using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using auth0documentdb.Models;
using auth0documentdb.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace auth0documentdb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)

        {
            services.AddAuthentication(
                options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
            services.AddSingleton<IDocumentDbService>(x=>new DocumentDbService(Configuration));
            //Based on https://auth0.com/docs/quickstart/webapp/aspnet-core/02-login-embedded-lock
            services.Configure<OpenIdConnectOptions>(options =>
            {
                // Specify Authentication Scheme
                options.AuthenticationScheme = "Auth0";

                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["auth0:domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["auth0:clientId"];
                options.ClientSecret = Configuration["auth0:clientSecret"];

                // Do not automatically authenticate and challenge
                options.AutomaticAuthenticate = false;
                options.AutomaticChallenge = false;

                // Set response type to code
                options.ResponseType = "code";

                // Set the callback path, so Auth0 will call back to http://localhost:60856/signin-auth0 
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard 
                options.CallbackPath = new PathString("/signin-auth0");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";
            });

            services.AddMvc();
            services.AddOptions();
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<OpenIdConnectOptions> oidcOptions)
        {

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            app.UseOpenIdConnectAuthentication(oidcOptions.Value);

            
            app.UseMvcWithDefaultRoute();
        }
    }
}
