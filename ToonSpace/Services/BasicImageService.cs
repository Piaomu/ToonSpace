using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ToonSpace.Services
{
    public class BasicImageService : IImageService
    {
        public async Task<byte[]> EncodeImageAsync(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            using var ms = new MemoryStream();
            await image.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot{fileName}";
            return await File.ReadAllBytesAsync(file);
        }

        public async Task<byte[]> EncodeImageURLAsync(string imageURL)
        {
            var client = new HttpClient();

            var response = await client.GetAsync(imageURL);

            Stream stream = await response.Content.ReadAsStreamAsync();

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return ms.ToArray();
        }

        public string DecodeImage(byte[] image, string contentType)
        {
            if (image == null)
            {
                return null;
            }
            var convertedImage = Convert.ToBase64String(image);
            return $"data:{contentType};base64,{convertedImage}";
        }

        public string ContentType(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            return image.ContentType;
        }
    }
}
