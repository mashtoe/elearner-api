using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ELearner.Infrastructure.FileAccess {
    public class Helper {
        private readonly string _filename;

        public Helper() {
            _filename = "C:/ElearnerFiles/sample.mp4";
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context) {
            try {
                var buffer = new byte[65536];

                using (var file = File.Open(_filename, FileMode.Open, System.IO.FileAccess.Read)) {
                    var length = (int)file.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0) {
                        bytesRead = file.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            } catch (Exception ex) {
                return;
            } finally {
                outputStream.Close();
            }
        }
    }
}
