using System;
using System.Collections.Generic;
using Contracts.Domain;

namespace Domain.Player.Inventory
{
    public class InventoryModel
    {
        private readonly InventorySlot[] _slots;

        public event Action<IInventorySlot> OnAddItem;

        public InventoryModel(int itemSlotsCount)
        {
            List<InventorySlot> slots = new();
            for (var i = 0; i < itemSlotsCount; i++)
            {
                var slot = new InventorySlot(i);
                slots.Add(slot);
            }

            _slots = slots.ToArray();
        }

        public InventorySlot GetSlot(int id) => _slots[id];

        public void SetSlot(int id, InventorySlot slot) => _slots[id] = slot;

        public void ClearSlot(int id)
        {
            _slots[id].ClearSlot();
        }
        
        public bool HasSlotsForStack(IItem item)
        {
            foreach (var slot in _slots)
            {
                if (!slot.SlotIsEmpty && slot.Item.Id == item.Id && item.IsStackable)
                {
                    if (slot.TryAddItem(item))
                    {
                        OnAddItem?.Invoke(slot);
                        return true;
                    }
                }
            }

            return false;
        }

        public bool TryAddItem(IItem item)
        {
            foreach (var slot in _slots)
            {
                if (slot.TryAddItem(item))
                {
                    OnAddItem?.Invoke(slot);
                    return true;
                }
            }
            
            return false;
        }
    }
}