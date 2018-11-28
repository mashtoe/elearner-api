using System;
using System.IO;
using System.Net;

namespace ELearner.Infrastructure.FileAccess {
    // codereview.stackexchange.com/questions/70679/seekable-http-range-stream
    public class PartialHTTPStream : Stream, IDisposable {
        Stream stream;
        WebResponse resp;
        int cacheRemaining = 0;
        const int cachelen = 259072;  //253 * 1024 (originally 1024)

        public string Url { get; private set; }
        public override bool CanRead { get { return true; } }
        public override bool CanWrite { get { return false; } }
        public override bool CanSeek { get { return true; } }

        long position = 0;
        public override long Position {
            get { return position; }
            set {
                long delta = value - position;
                if (delta == 0)
                    return;
                if (delta > 0 && delta < cacheRemaining) {
                    // Console.WriteLine("Seeking in cache");
                    byte[] dummy = new byte[delta];
                    cacheRemaining -= (int)delta;
                    while (delta > 0) {
                        int nread = stream.Read(dummy, 0, (int)delta);
                        if (nread == 0) throw new IOException();
                        delta -= nread;
                    }
                } else cacheRemaining = 0;
                position = value;
                // Console.WriteLine("Seek {0}", value);
            }
        }

        long? length;
        public override long Length {
            get {
                if (length == null) {
                    HttpWebRequest request = HttpWebRequest.CreateHttp(Url);
                    request.Method = "HEAD";
                    length = request.GetResponse().ContentLength;
                }
                return length.Value;
            }
        }

        public PartialHTTPStream(string Url) { this.Url = Url; }

        public override void SetLength(long value) { throw new NotImplementedException(); }

        public override int Read(byte[] buffer, int offset, int count) {

            if (cacheRemaining == 0) {
                // Console.WriteLine("Cache miss");
                if (stream != null) {
                    stream.Close();
                    resp.Close();
                }
                HttpWebRequest req = HttpWebRequest.CreateHttp(Url);
                cacheRemaining = (int)Math.Min(Length - Position, Math.Max(count, cachelen));
                req.AddRange(Position, Position + cacheRemaining - 1);
                resp = req.GetResponse();
                stream = resp.GetResponseStream();
            }

            count = Math.Min(buffer.Length - offset, Math.Min(cacheRemaining, count));
            // Console.WriteLine("Read {0} @ {1}", count, Position);

            int nread = stream.Read(buffer, offset, count);
            position += nread;
            cacheRemaining -= nread;
            return nread;
        }

        public override void Write(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }

        public override long Seek(long pos, SeekOrigin origin) {
            switch (origin) {
                case SeekOrigin.End:
                    Position = Length + pos;
                    break;
                case SeekOrigin.Begin:
                    Position = pos;
                    break;
                case SeekOrigin.Current:
                    Position += pos;
                    break;
            }
            return Position;
        }

        public override void Flush() { }

        new void Dispose() {
            base.Dispose();
            if (stream != null) {
                stream.Dispose();
                stream = null;
            }
            if (resp != null) {
                resp.Dispose();
                resp = null;
            }
        }
    }
}
