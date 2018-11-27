using ELearner.Core.DomainService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Infrastructure.FileAccess {
    public class LocalVideoStreamService : IVideoStreamer {
        public Stream GetVideoStream(string id) {
            var stream = (Stream) new FileStream(id, FileMode.Open, System.IO.FileAccess.Read);
            return stream;
            //var stream = await new FileStream(id, FileMode.Open, FileAccess.Read);

            //return new FileStreamResult(stream, new MediaTypeHeaderValue("video/mp4").MediaType);
        }
    }
}
