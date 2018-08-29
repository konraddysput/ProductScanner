using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Hubs;
using ProductScanner.Gateway.Interfaces.Events;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.Photo;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImageClasificationResultEventHandler : IIntegrationEventHandler<ImageClasificationResultEvent>
    {
        private readonly IHubContext<PreprocesingHub> _hub;
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;

        public ImageClasificationResultEventHandler(
            IPhotoService photoService,
            IHubContext<PreprocesingHub> hub,
            IMapper mapper)
        {
            _hub = hub;
            _photoService = photoService;
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

            photoViewModel.UserId = model.UserId;
            await _photoService.Update(photoViewModel);
            await _photoService.SaveChanges();
            await _hub.Clients.All.SendAsync("DataReady", model.Id, true);
        }
    }
}
