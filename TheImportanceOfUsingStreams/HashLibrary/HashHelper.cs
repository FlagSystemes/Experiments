using System.IO;
using System.Security.Cryptography;

public class HashHelper
{
    static SHA512 sha512;

    static HashHelper()
    {
        sha512 = SHA512.Create();
    }

    public static byte[] ComputeHashWithStream(string path)
    {
        using (var stream = File.OpenRead(path))
        {
            return sha512.ComputeHash(stream);
        }
    }

    public static byte[] ComputeHashWithBytes(string path)
    {
        byte[] fileBytes;
        using (var stream = File.OpenRead(path))
        {
            var length = (int) stream.Length;
            var buffer = new byte[length];
            stream.Read(buffer, 0, length);
            fileBytes = buffer;
        }
        return sha512.ComputeHash(fileBytes);
    }
}