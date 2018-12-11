using ELearner.Core.DomainService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class LocalVideoStreamService : IVideoStreamer {
        public Stream GetVideoStream(string id) {
            var stream = (Stream) new FileStream(id, FileMode.Open, System.IO.FileAccess.Read);
            return stream;
        }

        public void UploadFile(IFormFile file) {
            string folderName = "Upload";
            // string webRootPath = _hostingEnvironment.WebRootPath;
            string localPath = "C:/ElearnerFiles/videos/";
            string newPath = Path.Combine(localPath, folderName);
            if (!Directory.Exists(newPath)) {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0) {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create)) {
                    file.CopyTo(stream);
                }
            }
        }
    }
}
