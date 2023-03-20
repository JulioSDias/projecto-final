using Projecto_Final.Models.ImageDTOs;

namespace Projecto_Final.Services
{
    public interface IImageService
    {
        Task<bool> UploadImage(ImageUploadDTO newImage);
        Task<ImageReturnDTO> GetById(int id);
        Task<List<ImageReturnDTO>> GetImagesByProductId(int id);
        Task<bool> Delete(int id);
    }
}
