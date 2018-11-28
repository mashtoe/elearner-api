using ELearner.Core.DomainService;
using System.IO;
using System.Net.Http;

namespace ELearner.Infrastructure.FileAccess {
    public class VideoStreamService: IVideoStreamer {

        private HttpClient _client;

        public VideoStreamService() {
            _client = new HttpClient();
        }

        public Stream GetVideoStream(string name) {
            return new PartialHTTPStream(name);
        }

        ~VideoStreamService() {
            if (_client != null)
                _client.Dispose();
        }
    }
}
