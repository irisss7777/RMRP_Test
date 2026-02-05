using System;
using Contracts.Domain;
using Contracts.Factory;
using Contracts.Presentation;
using Domain.Player;
using Domain.Player.Inventory;
using Plugins.MessagePipe.MessageBus.Runtime;
using Color = UnityEngine.Color;

namespace Application.Player
{
    public class PlayerInventoryService : IMessageDisposable
    {
        private readonly IInventoryPresenter _inventoryPresenter;
        private readonly IItemFactory _itemFactory;
        private readonly PlayerModel _playerModel;
        private readonly IPlayerPresenter _playerPresenter;

        public event Action OnDispose;

        public PlayerInventoryService(PlayerModel playerModel, IInventoryPresenter inventoryPresenter, IItemFactory itemFactory, IPlayerPresenter playerPresenter)
        {
            _playerModel = playerModel;
            _inventoryPresenter = inventoryPresenter;
            _itemFactory = itemFactory;
            _playerPresenter = playerPresenter;

            _playerModel.Inventory.OnAddItem += OnAddItem;
            _inventoryPresenter.OnItemMoved += ReplaceItem;
            _inventoryPresenter.OnItemDropped += DropItem;
        }

        public bool AddItem(IItem item)
        {
            if (_playerModel.Inventory.HasSlotsForStack(item))
                return true;
            
            return _playerModel.Inventory.TryAddItem(item);
        }

        private void OnAddItem(IInventorySlot slot)
        {
            _inventoryPresenter.AddItem(slot.SlotId, slot);
        }

        private void ReplaceItem(int oldSlotId, int newSlotId)
        {
            var oldSlot = _playerModel.Inventory.GetSlot(oldSlotId);
            var newSlot = _playerModel.Inventory.GetSlot(newSlotId);
    
            _playerModel.Inventory.SetSlot(oldSlotId, newSlot);
            _playerModel.Inventory.SetSlot(newSlotId, oldSlot);
    
            oldSlot.UpdateSlotId(newSlotId); 
            newSlot.UpdateSlotId(oldSlotId);  
  
            _inventoryPresenter.UpdateSlotView(oldSlotId);
            _inventoryPresenter.UpdateSlotView(newSlotId);
        }

        private void DropItem(int id)
        {
            var item = _playerModel.Inventory.GetSlot(id).Item;
            _itemFactory.CreateItem(_playerPresenter.Position, item.Name, item.Id, item.IsStackable, item.MaxStackCount, item.Color, _playerModel.Inventory.GetSlot(id).StackCount);
            
            _playerModel.Inventory.ClearSlot(id);
        }

        public void Dispose()
        {
            OnDispose?.Invoke();

            _playerModel.Inventory.OnAddItem -= OnAddItem;
            _inventoryPresenter.OnItemMoved -= ReplaceItem;
            _inventoryPresenter.OnItemDropped -= DropItem;
        }
    }
}