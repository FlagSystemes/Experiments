using System;
using System.IO;
using Nancy;

namespace DirectToStream
{
    public static class FormatterExtensions
    {
        public static Response AsStream(this IResponseFormatter formatter, Func<Stream> readStream, string contentType)
        {
            return new StreamResponse(readStream, contentType);
        }
    }
}