using AutoMapper;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces.Events;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.Photo;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImageClasificationResultEventHandler : IIntegrationEventHandler<ImageClasificationResultIntegrationEvent>
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public ImageClasificationResultEventHandler(
            IPhotoService photoService,
            IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }

        public async Task Handle(ImageClasificationResultIntegrationEvent @event)
        {
            var photoViewModel = _mapper.Map<PhotoViewModel>(@event);
            var model = await _photoService.Get(photoViewModel.Id);
            if (model == null)
            {
                return;
            }

            photoViewModel.UserId = model.UserId;
            await _photoService.Update(photoViewModel);
            await _photoService.SaveChanges();
        }
    }
}
