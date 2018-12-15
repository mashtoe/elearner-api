using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Dtos;
using ELearner.Core.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELearner.Core.ApplicationService.Services {
    public class FileHandlingService : IFileHandlingService {

        readonly IFileHandler _fileHanlder;
        readonly IDataFacade _facade;
        readonly UndistributedCourseMaterialConverter _matConv;
        readonly LessonConverter _lesConv;


        public FileHandlingService(IDataFacade facade, IFileHandler fileHandler) {
            _facade = facade;
            _fileHanlder = fileHandler;
            _matConv = new UndistributedCourseMaterialConverter();
            _lesConv = new LessonConverter();
        }

        public Stream GetVideoStream(string name) {
            var url = "http://elearning.vps.hartnet.dk/lessonFiles/" + name;
            // CTRL E -> V ev
            //var url = "C:/ElearnerFiles/long.mp4";
            //var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            return _fileHanlder.GetVideoStream(url);
        }

        public LessonBO UploadFile(IFormFile file, int courseId, IProgress<UploadProgress> progress, int jobId, string fileName) {
            using (var uow = _facade.UnitOfWork) {
                // generate filename
                // upload file
                string fullFileName = _fileHanlder.UploadFile(file, progress, jobId, fileName);
                if (fullFileName != null) {
                    // create and return material object on success
                    var course = uow.CourseRepo.Get(courseId);
                    /*var material = new UndistributedCourseMaterial() {
                        VideoId = fullFileName,
                        Course = course
                    };*/
                    var lesson = new Lesson() {
                        VideoId = fullFileName,
                        Title = fullFileName,
                        Course = course
                    };

                    course.Lessons.Add(lesson);
                    uow.Complete();
                    return _lesConv.Convert(lesson);
                } else { return null; }
            }

        }
    }
}
