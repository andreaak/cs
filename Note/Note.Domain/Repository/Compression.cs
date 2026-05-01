using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Note.Domain.Repository
{
    public static class  Compression
    {
        public static byte[] Compress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public static string Compress(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            using (var memoryStream = new MemoryStream())
            {
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(buffer, 0, buffer.Length);
                }

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static string Decompress(string compressedText)
        {
            byte[] buffer = Convert.FromBase64String(compressedText);

            using (var memoryStream = new MemoryStream(buffer))
            using (var gzip = new GZipStream(memoryStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream())
            {
                gzip.CopyTo(resultStream);
                return Encoding.UTF8.GetString(resultStream.ToArray());
            }
        }

        public static byte[] CompressToGzip(string text)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);

            using (var output = new MemoryStream())
            {
                using (var gzip = new GZipStream(output, CompressionMode.Compress))
                {
                    gzip.Write(inputBytes, 0, inputBytes.Length);
                }

                return output.ToArray();
            }
        }

        public static string DecompressGzip(byte[] compressedBytes)
        {
            using (var input = new MemoryStream(compressedBytes))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                gzip.CopyTo(output);
                return Encoding.UTF8.GetString(output.ToArray());
            }
        }
    }
}
