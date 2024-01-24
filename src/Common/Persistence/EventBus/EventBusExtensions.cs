using AppCommon.EventBus;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.EventBus;

public static class EventBusExtensions
{
    public static void AddOutboxMessageBasedEventBus(this IServiceCollection services)
    {
        services.AddTransient<IEventBus, OutboxMessageBasedEventBus>();
    }
}
