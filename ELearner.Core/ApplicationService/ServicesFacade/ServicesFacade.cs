namespace ELearner.Core.ApplicationService.ServicesFacade {
    public class ServicesFacade : IServicesFacade {

        public IUserService UserService { get; }
        public ICourseService CourseService { get; }


        public ServicesFacade(IUserService userService,
            ICourseService courseService) {
            UserService = userService;
            CourseService = courseService;
        }
    }
}
