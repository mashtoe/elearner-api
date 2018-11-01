using Elearner.Infrastructure.Data;
using Elearner.Infrastructure.Data.Repositories;
using Elearner.Infrastructure.Data.UOW;
using ELearner.Core.ApplicationService;
using ELearner.Core.ApplicationService.Services;
using ELearner.Core.ApplicationService.ServicesFacade;
using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.DomainService.UOW;
using ELearner.Core.Entity;
using ELearner.Infrastructure.Static.Data;
using ELearner.Infrastructure.Static.Data.Repositories;
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
            services.AddDbContext<ElearnerAppContext>(option => option.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ElearnerAppContext>(option => option.UseInMemoryDatabase("TheDB"));
            
            services.AddScoped<IServicesFacade, ServicesFacade>();

            //services.AddScoped<IDataAccessFacade, DataAccessFacade>();
            // use following line instead for static "db"
            services.AddScoped<IDataAccessFacade, DataAccessFacadeStatic>();

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
                    var _context = scope.ServiceProvider.GetService<ElearnerAppContext>();
                    DBInit.SeedDB(_context);
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
//services.AddScoped<IStudentRepository, Infrastructure.Data.Repositories.StudentRepository>();
//services.AddScoped<IStudentService, StudentService>();
//services.AddScoped<ICourseService, CourseService>();
//services.AddScoped<ICourseRepository, Infrastructure.Data.Repositories.CourseRepository>();
//services.AddScoped<IUnitOfWork, UnitOfWork>();