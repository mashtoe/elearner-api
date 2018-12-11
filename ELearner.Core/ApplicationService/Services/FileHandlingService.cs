using ELearner.Core.DomainService;
using ELearner.Core.Entity.BusinessObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELearner.Core.ApplicationService.Services {
    public class FileHandlingService : IFileHandlingService {

        readonly IVideoStreamer _videoStream;

        public FileHandlingService(IVideoStreamer videoStream) {
            _videoStream = videoStream;
        }

        public Stream GetVideoStream(string name) {
            var url = "http://elearning.vps.hartnet.dk/lessonFiles/" + name;
            // CTRL E -> V ev
            //var url = "C:/ElearnerFiles/long.mp4";
            //var url = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";

            return _videoStream.GetVideoStream(url);
        }

        public UndistributedCourseMaterialBO UploadFile(IFormFile file) {
            // upload file
            // create and return material object on success
            return null;

        }
    }
}
