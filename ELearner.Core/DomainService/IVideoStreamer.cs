using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Core.DomainService {
    public interface IVideoStreamer {
        Stream GetVideoStream(string id);
        void UploadFile(IFormFile file);
    }
}
