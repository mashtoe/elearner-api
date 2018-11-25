using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELearner.Core.DomainService {
    public interface IFileAccess {
        Task<Stream> GetVideo();
    }
}
