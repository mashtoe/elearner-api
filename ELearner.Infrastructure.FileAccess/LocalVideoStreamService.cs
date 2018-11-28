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
        }
    }
}
