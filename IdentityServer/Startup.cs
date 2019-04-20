using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServer
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //// IdentityServer
            //services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            //services.AddAuthentication(Configuration["Identity:Scheme"])
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.RequireHttpsMetadata = false; // for dev env
            //    options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
            //        options.ApiName = Configuration["Service:Name"]; // match with configuration in IdentityServer
            //});
            InMemoryConfiguration.Configuration = this.Configuration;

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddTestUsers(InMemoryConfiguration.GetUsers().ToList())
            //    .AddInMemoryClients(InMemoryConfiguration.GetClients())
            //    .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources());

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential();

            services.AddIdentityServer()
                .AddInMemoryPersistedGrants()
                .AddInMemoryApiResources(InMemoryConfiguration.GetApiResources())
                //.AddInMemoryIdentityResources(InMemoryConfiguration.GetApiResources())
                .AddInMemoryClients(InMemoryConfiguration.GetClients())
                .AddTestUsers(InMemoryConfiguration.GetUsers())
                .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }

    public class InMemoryConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service"),
                new ApiResource("productservice", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //new Client
                //{
                //    ClientId = "client.api.service",
                //    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //    AllowedScopes = new [] { "clientservice" }
                //},
                //new Client
                //{
                //    ClientId = "product.api.service",
                //    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                //    AllowedScopes = new [] { "clientservice", "productservice" }
                //},
                new Client
                {
                    ClientId = "ccshis.pms",
                    ClientSecrets = new [] { new Secret("ccshis.pms".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                }
            };
        }

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword"
                },
                //new TestUser
                //{
                //    SubjectId = "10002",
                //    Username = "andy@hotmail.com",
                //    Password = "andypassword"
                //},
                //new TestUser
                //{
                //    SubjectId = "10003",
                //    Username = "leo@hotmail.com",
                //    Password = "leopassword"
                //}
            }.ToList();
        }
    }
}
