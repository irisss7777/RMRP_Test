using Contracts.Signals;
using MessagePipe;
using Plugins.MessagePipe.MessageBus.Runtime;
using Zenject;

namespace Infrastructure.Installer
{
    public class MessageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MessageBus>().AsSingle();

            var options = Container.BindMessagePipe();

            InputBind(options);
            PresentationBind(options);
        }

        private void InputBind(MessagePipeOptions options) 
        {
            Container.BindMessageBroker<InputMoveSignal>(options);
            Container.BindMessageBroker<InputInventorySignal>(options);
            Container.BindMessageBroker<InputPickupItemSignal>(options);
        }
        
        private void PresentationBind(MessagePipeOptions options) 
        {
            Container.BindMessageBroker<UiStateSignal>(options);
            Container.BindMessageBroker<SelectForPickupSignal>(options);
            Container.BindMessageBroker<ItemPickupSignal>(options);
        }
    }
}