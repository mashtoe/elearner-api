using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Core.DomainService {
    public interface IFileHandler {
        Stream GetVideoStream(string id);
        // return name of uploaded file pls
        string UploadFile(IFormFile file);
    }
}