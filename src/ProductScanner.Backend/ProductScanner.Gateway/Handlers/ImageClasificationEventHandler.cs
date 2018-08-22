﻿using ProductScanner.Gateway.Events;
using ProductScanner.Gateway.Interfaces.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Gateway.Handlers
{
    public class ImageClasificationEventHandler : IIntegrationEventHandler<ImageClasificationIntegrationEvent>
    {

        //public ProductPriceChangedIntegrationEventHandler(IBasketRepository repository)
        //{
        //    _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        //}

        public async Task Handle(ImageClasificationIntegrationEvent @event)
        {
            //logic here
            await Task.CompletedTask;
        }
    }
}