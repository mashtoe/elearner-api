using ELearner.Core.DomainService;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class LocalFileHandler : IFileHandler {
        readonly string localPath = @"C:\ElearnerFiles\"; 

        public Stream GetVideoStream(string id) {
            var url = localPath + id;
            var stream = (Stream)new FileStream(id, FileMode.Open, System.IO.FileAccess.Read);
            return stream;
        }

        public string UploadFile(IFormFile file) {
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
                return fileName;
            }
            throw new Exception();
        }

        public string UploadFil2e(IFormFile file, IProgress<UploadProgress> progress, int jobId, string fileName)
        {
            throw new NotImplementedException();
        }

        public string UploadFile(IFormFile file, IProgress<UploadProgress> progress, int jobId, string fileName) {
            try {
                string fullFileName = "";
                if (file.ContentType.Equals("video/mp4")) {
                    System.IO.Directory.CreateDirectory(localPath);
                    fullFileName = fileName + ".mp4";
                    string fileUri = localPath + fullFileName;

                    using (var localFileStream = new FileStream(fileUri, FileMode.Create)) {
                        using (var fileStream = file.OpenReadStream()) {
                            // fileStream.CopyTo(ftpStream);
                            byte[] buffer = new byte[512 * 1024];
                            int read;
                            int currentProgress = 0;
                            while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                                localFileStream.Write(buffer, 0, read);
                                int newProgress = (int)(fileStream.Position * 100 / fileStream.Length);
                                if (currentProgress != newProgress) {
                                    progress.Report(new UploadProgress() { JobId = jobId, Progress = currentProgress, FileName = fileName });
                                    currentProgress = newProgress;
                                }
                            }
                            progress.Report(new UploadProgress() { JobId = jobId, Progress = 100, FileName = fileName });
                        }
                    }
                }
                return fullFileName;
            } catch (Exception ex) {
                return null;
            }

        }
    }
}
