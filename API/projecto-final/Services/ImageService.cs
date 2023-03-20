using Microsoft.EntityFrameworkCore;
using Projecto_Final.Contexts;
using Projecto_Final.Models;
using Projecto_Final.Models.ImageDTOs;

namespace Projecto_Final.Services
{
    public class ImageService : IImageService
    {
        private readonly StoreContext _context;
        public ImageService(StoreContext context)
        {
            _context = context;
        }
        public async Task<bool> UploadImage(ImageUploadDTO newImage)
        {
            using var memoryStream = new MemoryStream();
            await newImage.Image.CopyToAsync(memoryStream);

            var image = new ProductImage
            {
                Name = newImage.Image.FileName,
                FileExtention = Path.GetExtension(newImage.Image.FileName),
                CreatedDate = DateTimeOffset.Now,
                Image = memoryStream.ToArray(),
                ProductId = newImage.ProductId
            };

            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ImageReturnDTO> GetById(int id) {

            var DBimage = await _context.Images.FindAsync(id);
            if (DBimage == null) return null;

            var imageString = $"data:image/{DBimage.FileExtention};base64," + Convert.ToBase64String(DBimage.Image);
            
            var returnImage = new ImageReturnDTO
            {
                Id = DBimage.Id,
                Name = DBimage.Name,
                Image = imageString,
                CreatedDate = DBimage.CreatedDate,
            };
            return returnImage;
        }

        public async Task<List<ImageReturnDTO>> GetImagesByProductId(int id) {
            var DBimages = await _context.Images.Where(p => p.ProductId == id).ToListAsync();
            if (DBimages == null) return null;

            var returnImages = new List<ImageReturnDTO>();
            foreach (var image in DBimages) {
                var returnImage = new ImageReturnDTO
                {
                    Id = image.Id,
                    Name = image.Name,
                    Image = $"data:image/{image.FileExtention};base64," + Convert.ToBase64String(image.Image),
                    CreatedDate = image.CreatedDate,
                };
                returnImages.Add(returnImage);
            }
            return returnImages;
        }

        public async Task<bool> Delete(int id) {
            var DBimage = await _context.Images.FindAsync(id);
            if (DBimage == null) return false;

            _context.Images.Remove(DBimage);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
