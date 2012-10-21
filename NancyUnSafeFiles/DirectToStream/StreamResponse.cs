using System;
using System.IO;
using Nancy;

namespace DirectToStream
{
    public class StreamResponse : Response
    {
        public StreamResponse(Func<Stream> readStream, string contentType)
        {
            Contents = stream =>
                           {
                               using (var read = readStream())
                               {
                                   read.CopyTo(stream);
                               }
                           };
            ContentType = contentType;
            StatusCode = HttpStatusCode.OK;
        }
    }
}