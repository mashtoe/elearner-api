using ELearner.Core.DomainService;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ELearner.Infrastructure.FileAccess {
    public class VideoStreamService: IVideoStreamer {

        private static string pathFtp = "ftp://elearning@elearning.vps.hartnet.dk/www/lessonFiles/";

        public VideoStreamService() {
        }

        public Stream GetVideoStream(string name) {
            return new PartialHTTPStream(name);
        }

        public void UploadFile(IFormFile file) {

        }

        public string UploadData(byte[] data, string fileType) {
            using (WebClient client = new WebClient()) {
                string uuid = Guid.NewGuid().ToString();
                SetCredentials(client);
                string fileName = uuid + fileType;
                string fileUri = pathFtp + fileName;
                client.UploadData(fileUri, data);
                return fileName;
            }
        }

        /// <summary>
        /// Sets the credentials fot the ftp server, so we can access our files
        /// </summary>
        /// <param name="client"></param>
        public void SetCredentials(WebClient client) {
            client.Credentials = new NetworkCredential("elearning.vps.hartnet.dk ", "eLearn!ng");
        }
    }
}
