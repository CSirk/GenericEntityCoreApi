﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GenericEntityCoreApi.Core;
using GenericEntityCoreApi.Core.Mapping;
using GenericEntityCoreApi.Core.Models;
using GenericEntityCoreApi.Core.Services;
using GenericEntityCoreApi.Core.Services.Interfaces;
using GenericEntityCoreApi.EntityFramework.ExampleDatabase;
using GenericEntityCoreApi.EntityFramework.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace GenericEntityCoreApi.Api
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
            // Auto Mapper Configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //DB Contexts
            //server=YOURSERVER;port=YOURPORT;user=YOURUSER;password=YOURPASSWORD;database=YOURDATABASE
            services.AddDbContext<csirk_ExampleDatabaseContext>(options => options.UseMySQL("server=YOURSERVER;port=YOURPORT;user=YOURUSER;password=YOURPASSWORD;database=YOURDATABASE"));

            //Repos
            services.AddTransient<IGenericRepository<csirk_ExampleDatabaseContext, Customer>, CustomerRepository>();
            services.AddTransient<IGenericRepository<csirk_ExampleDatabaseContext, Animal>, AnimalRepository>();


            //Services
            services.AddTransient<IGenericService<DomCustomer, Customer, csirk_ExampleDatabaseContext>, CustomerService>();
            services.AddTransient<IGenericService<DomAnimal, Animal, csirk_ExampleDatabaseContext>, AnimalService>();


            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
