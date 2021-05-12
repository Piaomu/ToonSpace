using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToonSpace.Services
{
    public interface IImageService
    {
        Task<byte[]> EncodeImageAsync(IFormFile poster);

        //encode an image from a url
        Task<byte[]> EncodeImageURLAsync(string imageURL);

        string DecodeImage(byte[] poster, string contentType);

        string ContentType(IFormFile poster);
    }
}
