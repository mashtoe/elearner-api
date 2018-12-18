using System;
using System.Collections.Generic;
using System.Text;

namespace ELearner.Core.Entity.Dtos
{
    public class UploadProgress
    {
        public int Progress { get; set; }
        public int JobId { get; set; }
        // used to determine which file this progress is for, in the client
        public string FileName { get; set; }
    }
}
