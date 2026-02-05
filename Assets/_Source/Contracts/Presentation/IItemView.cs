using Contracts.Domain;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Contracts.Presentation
{
    public interface IItemView
    {
        public IItem ItemData { get; }
        
        public void Initialize(IItem data, int id, MessageBus messageBus);
    }
}