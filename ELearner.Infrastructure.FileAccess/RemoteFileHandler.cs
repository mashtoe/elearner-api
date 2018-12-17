using ELearner.Core.DomainService;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Net.FtpClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class RemoteFileHandler: IFileHandler {

        // private static string pathFtp = "ftp://elearning@elearning.vps.hartnet.dk/www/lessonFiles/";

        public RemoteFileHandler() {
        }

        public Stream GetVideoStream(string name) {
            return new PartialHTTPStream(name);
        }


        public string UploadFile(IFormFile file, IProgress<UploadProgress> progress, int jobId, string fileName) {
            try {
                string fullFileName = "";
                using (FtpClient client = new FtpClient()) {
                    SetCredentials(client);
                    if (file.ContentType.Equals("video/mp4")) {
                        fullFileName = fileName + ".mp4";
                        string fileUri = "/www/lessonFiles/" + fullFileName;

                        using (var ftpStream = client.OpenWrite(fileUri)) {
                            using (var fileStream = file.OpenReadStream()) {
                                // fileStream.CopyTo(ftpStream);
                                byte[] buffer = new byte[512 * 1024];
                                int read;
                                int currentProgress = 0;
                                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                                    ftpStream.Write(buffer, 0, read);
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
                }
                return fullFileName;
            } catch (Exception ex) {
                return null;
            }

        }

        
        private void SetCredentials(FtpClient client) {
            client.Host = "elearning.vps.hartnet.dk";
            // client.Port = _settings.FtpsRemotePort;
            client.DataConnectionConnectTimeout = 60000;
            client.ConnectTimeout = 60000;
            client.Credentials = new NetworkCredential("elearning", "eLearn!ng");
            client.DataConnectionType = 0;
        }

        /*
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

        public string UploadFile2(IFormFile file) {
            string fileName = null;
            using (WebClientNoTimeOut client = new WebClientNoTimeOut()) {
                if (file.ContentType.Equals("video/mp4")) {
                    string uuid = Guid.NewGuid().ToString();
                    fileName = uuid + ".mp4";
                    string fileUri = pathFtp + fileName;
                    SetCredentials(client);
                    using (Stream fileStream = file.OpenReadStream()) {
                        var ftpWriteStream = client.OpenWrite(fileUri);
                        byte[] buffer = new byte[512 * 1024];
                        int read;
                        while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0) {
                            ftpWriteStream.Write(buffer, 0, read);
                            double progress = fileStream.Position * 100 / fileStream.Length;
                        }

                        // 2
                        // fileStream.CopyTo(openWriteStream);

                        // 1
                        // byte[] data = ReadFully(fileStream);
                        // client.UploadData(fileUri, data);
                    }
                }
            }
            return fileName;
        }*/
    }
}
