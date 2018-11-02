namespace ELearner.Core.ApplicationService.ServicesFacade {
    public class ServicesFacade : IServicesFacade {

        public IStudentService StudentService { get; }
        public ICourseService CourseService { get; }


        public ServicesFacade(IStudentService studentService,
            ICourseService courseService) {
            StudentService = studentService;
            CourseService = courseService;
        }
    }
}
