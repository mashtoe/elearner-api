using ELearner.Core.DomainService;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class LocalFileAccess : IFileAccess {

        //private HttpClient _client;
        private string pathUri = "C:/ElearnerFiles/dogs.mp4";

        public LocalFileAccess() {
            // _client = new HttpClient();
        }

        public async Task<Stream> GetVideo() {
            using (MemoryStream mem = new MemoryStream()) {
                var stuff = File.ReadAllBytes(pathUri);
                mem.Write(stuff, 0, (int)stuff.Length);
                return await new Task<Stream>(() => 
                {
                    return mem;
                });
            }
            //return await _client.GetStreamAsync(pathUri);
        }
    }
}
