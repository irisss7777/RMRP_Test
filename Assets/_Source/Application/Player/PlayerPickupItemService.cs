using System;
using Contracts.Domain;
using Contracts.Signals;
using Plugins.MessagePipe.MessageBus.Runtime;

namespace Application.Player
{
    public class PlayerPickupItemService : IMessageDisposable
    {
        private readonly PlayerInventoryService _playerInventoryService;
        private readonly MessageBus _messageBus;
        
        public event Action OnDispose;

        private IItem _pickupTarget;
        private int _targetItemViewId;

        public PlayerPickupItemService(MessageBus messageBus, PlayerInventoryService playerInventoryService)
        {
            _messageBus = messageBus;
            _playerInventoryService = playerInventoryService;
            messageBus.Subscribe((SelectForPickupSignal signal) => SetPickupTarget(signal), this);
            messageBus.Subscribe((InputPickupItemSignal signal) => PurchasePickup(), this);
        }

        private void SetPickupTarget(SelectForPickupSignal signal)
        {
            var target = signal.CanPickup ? signal.ItemData : null;

            _pickupTarget = target;
            _targetItemViewId = signal.ItemViewId;
        }

        private void PurchasePickup()
        {
            if(_pickupTarget == null)
                return;
            
            if(_playerInventoryService.AddItem(_pickupTarget))
                _messageBus.Publish(new ItemPickupSignal(_targetItemViewId));

            _pickupTarget = null;
        }

        public void Dispose()
        {
            OnDispose?.Invoke();
        }
    }
}