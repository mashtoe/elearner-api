using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.Services;
using ELearner.Core.DomainService;
using ELearner.Infrastructure.Static.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elearner.API
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

            // here we define which implementation of the repositories we want to use, when we use interfaces for dependancyinjectection
            // in the constructor of the StudentsController class we dependancy inject the studentservice etc
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            //Lars outcommented the line below in his Clean Architecture RestAPI setup guide. will figure out why when i watch the next series
            // means we can go to localhost witohut using https
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
