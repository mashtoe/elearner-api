using ELearner.Core.DomainService;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace ELearner.Infrastructure.FileAccess {
    public class RemoteFileHandler: IFileHandler {

        private static string pathFtp = "ftp://elearning@elearning.vps.hartnet.dk/www/lessonFiles/";

        public RemoteFileHandler() {
        }

        public Stream GetVideoStream(string name) {
            return new PartialHTTPStream(name);
        }

        public string UploadFile(IFormFile file) {
            string fileName = null;
            using (WebClientNoTimeOut client = new WebClientNoTimeOut()) {
                if (file.ContentType.Equals("video/mp4")) {
                    string uuid = Guid.NewGuid().ToString();
                    fileName = uuid + ".mp4";
                    string fileUri = pathFtp + fileName;
                    SetCredentials(client);
                    using (Stream fileStream = file.OpenReadStream()) {
                        var openWriteStream = client.OpenWrite(fileUri);
                        fileStream.CopyTo(openWriteStream);
                        // byte[] data = ReadFully(fileStream);
                        // client.UploadData(fileUri, data);
                    }
                     //var openWriteStream = client.OpenWrite(fileUri, "STOR");
                    //await file.CopyToAsync(openWriteStream);
                }
            }
            return fileName;
        }

        public static byte[] ReadFully(Stream input) {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Sets the credentials fot the ftp server, so we can access our files
        /// </summary>
        /// <param name="client"></param>
        public void SetCredentials(WebClient client) {
            //client.Credentials = new NetworkCredential("elearning.vps.hartnet.dk", "eLearn!ng");
            client.Credentials = new NetworkCredential("elearning", "eLearn!ng");
        }
    }
}
