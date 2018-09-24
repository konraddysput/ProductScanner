﻿using AutoMapper;
using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces.Events;
using ProductScanner.Services.Interfaces;
using ProductScanner.ViewModels.PhotoData;
using ProductScanner.ViewModels.PhotoType;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImagePreprocessingResultEventHandler : IIntegrationEventHandler<ImagePreprocessingResultEvent>
    {
        private readonly IPhotoTypeService _photoTypeService;
        private readonly IPhotoDataService _photoDataService;
        private readonly IMapper _mapper;

        public ImagePreprocessingResultEventHandler(
            IPhotoTypeService photoTypeService,
            IPhotoDataService photoDataService,
            IMapper mapper)
        {
            _photoTypeService = photoTypeService;
            _photoDataService = photoDataService;
            _mapper = mapper;
        }

        public async Task Handle(ImagePreprocessingResultEvent @event)
        {
            foreach (var eventData in @event.Data)
            {

                await AddPhotoData(@event, eventData);
                await AddPhotoTypes(@event, eventData);

            }
            await _photoTypeService.SaveChanges();
        }

        private async Task AddPhotoTypes(ImagePreprocessingResultEvent @event, ImagePreprocessingResultEventEntry eventData)
        {
            var photoTypes = eventData.Types.Select(n => new PhotoTypeViewModel()
            {
                PhotoId = eventData.Id,
                Type = n
            });
            foreach (var photoType in photoTypes)
            {
                await _photoTypeService.Create(photoType);
            }
        }

        private async Task AddPhotoData(ImagePreprocessingResultEvent @event, ImagePreprocessingResultEventEntry eventData)
        {
            var photoData = eventData.Data.Select(n => new PhotoDataViewModel()
            {
                PhotoId = eventData.Id,
                Type = n.Key,
                Value = n.Value
            });

            foreach (var data in photoData)
            {
                await _photoDataService.Create(data);
            }
        }
    }
}
