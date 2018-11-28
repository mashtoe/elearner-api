using ELearner.Core.DomainService;
using System.IO;
using System.Net.Http;

namespace ELearner.Infrastructure.FileAccess {
    public class VideoStreamService: IVideoStreamer {
        public VideoStreamService() {
        }

        public Stream GetVideoStream(string name) {
            return new PartialHTTPStream(name);
        }
    }
}
