using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ELearner.Infrastructure.FileAccess {

    public class WebClientNoTimeOut : WebClient {
        protected override WebRequest GetWebRequest(Uri address) {
            FtpWebRequest request = (FtpWebRequest)base.GetWebRequest(address);
            request.Timeout = 900000; //15 minute timeout
            return request;
        }
    }
}


