﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.Entities;
using ProductCatalog.Core.Services;
using ProductCatalog.Data.Repositories;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using ProductCatalog.Api.Extensions;

namespace ProductCatalog.Api
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

            //DI
            services.AddScoped<IProductCatalogService, ProductCatalogService>();
            services.AddScoped<IProductCatalogRepository, ProductCatalogRepository>();

            //Entity
            services.AddDbContext<ProductCatalogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ProductCatalogConnectionString")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Products Catalog Api", Version = "v1", Description = "Products Catalog Api" });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            Mapper.Initialize(AutomapperConfigurationExtension.ConfigureAutomapperAPI);

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
