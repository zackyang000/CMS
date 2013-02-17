using System;
namespace AtomLab.Domain
{
    /// <summary>
    /// 处理领域事件方法接口.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }

    /// <summary>
    /// 处理领域事件方法接口.(需实现得到领域对象方法GetEntityId)
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public interface IEntityEventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IEvent
    {
        object GetEntityId(TEvent evnt);
    }
}
