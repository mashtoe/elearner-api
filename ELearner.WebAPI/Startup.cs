using Elearner.Infrastructure.Data;
using Elearner.Infrastructure.Data.Facade;
using Elearner.Infrastructure.Data.UOW;
using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.Services;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Utilities;
using ELearner.Infrastructure.Static.Data.Repositories;
using ELearner.Infrastructure.Static.Data.UOW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
//using ELearner.Infrastructure.Data.Repositories;

namespace Elearner.API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //services.AddDbContext<ElearnerAppContext>(option => option.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ElearnerAppContext>(option => option.UseInMemoryDatabase("TheDB"));
            services.AddScoped<IDataFacade, DataFacade>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IDataSeeder, DataSeeder>();

            // use following line instead for static "db"
            //services.AddScoped<IUnitOfWork, UnitOfWorkStatic>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Here Cross-Origin Resource Sharing is added
            // Important that this line is before AddMvc
            services.AddCors();

            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    // since datseeder is located in the core, the dataseeder dont know anything about 
                    // what kind of data persistance or database we use
                    var service = scope.ServiceProvider.GetService<IDataSeeder>();
                    service.SeedData();
                }
            } else {
                app.UseHsts();
            }
            // Origins who are allowed to request data from this api is listed here. We allow all http methods and all headers atm
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

            //Lars outcommented the line below in his Clean Architecture RestAPI setup guide. will figure out why when i watch the next series
            // --means we can go to localhost witohut using https
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}


// here we define which implementation of the repositories we want to use, when we use interfaces for dependancyinjectection
// in the constructor of the StudentsController class we dependancy inject the studentservice etc

