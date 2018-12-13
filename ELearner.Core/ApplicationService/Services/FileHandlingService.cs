using ELearner.Core.DomainService;
using ELearner.Core.DomainService.Facade;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Converters;
using ELearner.Core.Entity.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELearner.Core.ApplicationService.Services {
    public class FileHandlingService : IFileHandlingService {

        readonly IVideoStreamer _videoStream;
        readonly IDataFacade _facade;
        readonly UndistributedCourseMaterialConverter _matConv;


        public FileHandlingService(IDataFacade facade, IVideoStreamer videoStream) {
            _facade = facade;
            _videoStream = videoStream;
            _matConv = new UndistributedCourseMaterialConverter();
        }

        public Stream GetVideoStream(string name) {
            var url = "http://elearning.vps.hartnet.dk/lessonFiles/" + name;
            // CTRL E -> V ev
            //var url = "C:/ElearnerFiles/long.mp4";
            //var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            return _videoStream.GetVideoStream(url);
        }

        public UndistributedCourseMaterialBO UploadFile(IFormFile file, int courseId) {
            using (var uow = _facade.UnitOfWork) {
                // upload file
                var fileName = _videoStream.UploadFile(file);
                if (fileName != null) {
                    // create and return material object on success
                    var course = uow.CourseRepo.Get(courseId);
                    var material = new UndistributedCourseMaterial() {
                        VideoId = fileName,
                        Course = course
                    };
                    uow.Complete();
                    return _matConv.Convert(material);
                } else { return null; }
            }

        }
    }
}
