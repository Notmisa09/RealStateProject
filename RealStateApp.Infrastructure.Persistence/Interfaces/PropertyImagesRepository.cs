using RealStateApp.Core.Application.Interfaces.IRepository;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Context;
using RealStateApp.Infrastructure.Persistence.Repositories;

namespace RealStateApp.Infrastructure.Persistence.Interfaces
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
    }
}
