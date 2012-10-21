using Nancy;

namespace WithExtension
{
    public static class FormatterExtensions
    {
        public static Response AsFileEx(this IResponseFormatter formatter, string applicationRelativeFilePath, string contentType)
        {
            return new GenericFileResponseEx(applicationRelativeFilePath, contentType);
        }

        public static Response AsFileEx(this IResponseFormatter formatter, string applicationRelativeFilePath)
        {
            return new GenericFileResponseEx(applicationRelativeFilePath);
        }

        public static Response AsCssEx(this IResponseFormatter formatter, string applicationRelativeFilePath)
        {
            return AsFileEx(formatter, applicationRelativeFilePath);
        }

        public static Response AsImageEx(this IResponseFormatter formatter, string applicationRelativeFilePath)
        {
            return AsFileEx(formatter, applicationRelativeFilePath);
        }

        public static Response AsJsEx(this IResponseFormatter formatter, string applicationRelativeFilePath)
        {
            return AsFileEx(formatter, applicationRelativeFilePath);
        }

    }
}