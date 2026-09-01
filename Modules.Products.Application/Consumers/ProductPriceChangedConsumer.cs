using Common.Events; // Sənin Event-in olduğu yer
using MassTransit;
using Modules.Products.Application.Repositories;
using Modules.Products.Domain;


namespace Modules.Products.Application.Consumers;

// IConsumer interfeysi MassTransit-ə bu class-ın bir dinləyici olduğunu bildirir
public class ProductPriceChangedConsumer : IConsumer<ProductPriceChangedEvent>
{
   
    private readonly IProductPriceHistoryRepository _historyRepository;

    public ProductPriceChangedConsumer(IProductPriceHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }


    public async Task Consume(ConsumeContext<ProductPriceChangedEvent> context)
    {
        var message = context.Message; 

      
        var historyRecord = new ProductPriceHistory
        {
            
           ProductId = message.ProductId,
           OldPrice=message.OldPrice,
           NewPrice=message.NewPrice,

        };
       

        await _historyRepository.AddAsync(historyRecord);
        await _historyRepository.SaveChangesAsync();
    }
}