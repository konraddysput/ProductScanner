using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Hubs;
using ProductScanner.Gateway.Interfaces;
using ProductScanner.Gateway.Interfaces.Events;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.Photo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImageClasificationResultEventHandler : IIntegrationEventHandler<ImageClasificationResultEvent>
    {
        private readonly IPhotoObjectService _photoObject;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        private readonly IEventBus _eventBus;


        public ImageClasificationResultEventHandler(
            IEventBus eventBus,
            IPhotoService photoService,
            IPhotoObjectService photoObjectService,
            IHubContext<PreprocesingHub> hub,
            IMapper mapper)
        {
            _photoObject = photoObjectService;
            _photoService = photoService;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task Handle(ImageClasificationResultEvent @event)
        {
            var photoViewModel = _mapper.Map<PhotoViewModel>(@event);
            var model = await _photoService.Get(photoViewModel.Id);
            if (model == null)
            {
                return;
            }
            //save python model
            photoViewModel.UserId = model.UserId;
            await _photoService.Update(photoViewModel);
            await _photoService.SaveChanges();

            //convert data to model available for java API
            var photos = _photoObject.Get()
                .Where(n => n.PhotoId == @event.Id);
            var data = _mapper.Map<IEnumerable<ImageClasificationEventResultEntry>>(photos);
            var webSemanticEvent = new ImagePreprocessingEvent()
            {
                Id = @event.Id,
                Data = data
            };

            _eventBus.Publish(webSemanticEvent);
        }
    }
}
