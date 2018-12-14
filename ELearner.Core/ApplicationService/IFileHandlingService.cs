using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Core.ApplicationService {
    public interface IFileHandlingService {
        // 
        Stream GetVideoStream(string id);
        // upload file to destination
        UndistributedCourseMaterialBO UploadFile(IFormFile file, int courseId, IProgress<UploadProgress> progress, int jobId, string fileName);

    }
}
