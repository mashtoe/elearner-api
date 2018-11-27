using ELearner.Core.DomainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class VideoStreamService: IVideoStreamer {

        private HttpClient _client;

        public VideoStreamService() {
            _client = new HttpClient();
        }

        public Stream GetVideoStream(string name) {
            var stream = _client.GetStreamAsync(name).Result;

            //var wc = new WebClient();
            //var stream2 = wc.OpenRead(name);
            
            //var req = System.Net.WebRequest.Create(name);
            //FileStream stream = (FileStream)req.GetResponse().GetResponseStream();
            return stream;
        }
        
        ~VideoStreamService() {
            if (_client != null)
                _client.Dispose();
        }
    }
}
