using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Core.DomainService {
    public interface IFileHandler {
        Stream GetVideoStream(string id);
        // Return name of uploaded file (this used to find uploaded file again)
        string UploadFile(IFormFile file, IProgress<UploadProgress> progress, int jobId, string fileName);
    }
}