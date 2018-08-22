using Abp.Domain.Uow;
using Abp.Events.Bus;

namespace Abp.Extensions
{
    /// <summary>
    /// 事件总线扩展
    /// </summary>
    public static class EventBusExtensions
    {
        /// <summary>
        /// 确保事件触发。如果在事务过程中，确保在事务完成后触发事件
        /// </summary>
        /// <typeparam name="TEventData"></typeparam>
        /// <param name="eventBus"></param>
        /// <param name="currentActiveUnitOfWork"></param>
        /// <param name="eventData"></param>
        /// <param name="triggerInUow"></param>
        public static void EnsureTrigger<TEventData>(this IEventBus eventBus, IActiveUnitOfWork currentActiveUnitOfWork, TEventData eventData, bool triggerInUow = false) where TEventData : EventData
        {
            if (currentActiveUnitOfWork == null || triggerInUow)
            {
                eventBus.Trigger(eventData);
                return;
            }

            currentActiveUnitOfWork.Completed += (sender, args) => eventBus.Trigger(eventData);
        }
    }
}