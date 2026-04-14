using Baytak.Application.DTOs.PropertyImage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IPropertyImageService
    {
        Task<string> UploadAsync(UploadImageDto dto);
        Task<IEnumerable<PropertyImageResponseDto>> GetImagesAsync(GetImageDto dto);
        Task DeleteAsync(Guid imageId,string userId);
    }
}
