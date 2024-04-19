using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repository;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropertyImagesRepository : BaseRepository<PropertyImages> , IPropertyImagesRepository
    {
        private readonly RealStateContext _context;
        public PropertyImagesRepository(RealStateContext context) : base(context){ _context = context; }

        public string GetFirstImage(int PropertyId)
        {
            var image =  _context.PropertyImages.FirstOrDefault(x => x.PropertyId == PropertyId);
            if (image == null)
            {
                return null;
            }
            var imageURL = image.ImageURL;
            return imageURL;
        }


        public async Task RemoveImages(int PropertyId)
        {
            var images = _context.PropertyImages.Where(x => x.PropertyId == PropertyId).ToList();
            _context.RemoveRange(images);
            await _context.SaveChangesAsync();
        }
    }
}
